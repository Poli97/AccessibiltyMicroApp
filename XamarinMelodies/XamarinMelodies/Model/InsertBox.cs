using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class InsertBox : Frame
    {
        public Label l;
        public InsertBox()
        {
            l = new Label
            {
                Text = "" + 0,
            };
            BackgroundColor = Color.White;
            BorderColor = Color.Blue;
            Padding = 4;

            TapGestureRecognizer tapEvent = new TapGestureRecognizer();
            tapEvent.Tapped += openKeyboard;
            this.GestureRecognizers.Add(tapEvent);
        }

        private void openKeyboard(object sender, EventArgs e)
        {
            Debug.WriteLine("DEBUG: aperta tastiera");
            ExercisePage.keyboardCO.IsVisible = true;
            this.BorderColor = Color.Red;
        }
    }
}
