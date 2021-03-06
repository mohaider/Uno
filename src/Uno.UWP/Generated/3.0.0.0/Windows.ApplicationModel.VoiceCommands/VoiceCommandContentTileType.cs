#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.ApplicationModel.VoiceCommands
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public   enum VoiceCommandContentTileType 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleOnly,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWithText,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith68x68Icon,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith68x68IconAndText,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith68x92Icon,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith68x92IconAndText,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith280x140Icon,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		TitleWith280x140IconAndText,
		#endif
	}
	#endif
}
