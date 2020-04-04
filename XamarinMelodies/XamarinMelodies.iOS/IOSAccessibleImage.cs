using System;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinMelodies.iOS;
using XamarinMelodies.Model;

[assembly: ExportRenderer(typeof(XamarinMelodies.Model.ToggleButton), typeof(IOSAccessibleImage))]
[assembly: ExportRenderer(typeof(IAccessibleImage), typeof(IOSAccessibleImage))]
namespace XamarinMelodies.iOS
{
    public class IOSAccessibleImage: ImageRenderer
    {
        IAccessibleImage renderer;
        Model.ToggleButton toggleRenderer;
        string animal = "nil";
        int indice = -1;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            //prendo il parametro stringa animal per capire se è dog o cat
            if (ExercisePage.isToggleButton == 1)    //controllo se è true significa che sono nell'esercizio IOD
            {
                toggleRenderer = e.NewElement as Model.ToggleButton;
                animal = toggleRenderer.Animal;
                indice = toggleRenderer.indice;
            }

            if (ExercisePage.isToggleButton == 0)
            {
                renderer = e.NewElement as IAccessibleImage;
                animal = renderer.Animal;
            }

            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            SetNativeControl(new NativeAccessibleImageIOS(animal, indice));

        }

        /*protected override UIImageView CreateNativeControl()
        {
            return new NativeAccessibleImageIOS("ciao");
        }*/
    }


    //creo sottoclasse di UIImageView nativa IOS
    public class NativeAccessibleImageIOS : FormsUIImageView
    {
        string animal;
        int indice;

        public NativeAccessibleImageIOS(string animal, int index)
        {
            this.animal = animal;
            this.indice = index;
            //this.UserInteractionEnabled = true;
            this.BackgroundColor = UIColor.White;
            this.AccessibilityTraits = UIAccessibilityTrait.None;
            Debug.WriteLine($"DEBUG: accessible image with {animal} in IOS");

            if (ExercisePage.isToggleButton == 0)
            {
                this.AccessibilityLabel = " ";
            }
        }

        public override void AccessibilityElementDidBecomeFocused()
        {
            base.AccessibilityElementDidBecomeFocused();
            Debug.WriteLine($"DEBUG {animal} I am in focus");

            if (indice != -1)
            {
                if (ExercisePage.toggleButtons[indice].Animal == "dog")
                {
                    ExercisePage.dogPlayer.Play();
                }
            }
            else
            {
                if (animal == "dog")
                {
                    ExercisePage.dogPlayer.Play();
                }


                if (animal == "cat")
                {
                    ExercisePage.catPlayer.Play();
                }
            }
        }
    }

}
