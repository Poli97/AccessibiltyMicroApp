using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMARINAccessibilityName
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Button b1, b2;
        Label l1, l2;
        IAccessibilityName accessibilityNameService;

        public MainPage()
        {
            InitializeComponent();

            accessibilityNameService = DependencyService.Get<IAccessibilityName>(DependencyFetchTarget.NewInstance);

            b1 = new Button
            {
                Text = "I HAVE AN ACCESSIBILITY DESCRIPTION",
                BackgroundColor = Color.Green,
            };
            AutomationProperties.SetName(b1, "This is my accessibility name");
            AutomationProperties.SetHelpText(b1, "And this is my accessibility help text");
            stacklayout.Children.Add(b1);

            b2 = new Button
            {
                Text = "USELESS BUTTON WITH NO DESCRIPTION",
                BackgroundColor = Color.Green,
            };
            stacklayout.Children.Add(b2);

            l1 = new Label
            {
                BackgroundColor = Color.Orange,
                WidthRequest = 100,
                HeightRequest = 100,
            };
            AutomationProperties.SetIsInAccessibleTree(l1, true);
            AutomationProperties.SetName(l1, "Accessibility name should not be visible");
            AutomationProperties.SetHelpText(l1, "Neither accessibility help text should not be visible");
            stacklayout.Children.Add(l1);

            l2 = new Label
            {
                BackgroundColor = Color.Orange,
                WidthRequest = 100,
                HeightRequest = 100,
            };
            AutomationProperties.SetIsInAccessibleTree(l2, true);
            stacklayout.Children.Add(l2);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            accessibilityNameService.setName(l2, "Accessibility name setted with dependency service");

        }
    }
}
