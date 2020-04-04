using System;
using System.ComponentModel;
using System.Reflection;
using CustomViewAccessibility;
using CustomViewAccessibility.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ICustomViewRenderer), typeof(IOSCustomView))]
namespace CustomViewAccessibility.iOS
{
    public class IOSCustomView : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {

            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            Console.WriteLine("PIPPO created from IOS");
            Control.BackgroundColor = UIColor.Red;
			
        }

		// Jon Added
		protected override UIButton CreateNativeControl()
		{
			return new AccessibleButton();
		}


	}

	//Jon Added
	public class AccessibleButton : UIButton
	{
		
		public AccessibleButton() :base()
		{
			Console.WriteLine($"***** AccessibleButton created *****");
			
		}

		public override void AccessibilityElementDidBecomeFocused()
		{
			base.AccessibilityElementDidBecomeFocused();
			this.BackgroundColor = UIColor.Green;
			Console.WriteLine("PIPPO I am in focus");
		}

		public override void AccessibilityElementDidLoseFocus()
		{
			base.AccessibilityElementDidLoseFocus();
			this.BackgroundColor = UIColor.Red;
			Console.WriteLine("PIPPO I am NOT in focus");
		}
	}
}