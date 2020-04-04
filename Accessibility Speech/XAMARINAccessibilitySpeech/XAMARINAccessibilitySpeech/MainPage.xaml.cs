using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMARINAccessibilitySpeech
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IAccessibilitySpeakService accessibilitySpeak;
        Button speakButton;

        public MainPage()
        {
            InitializeComponent();

            accessibilitySpeak = DependencyService.Get<IAccessibilitySpeakService>();

            speakButton = new Button
            {
                Text = "Press me to speak",
                BackgroundColor = Color.Green,
            };
            speakButton.Clicked += pressSpeak;

            stacklayout.Children.Add(speakButton);
        }

        private void pressSpeak(object sender, EventArgs e)
        {
            if(Device.RuntimePlatform == Device.iOS)
            {
                accessibilitySpeak.speakText(sender as View, "This is an accessibility speak in XAMARIN IOS");
            }
            if(Device.RuntimePlatform == Device.Android)
            {
                accessibilitySpeak.speakText(sender as View, "This is an accessibility speak in XAMARIN ANDROID");
            }

        }
    }
}
