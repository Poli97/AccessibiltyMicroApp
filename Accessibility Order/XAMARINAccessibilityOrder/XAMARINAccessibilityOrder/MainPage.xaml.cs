using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMARINAccessibilityOrder
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Button b1, b2, b3, changeorder;

        public MainPage()
        {
            InitializeComponent();
            stacklayout.Spacing = 10;

            changeorder = new Button
            {
                Text = "Press to change order",
                TabIndex = 0,
            };
            stacklayout.Children.Add(changeorder);
            changeorder.Clicked += changeOrder;

            b1 = new Button
            {
                Text = "BUTTON 1",
                TabIndex = 1,
            };
            stacklayout.Children.Add(b1);

            b2 = new Button
            {
                Text = "BUTTON 2",
                TabIndex = 2
            };
            stacklayout.Children.Add(b2);

            b3 = new Button
            {
                Text = "BUTTON 3",
                TabIndex = 3,
            };
            stacklayout.Children.Add(b3);

        }

        private void changeOrder(object sender, EventArgs e)
        {
            Console.WriteLine("Pressed to change focus b3,b1,b2");
            b3.TabIndex = 1;
            b1.TabIndex = 2;
            b2.TabIndex = 3;
            (sender as Button).Text = "Order changed b3,b1,b2";
        }
    }
}
