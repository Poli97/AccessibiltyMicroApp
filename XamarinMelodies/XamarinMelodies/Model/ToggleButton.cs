using System;
using System.Diagnostics;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class ToggleButton: Image
    {
        public bool isToggled = false;
        Image imageFrame = new Image();
        public string Animal;
        public int indice;

        public ToggleButton(int index, bool toggled, string animal)
        {
            AutomationProperties.SetIsInAccessibleTree(this, true);

            TabIndex = index+1;
            isToggled = toggled;
            indice = index;
            Animal = animal;

            //imageFrame.Source = ImageSource.FromResource("XamarinMelodies.Immagini.dog.png");
            //imageFrame.Aspect = Aspect.AspectFit;

            //BorderColor = Color.Blue;
            //Padding = 4;
            if(isToggled == false)
            {
                this.Source = null;
                this.BackgroundColor = Color.White;
                //Content = null;
                AutomationProperties.SetName(this, "Empty button, double tap to insert dog");
            }
            else
            {
                this.BackgroundColor = Color.White;
                this.Source = ImageSource.FromResource("XamarinMelodies.Immagini.dog.png");
                //Content = imageFrame;
                AutomationProperties.SetName(this, " , double tap do remove");
            }

            TapGestureRecognizer tapEvent = new TapGestureRecognizer();
            tapEvent.Tapped += toggle;
            this.GestureRecognizers.Add(tapEvent);

        }

        public void toggle(object sender, EventArgs e)
        {
            if(isToggled == false)
            {
                ExercisePage.dogPlayer.Play();
                isToggled = true;
                ExercisePage.toggleButtons[indice].Animal = "dog";
                this.Source = ImageSource.FromResource("XamarinMelodies.Immagini.dog.png");
                AutomationProperties.SetName(this, " , double tap do remove");
                Debug.WriteLine("DEBUG: bottone attivato con immagine");
            }
            else
            {
                ExercisePage.popSound1.Play();
                isToggled = false;
                ExercisePage.toggleButtons[indice].Animal = "nil";
                this.Source = null;
                AutomationProperties.SetName(this, "Empty button, double tap to insert dog");
                Debug.WriteLine("DEBUG: bottone disattivato, rimuovo immagine");
            }
        }

    }
}
