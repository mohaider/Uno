#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Globalization.NumberFormatting
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial interface INumberFormatter 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		string Format( long value);
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		string Format( ulong value);
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		string Format( double value);
		#endif
	}
}
