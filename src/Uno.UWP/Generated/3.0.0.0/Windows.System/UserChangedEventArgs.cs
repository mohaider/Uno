#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.System
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial class UserChangedEventArgs 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.System.User User
		{
			get
			{
				throw new global::System.NotImplementedException("The member User UserChangedEventArgs.User is not implemented in Uno.");
			}
		}
		#endif
		// Forced skipping of method Windows.System.UserChangedEventArgs.User.get
	}
}
