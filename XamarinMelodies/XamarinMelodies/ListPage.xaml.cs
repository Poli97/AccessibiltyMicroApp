using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinMelodies.Model;

namespace XamarinMelodies
{
    public partial class ListPage : ContentPage
    {
        public IChangeAccessibilityFocus service;  //dependency service per il cambio di focus
        Label titleView;

        public ListPage()
        {
            InitializeComponent();
            //Title = "CHOOSE AN EXERCISE";
            var esercizi = Modell.globalEsercizi.exercises;
            sfondo.Orientation = StackOrientation.Vertical;

            service = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);

            //esercizi[1].pencil.Source = ImageSource.FromResource("XamarinMelodies.Immagini.pencil.png");

            //this.BackgroundImageSource = ImageSource.FromResource("XamarinMelodies.Immagini.bg1.jpg");
            //this.BackgroundColor = Color.PaleGreen;
            this.BackgroundColor = Color.FromHex("#ceffbf");
            NavigationPage.SetHasNavigationBar(this, false);

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            var screenHeight = mainDisplayInfo.Height;

            //CREO LA NAVBAR PERSONALIZZATA
            /*NavigationPage.SetHasBackButton(this, false);
            var navBarView = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = MainPage.navBarColor,
            };
            AbsoluteLayout.SetLayoutBounds(navBarView, new Rectangle(0.02, 0.05, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(navBarView, AbsoluteLayoutFlags.All);

            titleView = new Label
            {
                //Text = "CHOOSE AN EXERCISE",
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = MainPage.navColor,
            };
            navBarView.Children.Add(titleView);
            AutomationProperties.SetIsInAccessibleTree(titleView, true);
            //NavigationPage.SetTitleView(this, navBarView);
            sfondo.Children.Add(navBarView);*/

            StackLayout navbarview = new StackLayout
            {
                BackgroundColor = MainPage.navBarColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 55,
                Spacing = 10,
                Padding = new Thickness(20, 5, 0, 2),
            };

            titleView = new Label
            {
                TextColor = MainPage.navColor,
                Text = "CHOOSE AN EXERCISE",
                FontAttributes = FontAttributes.Bold,
                TabIndex = 0,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            AutomationProperties.SetIsInAccessibleTree(titleView, true);
            navbarview.Children.Add(titleView);

            sfondo.Children.Add(navbarview);


            var customCell = new DataTemplate(typeof(CustomCellList));
            customCell.SetBinding(CustomCellList.NameProperty, "name");

            ListView exList = new ListView();
            exList.TabIndex = 2;
            exList.ItemTemplate = customCell;
            exList.ItemsSource = esercizi;
            exList.BackgroundColor = Color.Transparent;
            exList.SelectionMode = ListViewSelectionMode.Single;
            exList.ItemTapped += onTap;
            exList.RowHeight = 60;

            sfondo.Children.Add(exList);

            /*if (Device.RuntimePlatform == Device.iOS)
            {
                AutomationProperties.SetIsInAccessibleTree(cellLabel, true);
            }*/

            /*listLabel.BackgroundColor = Color.NavajoWhite;
            listLabel.WidthRequest = screenWidth * 0.4;
            listLabel.HeightRequest = screenHeight * 0.15;
            listLabel.VerticalTextAlignment = TextAlignment.Center;
            listLabel.HorizontalTextAlignment = TextAlignment.Center;
            listLabel.FontAttributes = FontAttributes.Bold;
            AutomationProperties.SetIsInAccessibleTree(listLabel, true);*/

            //<ImageCell ImageSource="{local:ImageResource XamarinMelodies.Immagini.pencil.png}" x:Name="cella" Text="{Binding name}" TextColor="Black" />
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //service.ChangeFocus(titleView);
            }
        }

        private void onTap(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine($"Cliccato: {e.ItemIndex}");
            GoToDifficulty(e.ItemIndex, e.Item.ToString());
            ((ListView)sender).SelectedItem = null;

        }

        private async void GoToDifficulty(int n, string name)
        {
            var ExercisePage = new LevelsPage(id: n);
            await Navigation.PushAsync(ExercisePage);
        }

    }
}
