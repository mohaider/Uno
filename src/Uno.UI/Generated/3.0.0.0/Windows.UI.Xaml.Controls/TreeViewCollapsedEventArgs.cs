#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.UI.Xaml.Controls
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial class TreeViewCollapsedEventArgs 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Xaml.Controls.TreeViewNode Node
		{
			get
			{
				throw new global::System.NotImplementedException("The member TreeViewNode TreeViewCollapsedEventArgs.Node is not implemented in Uno.");
			}
		}
		#endif
		// Forced skipping of method Windows.UI.Xaml.Controls.TreeViewCollapsedEventArgs.Node.get
	}
}
