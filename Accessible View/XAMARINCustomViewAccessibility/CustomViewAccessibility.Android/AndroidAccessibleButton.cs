using System;
using Android.Content;
using Android.Service.Autofill;
using Android.Views.Accessibility;
using CustomViewAccessibility;
using CustomViewAccessibility.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IAccessibleButton), typeof(AndroidAccessibleButton))]
namespace CustomViewAccessibility.Droid
{
    public class AndroidAccessibleButton : ButtonRenderer
    {
        Context context;

        public AndroidAccessibleButton(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            Console.WriteLine("PIPPO created from Android");
            Control.SetBackgroundColor(Android.Graphics.Color.Red);
        }

		protected override Android.Widget.Button CreateNativeControl()
		{
			return new AccessibleButton(context);
		}

		
    }

	public class AccessibleButton : Android.Widget.Button
	{
		public AccessibleButton(Context context) : base(context)
		{
            Console.WriteLine("***** AccessibleButton created *****");
        }

		public override void OnInitializeAccessibilityEvent(AccessibilityEvent e)
		{
			base.OnInitializeAccessibilityEvent(e);
            if (e.EventType == EventTypes.ViewAccessibilityFocused)
            {
                this.SetBackgroundColor(Android.Graphics.Color.Green);
                this.Text = "I AM IN FOCUS";
                Console.WriteLine("ACCESSIBILITY I am in focus");
            }
			else if (e.EventType == EventTypes.ViewAccessibilityFocusCleared)
			{
                this.SetBackgroundColor(Android.Graphics.Color.Red);
                this.Text = "I AM NO MORE IN FOCUS";
                Console.WriteLine("ACCESSIBILITY I am in NOT in focus");
            }
        }
	}
}
