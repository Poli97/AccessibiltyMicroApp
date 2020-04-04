using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMARINChangeFocus
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Button b1, b2, b3;
        IChangeFocusService changeFocusService;

        public MainPage()
        {
            InitializeComponent();
            stacklayout.Spacing = 10;
            stacklayout.Margin = new Thickness(0, 10, 0, 0);

            changeFocusService = DependencyService.Get<IChangeFocusService>();

            b1 = new Button
            {
                Text = "Press to change focus to BUTTON 3",
                BackgroundColor = Color.Green,
            };
            stacklayout.Children.Add(b1);
            b1.Clicked += ChangeFocusTob3;

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

        }

        private void ChangeFocusTob3(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked to change focus to BUTTON 3");
            changeFocusService.ChangeFocus(b3);
        }
    }
}
