using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class DragBox: Label
    {
        ExercisePage pagina;
        public bool isSelected = false;
        public bool canBeSelected = true;
        public string parola;
        IAccessibilitySpeak accessSpeak;
        IChangeAccessibilityFocus changeFocusService;
        IAccessibleName accessibleName;

        public DragBox(ExercisePage p, string parola)
        {
            accessSpeak = DependencyService.Get<IAccessibilitySpeak>(DependencyFetchTarget.NewInstance);
            changeFocusService = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);
            accessibleName = DependencyService.Get<IAccessibleName>(DependencyFetchTarget.NewInstance);

            pagina = p;
            this.parola = parola;
            Text = parola;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
            BackgroundColor = Color.FromHex("#fca128");
            VerticalOptions = LayoutOptions.Center;
            WidthRequest = 150;
            HeightRequest = pagina.screen_height * 0.22;

            AutomationProperties.SetIsInAccessibleTree(this, true);
            //accessibleName.setAccessHint(this, "double tap to insert");

            TapGestureRecognizer tapgesture = new TapGestureRecognizer();
            tapgesture.Tapped += tapParola;
            this.GestureRecognizers.Add(tapgesture);

        }

        private async void tapParola(object sender, EventArgs e)
        {
            Debug.WriteLine($"DEBUG: parola cliccata = {this.Text}");

            if(canBeSelected)
            {
                isSelected = true;
                pagina.emptyLabelCTS.Text = this.parola;
                pagina.emptyLabelCTS.BackgroundColor = Color.FromHex("#fca128");
                accessSpeak.AccSpeak(this, $"{this.Text} inserted");
                this.Text = "";
                this.BackgroundColor = Color.Gray;
                //AutomationProperties.SetName(this, "empty box");
                //AutomationProperties.SetHelpText(this, "");
                //AutomationProperties.SetName(pagina.emptyLabelCTS, " ");
                //AutomationProperties.SetHelpText(pagina.emptyLabelCTS, "double tap to remove");
                accessibleName.setAccessName(this, "empty box");
                accessibleName.setAccessHint(this, "");
                accessibleName.setAccessName(pagina.emptyLabelCTS, this.parola);
                accessibleName.setAccessHint(pagina.emptyLabelCTS, "double tap to remove");

                //await Task.Delay(1000);
                changeFocusService.ChangeFocus(ExercisePage.firstLabelND);
                


                foreach(DragBox d in pagina.risposteCTS)
                {
                    d.canBeSelected = false;
                }
            }
        }
    }
}
