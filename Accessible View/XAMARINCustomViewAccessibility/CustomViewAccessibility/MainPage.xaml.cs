using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomViewAccessibility
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            Button notAccessibleButton = new Button
            {
                Text = "USELESS BUTTON",
                BackgroundColor = Color.Gray,
            };
            stacklayout.Children.Add(notAccessibleButton);

            IAccessibleButton accessibleButton = new IAccessibleButton();
            accessibleButton.Text = "NOT IN FOCUS";
            AutomationProperties.SetIsInAccessibleTree(accessibleButton, true);
            stacklayout.Children.Add(accessibleButton);

        }

	}
}