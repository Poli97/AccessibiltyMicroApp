using System;
using System.ComponentModel;
using System.Reflection;
using CustomViewAccessibility;
using CustomViewAccessibility.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IAccessibleButton), typeof(IOSAccessibleButton))]
namespace CustomViewAccessibility.iOS
{
    public class IOSAccessibleButton : ButtonRenderer
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

		protected override UIButton CreateNativeControl()
		{
			return new AccessibleButton();
		}
	}

	public class AccessibleButton : UIButton
	{
		
		public AccessibleButton() :base()
		{
			Console.WriteLine("***** AccessibleButton created *****");
		}

		public override void AccessibilityElementDidBecomeFocused()
		{
			base.AccessibilityElementDidBecomeFocused();
			this.BackgroundColor = UIColor.Green;
			this.SetTitle("I AM IN FOCUS", UIControlState.Normal);
			Console.WriteLine("ACCESSIBILITY I am in focus");
		}

		public override void AccessibilityElementDidLoseFocus()
		{
			base.AccessibilityElementDidLoseFocus();
			this.BackgroundColor = UIColor.Red;
			this.SetTitle("I AM NO MORE IN FOCUS", UIControlState.Normal);
			Console.WriteLine("ACCESSIBILITY I am NOT in focus");
		}
	}
}