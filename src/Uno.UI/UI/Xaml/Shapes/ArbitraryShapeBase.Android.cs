﻿using Android.Graphics;
using Uno.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Uno.UI;
using Windows.Foundation;
using Uno.Disposables;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Views;
using Uno.Extensions;
using Windows.UI.Xaml.Media;

namespace Windows.UI.Xaml.Shapes
{
	public abstract partial class ArbitraryShapeBase : Shape
	{
		// Drawing scale
		private double _scaleX;
		private double _scaleY;

		// Drawing size
		private double _calculatedWidth;
		private double _calculatedHeight;

		// Drawing container size
		private double _controlWidth;
		private double _controlHeight;

		private double _pathWidth;
		private double _pathHeight;

		public ArbitraryShapeBase()
		{

		}

		private Size GetActualSize() => new Size(_controlWidth, _controlHeight);

		protected override void OnBackgroundChanged(DependencyPropertyChangedEventArgs e)
		{
			// Don't call base, we need to keep UIView.BackgroundColor set to transparent
			RefreshShape();
		}

		protected override void OnLoaded()
		{
			base.OnLoaded();

			RefreshShape();
		}

		protected abstract Android.Graphics.Path GetPath();

		private IDisposable BuildDrawableLayer()
		{
			if(_controlHeight == 0 || _controlWidth == 0)
			{
				return Disposable.Empty;
			}

			var drawables = new List<Drawable>();

			var path = GetPath();
			if (path == null)
			{
				return Disposable.Empty;
			}

			// Scale the path using its Stretch
			Android.Graphics.Matrix matrix = new Android.Graphics.Matrix();
			switch (this.Stretch)
			{
				case Media.Stretch.Fill:
				case Media.Stretch.None:
					matrix.SetScale((float)_scaleX, (float)_scaleY);
					break;
				case Media.Stretch.Uniform:
					var scale = Math.Min(_scaleX, _scaleY);
					matrix.SetScale((float)scale, (float)scale);
					break;
				case Media.Stretch.UniformToFill:
					scale = Math.Max(_scaleX, _scaleY);
					matrix.SetScale((float)scale, (float)scale);
					break;
			}
			path.Transform(matrix);

			// Move the path using its alignements
			var translation = new Android.Graphics.Matrix();

			var pathBounds = new RectF();

			// Compute the bounds. This is needed for stretched shapes and stroke thickness translation calculations.
			path.ComputeBounds(pathBounds, true);

			if (Stretch == Stretch.None)
			{
				// Since we are not stretching, ensure we are using (0, 0) as origin.
				pathBounds.Left = 0;
				pathBounds.Top = 0;
			}

			if (!ShouldPreserveOrigin)
			{
				//We need to translate the shape to take in account the stroke thickness
				translation.SetTranslate((float)(-pathBounds.Left + PhysicalStrokeThickness * 0.5f), (float)(-pathBounds.Top + PhysicalStrokeThickness * 0.5f));
			}

			path.Transform(translation);

			// Draw the fill
			var drawArea = new Foundation.Rect(0, 0, _controlWidth, _controlHeight);

            var imageBrushFill = Fill as ImageBrush;
            if (imageBrushFill != null)
            {
				var bitmapDrawable = new BitmapDrawable(Context.Resources, imageBrushFill.TryGetBitmap(drawArea, () => RefreshShape(forceRefresh: true), path));
				drawables.Add(bitmapDrawable);
            }
            else
            {
				var fill = Fill ?? SolidColorBrushHelper.Transparent;
				var fillPaint = fill.GetFillPaint(drawArea);

				var lineDrawable = new PaintDrawable();
				lineDrawable.Shape = new PathShape(path, (float)_controlWidth, (float)_controlHeight);
				lineDrawable.Paint.Color = fillPaint.Color;
				lineDrawable.Paint.SetShader(fillPaint.Shader);
				lineDrawable.Paint.SetStyle(Paint.Style.Fill);
				lineDrawable.Paint.Alpha = fillPaint.Alpha;

				this.SetStrokeDashEffect(lineDrawable.Paint);

				drawables.Add(lineDrawable);
			}

			// Draw the contour
			if (Stroke != null)
			{
				using (var strokeBrush = new Paint(Stroke.GetStrokePaint(drawArea)))
				{
					var lineDrawable = new PaintDrawable();
					lineDrawable.Shape = new PathShape(path, (float)_controlWidth, (float)_controlHeight);
					lineDrawable.Paint.Color = strokeBrush.Color;
					lineDrawable.Paint.SetShader(strokeBrush.Shader);
					lineDrawable.Paint.StrokeWidth = (float)PhysicalStrokeThickness;
					lineDrawable.Paint.SetStyle(Paint.Style.Stroke);
					lineDrawable.Paint.Alpha = strokeBrush.Alpha;

					this.SetStrokeDashEffect(lineDrawable.Paint);

					drawables.Add(lineDrawable);
				}
			}

			var layerDrawable = new LayerDrawable(drawables.ToArray());

            // Set bounds must always be called, otherwise the android layout engine can't determine
            // the rendering size. See Drawable documentation for details.
            layerDrawable.SetBounds(0, 0, (int)_controlWidth, (int)_controlHeight);

            return SetOverlay(this, layerDrawable);
		}

