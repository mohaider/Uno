#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Devices.Geolocation
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public   enum GeoshapeType 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Geopoint,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Geocircle,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Geopath,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		GeoboundingBox,
		#endif
	}
	#endif
}
