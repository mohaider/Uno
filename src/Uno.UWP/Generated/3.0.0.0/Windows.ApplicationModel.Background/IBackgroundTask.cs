#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.ApplicationModel.Background
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial interface IBackgroundTask 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		void Run( global::Windows.ApplicationModel.Background.IBackgroundTaskInstance taskInstance);
		#endif
	}
}