		private IDisposable SetOverlay(View view, Drawable drawable)
		{
#if __ANDROID_18__
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
			{
				using (PreventRequestLayout())
				{
					Overlay.Add(drawable);
				}

				return Disposable.Create(
					() => {
						using (PreventRequestLayout())
						{
							Overlay.Remove(drawable);
						}
					}
				);
			}
			else
#endif
			{
#pragma warning disable 0618 // Used for compatibility with SetBackgroundDrawable and previous API Levels

                // Set overlay is not supported by this platform, set use the background instead.
                // It'll break some scenarios, like having borders on top of the content.
                view.SetBackgroundDrawable(drawable);
				return Disposable.Create(() => view.SetBackgroundDrawable(null));

#pragma warning restore 0618
			}
		}

		protected override void OnLayoutCore(bool changed, int left, int top, int right, int bottom)
		{
			_controlWidth = right - left;
			_controlHeight = bottom - top;

			RefreshShape();
			base.OnLayoutCore(changed, left, top, right, bottom);
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			RectF pathBounds = new RectF();
			var path = this.GetPath();
			if (path != null)
			{
				path.ComputeBounds(pathBounds, false);
			}

			_pathWidth = pathBounds.Width();
			_pathHeight = pathBounds.Height();
			
			if (ShouldPreserveOrigin)
			{
				_pathWidth += pathBounds.Left;
				_pathHeight += pathBounds.Top;
			}

			var aspectRatio = _pathWidth / _pathHeight;

			var widthMode = ViewHelper.MeasureSpecGetMode(widthMeasureSpec);
			var heightMode = ViewHelper.MeasureSpecGetMode(heightMeasureSpec);

			var availableWidth = ViewHelper.MeasureSpecGetSize(widthMeasureSpec);
			var availableHeight = ViewHelper.MeasureSpecGetSize(heightMeasureSpec);

			var userWidth = ViewHelper.LogicalToPhysicalPixels(this.Width);
			var userHeight = ViewHelper.LogicalToPhysicalPixels(this.Height);

			switch (widthMode)
			{
				case Android.Views.MeasureSpecMode.AtMost:
				case Android.Views.MeasureSpecMode.Exactly:
					_controlWidth = availableWidth;
					break;
				default:
				case Android.Views.MeasureSpecMode.Unspecified:
					switch (Stretch)
					{
						case Stretch.Uniform:
							if (heightMode != Android.Views.MeasureSpecMode.Unspecified)
							{
								_controlWidth = availableHeight * aspectRatio;
							}
							else
							{
								_controlWidth = _pathWidth;
							}
                            break;
						default:
							_controlWidth = _pathWidth;
							break;
					}
					break;
			}

			switch (heightMode)
			{
				case Android.Views.MeasureSpecMode.AtMost:
				case Android.Views.MeasureSpecMode.Exactly:
					_controlHeight = availableHeight;
					break;
				default:
				case Android.Views.MeasureSpecMode.Unspecified:
					switch (Stretch)
					{
						case Stretch.Uniform:
							if (widthMode != Android.Views.MeasureSpecMode.Unspecified)
							{
								_controlHeight = availableWidth / aspectRatio;
							}
							else
							{
								_controlHeight = _pathHeight;
							}
							break;
						default:
							_controlHeight = _pathHeight;
							break;
					}
					break;
			}

			// Default values
			_calculatedWidth = LimitWithUserSize(_controlWidth, userWidth, _pathWidth);
			_calculatedHeight = LimitWithUserSize(_controlHeight, userHeight, _pathHeight);
			_scaleX = (_calculatedWidth - this.PhysicalStrokeThickness) / _pathWidth;
			_scaleY = (_calculatedHeight - this.PhysicalStrokeThickness) / _pathHeight;

			// Here we will override some of the default values
			switch (this.Stretch)
			{
				// If the Stretch is None, the drawing is not the same size as the control
				case Media.Stretch.None:
					_scaleX = 1;
					_scaleY = 1;
					_calculatedWidth = (double)_pathWidth;
					_calculatedHeight = (double)_pathHeight;
					break;
				case Media.Stretch.Fill:
					break;
				// Override the _calculated dimensions if the stretch is Uniform or UniformToFill
				case Media.Stretch.Uniform:
					var scale = Math.Min(_scaleX, _scaleY);
					_calculatedWidth = _pathWidth * scale;
					_calculatedHeight = _pathHeight * scale;
					break;
				case Media.Stretch.UniformToFill:
					scale = Math.Max(_scaleX, _scaleY);
					_calculatedWidth = _pathWidth * scale;
					_calculatedHeight = _pathHeight * scale;
					break;
			}

			_calculatedWidth += this.PhysicalStrokeThickness;
			_calculatedHeight += this.PhysicalStrokeThickness;

			SetMeasuredDimension((int)_calculatedWidth, (int)_calculatedHeight);
			IFrameworkElementHelper.OnMeasureOverride(this);
		}
	}
}
