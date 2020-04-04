using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinMelodies.Model;

namespace XamarinMelodies
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static Color navColor;
        public static Color navBarColor;
        public static ImageSource navImage;
        public IChangeAccessibilityFocus service;  //dependency service per il cambio di focus

        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("DEBUG: APPLICATION STARTING");

            Title = "XAMARIN MELODIES";

            service = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);

            if (Device.RuntimePlatform == Device.iOS)
            {
                navColor = Color.Blue;
                navBarColor = Color.White;
                navImage = ImageSource.FromResource("XamarinMelodies.Immagini.blackBackNav.png");
                AutomationProperties.SetIsInAccessibleTree(startImage, false);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                navColor = Color.White;
                navBarColor = Color.FromHex("019ee6");
                navImage = ImageSource.FromResource("XamarinMelodies.Immagini.whiteBackNav.png");
            }

            this.BackgroundImageSource = ImageSource.FromResource("XamarinMelodies.Immagini.bg1.jpg");

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            Debug.WriteLine($"DEBUG: Width: {screenWidth}");
            Debug.WriteLine($"DEBUG: scarico model {Modell.globalEsercizi.exercises}");

            AbsoluteLayout.SetLayoutBounds(startImage, new Rectangle(0.5, 0.40, 0.80, 0.70));
            AbsoluteLayout.SetLayoutFlags(startImage, AbsoluteLayoutFlags.All);

            var label1 = new Label();
            label1.Text = "Developed by Pecis Paolo \n @EveryWareLab";
            label1.FontAttributes = FontAttributes.Bold;
            label1.HorizontalTextAlignment = TextAlignment.Center;
            AbsoluteLayout.SetLayoutBounds(label1, new Rectangle(0.01, 0.95, 0.28, 0.20));
            AbsoluteLayout.SetLayoutFlags(label1, AbsoluteLayoutFlags.All);
            container1.Children.Add(label1);
            AutomationProperties.SetIsInAccessibleTree(label1, true);
            label1.TabIndex = 0;


            //GRAFICA DEL BOTTONE START
            startButton.BackgroundColor = Color.Orange;
            startButton.BorderColor = Color.White;
            startButton.TextColor = Color.White;
            startButton.CornerRadius = 8;
            startButton.BorderWidth = 2;
            AbsoluteLayout.SetLayoutBounds(startButton, new Rectangle(0.5, 0.9, 0.35, 0.15));
            AbsoluteLayout.SetLayoutFlags(startButton, AbsoluteLayoutFlags.All);

            //var bottone2 = new Button { Text = "Start222" };
            //container1.Children.Add(bottone2);
            //AbsoluteLayout.SetLayoutBounds(bottone2, new Rectangle(0.5, 0.2, 200, 100));
            //bottone2.BackgroundColor = Color.Beige;


            Debug.WriteLine($"Button: {startButton.Width}");
        }

        async void goToList(object sender, EventArgs e)
        {
            Debug.Write("Cliccato START");
            await Navigation.PushAsync(new ListPage());

        }
    }
}
