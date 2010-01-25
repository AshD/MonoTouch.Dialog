//
// Sample showing the core Element-based API to create a dialog
//
using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace Sample
{
	class Settings {
	[Section]
		public bool AccountEnabled;
				
	[Section ("Account", "Your credentials")]
		
		[Entry ("Enter your login name")]
		public string Login;
		
		[Password ("Enter your password")]
		public string Password;
		
	[Section ("Time Editing")]
		
		public DateTime Appointment;
		
		[Date]
		public DateTime Birthday;
		
		[Time]
		public DateTime Alarm;
		
	[Section ("Enumerations")]
		
		[Caption ("Favorite CLR type")]
		public TypeCode FavoriteType;
		
	[Section ("Checkboxes")]
		[Checkbox]
		bool English;
		
		[Checkbox]
		bool Spanish = true;
		
	[Section ("Image Selection")]
		public UIImage Top;
		public UIImage Middle;
		public UIImage Bottom;
	}
	
	public partial class AppDelegate 
	{
		Settings settings;
		
		public void DemoReflectionApi ()
		{	
			if (settings == null){
				var image = UIImage.FromFile ("monodevelop-32.png");
				
				settings = new Settings () {
					AccountEnabled = true,
					Login = "postmater@localhost.com",
					Appointment = DateTime.Now,
					Birthday = new DateTime (1980, 6, 24),
					Alarm = new DateTime (2000, 1, 1, 7, 30, 0, 0),
					FavoriteType = TypeCode.Int32,
					Top = image,
					Middle = image,
					Bottom = image
				};
			}
			var bc = new BindingContext (null, settings, "Settings");
			
			var dv = new DialogViewController (bc.Root, true);
			
			// When the view goes out of screen, we fetch the data.
			dv.ViewDissapearing += delegate {
				// This reflects the data back to the object instance
				bc.Fetch ();
				
				// Manly way of dumping the data.
				Console.WriteLine ("Current status:");
				Console.WriteLine (
				    "AccountEnabled: {0}\n" +
				    "Login:          {1}\n" +
				    "Password:       {2}\n" +
				    "Appointment:    {3}\n" +
				    "Birthday:       {4}\n" +
				    "Alarm:          {5}\n" +
				    "Favorite Type:  {6}\n", 
				    settings.AccountEnabled, settings.Login, settings.Password, 
				    settings.Appointment, settings.Birthday, settings.Alarm, settings.FavoriteType);
			};
			navigation.PushViewController (dv, true);	
		}
	}
}