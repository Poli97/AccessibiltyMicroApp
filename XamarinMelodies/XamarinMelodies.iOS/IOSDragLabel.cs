using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinMelodies.iOS;
using XamarinMelodies.Model;

[assembly: ExportRenderer(typeof(IDragLabel), typeof(IOSDragLabel))]
namespace XamarinMelodies.iOS
{
    public class IOSDragLabel : LabelRenderer
    {
        CGRect coordDest;
        UIView destinazione;
        UIView sfondo;
        ExercisePage pagina;
        CGRect initcoord;
        int codice;
        bool dragged = false;
        IChangeAccessibilityFocus changeFocusService;
        IAccessibilitySpeak accessSpeak;

        public IOSDragLabel()
        {
            changeFocusService = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);
            accessSpeak = DependencyService.Get<IAccessibilitySpeak>(DependencyFetchTarget.NewInstance);
        }

        string nome;
        IDragLabel custom;
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            
            custom = e.NewElement as IDragLabel;
            if (ExercisePage.CTSopened)
            {
                codice = custom.Codice;
                pagina = custom.Pagina;

            }
            //nome = custom.Testo;
            //pagina = custom.Pagina;
            //Console.WriteLine($"PIPPO: coordinate destinazione = {custom.Destinazione.X} {custom.Destinazione.Y}");

            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            //Control.Text = nome + " IOS";
            this.Layer.BorderColor = Color.Orange.ToCGColor();
            this.Layer.CornerRadius = 10;
            this.Layer.BorderWidth = 2;
            Console.WriteLine("PIPPO created from Renderer");
            


        }

        bool firstcheck = true; //per controllare le coordinate solo la prima volta
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (dragged)
            {
                UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Release to remove"));
            }
            else
            {
                UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Start dragging"));
            }

            if (firstcheck) //devo fare qui i controlli delle coordinate perchè nel costruttore non sono ancora settate, valgono 0, li faccio fare solo la prima volta per evitare inutile spreco di CPU
            {
                UIView vistaBase = ExercisePage.initcoord[codice].GetRenderer().NativeView;
                initcoord = vistaBase.Frame;

                destinazione = ExercisePage.emptyLabelCTSDD.GetRenderer().NativeView;
                Console.WriteLine($"PIPPO: coordinate box destinazione in iOS = {destinazione.Frame}");
            }
            firstcheck = false;

            Console.WriteLine("PIPPO: tocco iniziato");
        }

        bool canspeak = true;
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            this.Center = touch.LocationInView(this.Superview);
            this.Superview.BringSubviewToFront(this);

            CGRect coorv = destinazione.Superview.ConvertRectToCoordinateSpace(destinazione.Frame, this.Superview);

            if (coorv.Contains(touch.LocationInView(this.Superview)))
            {
               if (canspeak)
               {
                    if(ExercisePage.boxIsEmpty == false)
                    {
                        UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Box already filled"));
                    }
                    else
                    {
                        UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Release here"));
                    }
                    canspeak = false;
               }
            }
            else
            {
                canspeak = true;
            }
            
        }

        public override async void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            Console.WriteLine($"DEBUG: destinazione = {destinazione.Frame} {destinazione.Superview.Frame} {this.Superview.Frame}");


            CGRect coorv = destinazione.Superview.ConvertRectToCoordinateSpace(destinazione.Frame, this.Superview);

            Console.WriteLine($"DEBUG: cooridnate convertite: {coorv}");

            UITouch touch = touches.AnyObject as UITouch;
            Console.WriteLine($"PIPPO: tocco finito {touch.LocationInView(this.Superview)}");

            if (ExercisePage.boxIsEmpty)    //se è vuoto posso inseririci la risposta
            {
                if (coorv.Contains(touch.LocationInView(this.Superview)))
                {
                    Console.WriteLine("PIPPO sono dentro");
                    UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Relased in the answer box"));
                    
                    this.Frame = coorv;
                    ExercisePage.boxIsEmpty = false;
                    dragged = true;
                    ExercisePage.isDragged[codice] = true;
                    await Task.Delay(1500);

                    //cambio l'ordine di lettura degli elementi quando trascino
                    AutomationProperties.SetIsInAccessibleTree(ExercisePage.emptyLabelCTSDD, false);
                    AutomationProperties.SetIsInAccessibleTree(ExercisePage.initcoord[codice], true);   //abilito la graybox sotto la rispsota trascinata
                    AutomationProperties.SetName(ExercisePage.initcoord[codice], "empty box");
                    ExercisePage.initcoord[codice].TabIndex = this.TabIndex;    //il suo tabindex diviene uguale a quello della rispsota trascinata
                    this.TabIndex = 11;     //ordino la risposta trascinata dopo la prima label, che vale 10
                    ExercisePage.risposteCTSDD[codice].TabIndex = 11;
                    AutomationProperties.SetHelpText(ExercisePage.risposteCTSDD[codice], "Drag to remove");


                    changeFocusService.ChangeFocus(ExercisePage.firstLabel);    //dopo che ho trascinato la risposta metto il focus sulla prima parte della frase
                }
                else
                {
                    this.Frame = initcoord;
                    UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Returned to initial position"));
                }
            }
            else    //se non è vuoto ho due casi
            {
                this.Frame = initcoord;     //in generale riporto la box nell'origine

                if (dragged)    //caso in cui sto trascinando fuori l'attuale risposta per cambiarla e quindi rimetto a false boxempty
                {
                    ExercisePage.boxIsEmpty = true;
                    UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Removed"));

                    //cambio opzioni ordine lettura
                    AutomationProperties.SetIsInAccessibleTree(ExercisePage.emptyLabelCTSDD, true);
                    ExercisePage.emptyLabelCTSDD.TabIndex = 11;
                    AutomationProperties.SetIsInAccessibleTree(ExercisePage.initcoord[codice], false);   //DISabilito la graybox
                    this.TabIndex = 15 + codice;
                    ExercisePage.risposteCTSDD[codice].TabIndex = 15 + codice;
                    AutomationProperties.SetHelpText(ExercisePage.risposteCTSDD[codice], " ");

                }
                else
                {
                    UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString("Returned at initial position"));
                }
                dragged = false;
                ExercisePage.isDragged[codice] = false;

                await Task.Delay(1000);
                changeFocusService.ChangeFocus(ExercisePage.firstLabel);

            }


        }
    }

}
