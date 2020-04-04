using System;
using System.Diagnostics;
using Android.Content;
using Android.Views.Accessibility;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMelodies.Droid;
using XamarinMelodies.Model;

[assembly: ExportRenderer(typeof(XamarinMelodies.Model.ToggleButton), typeof(AndroidAccessibleImage))]
[assembly: ExportRenderer(typeof(IAccessibleImage), typeof(AndroidAccessibleImage))]
namespace XamarinMelodies.Droid
{
    public class AndroidAccessibleImage: ImageRenderer
    {
        Context context;
        IAccessibleImage renderer;
        Model.ToggleButton toggleRenderer;
        public static string animal = "nil";
        int indice = -1;

        public AndroidAccessibleImage(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            //prendo il parametro stringa animal per capire se è dog o cat
            if (ExercisePage.isToggleButton == 1)    //controllo se è true significa che sono nell'esercizio IOD
            {
                toggleRenderer = e.NewElement as Model.ToggleButton;
                indice = toggleRenderer.indice;
                animal = toggleRenderer.Animal;
            }

            if(ExercisePage.isToggleButton == 0)
            {
                renderer = e.NewElement as IAccessibleImage;
                animal = renderer.Animal;
            }

            

            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

        }

        protected override ImageView CreateNativeControl()
        {
            return new NativeAccessibleImageAndroid(context, animal, indice);
        }

    }



//creo la sottoclasse di ImageView nativa android
    public class NativeAccessibleImageAndroid : ImageView
    {
        String animal;
        int indice;

        public NativeAccessibleImageAndroid(Context context, string animale, int index) : base(context)
        {
            Debug.WriteLine($"DEBUG: accessible image with {animale} in Android");
            this.animal = animale;
            this.indice = index;
            this.SetBackgroundColor(Android.Graphics.Color.White);

        }

        public override void OnInitializeAccessibilityEvent(AccessibilityEvent e)
        {
            base.OnInitializeAccessibilityEvent(e);
            if (e.EventType == EventTypes.ViewAccessibilityFocused)
            {
                Debug.WriteLine($"DEBUG: {animal} i am in FOCUS");

                if(indice != -1)
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
            else if (e.EventType == EventTypes.ViewAccessibilityFocusCleared)
            {
                Console.WriteLine("PIPPO I am in NOT in focus");
            }
        }

    }
}
