#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Storage
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial interface IStorageItem2 : global::Windows.Storage.IStorageItem
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		global::Windows.Foundation.IAsyncOperation<global::Windows.Storage.StorageFolder> GetParentAsync();
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		bool IsEqual( global::Windows.Storage.IStorageItem item);
		#endif
	}
}
