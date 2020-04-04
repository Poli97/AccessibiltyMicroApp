using System;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class CustomCellList: ViewCell
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create("name", typeof(string), typeof(CustomCellList), "name");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public Image imageCell { get; set; }
        public Label nameLabel { get; set; }
        public StackLayout background { get; set; }

        public CustomCellList()
        {
            //instantiate each of our views
            imageCell = new Image()
            {
                WidthRequest = 180,
                Source = ImageSource.FromResource("XamarinMelodies.Immagini.pencil.png"),
            };

            //StackLayout cellWrapper = new StackLayout();
            background = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(3, 0, 0, 0),
            };
            nameLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
            };

            if (Device.RuntimePlatform == Device.iOS)
            {
                AutomationProperties.SetIsInAccessibleTree(imageCell, false);
                nameLabel.TabIndex = 5;
            }

            //nameLabel.SetBinding(Label.TextProperty, new Binding("name"));

            //add views to the view hierarchy
            background.Children.Add(imageCell);
            background.Children.Add(nameLabel);
            //cellWrapper.Children.Add(background);
            View = background;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                nameLabel.Text = Name;   
            }
        }
    }
}
