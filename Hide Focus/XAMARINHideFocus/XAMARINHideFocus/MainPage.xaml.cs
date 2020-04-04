using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMARINHideFocus
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Button b1, b2, b3;
        BoxView v1;
        public MainPage()
        {
            InitializeComponent();

            b1 = new Button
            {
                Text = "Press to hide BUTTON 2 focus",
                BackgroundColor = Color.Green,
            };
            stacklayout.Children.Add(b1);
            b1.Clicked += hideFocus;

            b2 = new Button
            {
                Text = "BUTTON 2",
                BackgroundColor = Color.Green,
            };
            stacklayout.Children.Add(b2);

            b3 = new Button
            {
                Text = "BUTTON 3",
                BackgroundColor = Color.Green,
            };
            stacklayout.Children.Add(b3);

            v1 = new BoxView
            {
                BackgroundColor = Color.Green,
                WidthRequest = 100,
                HeightRequest = 50,
            };
            AutomationProperties.SetIsInAccessibleTree(v1, true);
            AutomationProperties.SetName(v1, "Now I am accessible");
            stacklayout.Children.Add(v1);

        }

        private void hideFocus(object sender, EventArgs e)
        {
            b2.Text = "I am no more focusable";
            AutomationProperties.SetIsInAccessibleTree(b2, false);
            AutomationProperties.SetIsInAccessibleTree(v1, false);
            v1.BackgroundColor = Color.Red;
        }
    }
}
