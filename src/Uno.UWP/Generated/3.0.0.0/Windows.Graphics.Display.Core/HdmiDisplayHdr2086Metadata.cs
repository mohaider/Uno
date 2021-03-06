#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Graphics.Display.Core
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial struct HdmiDisplayHdr2086Metadata 
	{
		// Forced skipping of method Windows.Graphics.Display.Core.HdmiDisplayHdr2086Metadata.HdmiDisplayHdr2086Metadata()
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort RedPrimaryX;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort RedPrimaryY;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort GreenPrimaryX;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort GreenPrimaryY;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort BluePrimaryX;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort BluePrimaryY;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort WhitePointX;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort WhitePointY;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort MaxMasteringLuminance;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort MinMasteringLuminance;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort MaxContentLightLevel;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  ushort MaxFrameAverageLightLevel;
		#endif
	}
}
