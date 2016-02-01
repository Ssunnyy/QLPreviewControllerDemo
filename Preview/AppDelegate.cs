using Foundation;
using UIKit;
using QuickLook;
using System;

namespace Preview
{
	class PreviewDelegate : QLPreviewControllerDelegate
	{
	}

	class PreviewDataSource : QLPreviewControllerDataSource
	{
		#region implemented abstract members of QLPreviewControllerDataSource

		public override IQLPreviewItem GetPreviewItem (QLPreviewController controller, nint index)
		{
			// This does not work:
			//var url = NSUrl.FromFilename("pdf.pdf");

			// This does:
			var url = NSUrl.FromFilename(NSBundle.MainBundle.PathForResource ("pdf.pdf", null));
			return new QLItem ("Some PDF", url);
		}

		public override nint PreviewItemCount (QLPreviewController controller)
		{
			return 1;
		}

		#endregion
	}

	class QLItem : QLPreviewItem
	{
		public QLItem (string title, NSUrl uri)
		{
			this.title = title;
			url = uri;
		}

		private readonly string title;

		public override string ItemTitle
		{
			get { return title; }
		}

		private readonly NSUrl url;

		public override NSUrl ItemUrl
		{
			get { return url; }
		}
	}

	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
			this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
			this.Window.MakeKeyAndVisible();

			var preview = new QLPreviewController ();
			preview.DataSource = new PreviewDataSource ();
			preview.Delegate = new PreviewDelegate();

			// You can present the controller directly...
			//this.Window.RootViewController = new UINavigationController(preview);

		 	// ...or add its view to your own view hierarchy.
			this.Window.RootViewController = new UIViewController();
			this.Window.RootViewController.View.AddSubview(preview.View);
			return true;
		}
	}
}


