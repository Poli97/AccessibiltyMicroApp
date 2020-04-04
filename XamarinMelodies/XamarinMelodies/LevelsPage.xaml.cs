using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinMelodies.Model;

namespace XamarinMelodies
{
    public partial class LevelsPage : ContentPage
    {
        int idEs;
        public IChangeAccessibilityFocus service;  //dependency service per il cambio di focus
        ImageButton backButton;

        public LevelsPage(int id)
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromHex("#ceffbf");

            //this.BackgroundImageSource = ImageSource.FromResource("XamarinMelodies.Immagini.bg1.jpg");
            NavigationPage.SetHasNavigationBar(this, false);

            idEs = id;

            service = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);

            //CREO LA NAVBAR PERSONALIZZATA
            NavigationPage.SetHasBackButton(this, false);
            StackLayout navBarView = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = MainPage.navBarColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 55,
                Spacing = 10,
                Padding = new Thickness(20,5,0,2),

            };
            AutomationProperties.SetIsInAccessibleTree(navBarView, true);
            backButton = new ImageButton
            {
                Source = MainPage.navImage,
                BackgroundColor = Color.Transparent,
                HeightRequest = 35,
                TabIndex = 0,

            };
            backButton.Clicked += goBackList;
            navBarView.Children.Add(backButton);
            AutomationProperties.SetName(backButton, "back");
            var titleView = new Label
            {
                Text = " CHOOSE THE DIFFICULTY LEVEL",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = MainPage.navColor,
                TabIndex = 0,

            };
            navBarView.Children.Add(titleView);
            AutomationProperties.SetIsInAccessibleTree(titleView, true);
            //NavigationPage.SetTitleView(this, navBarView);
            screen.Children.Add(navBarView);

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            var screenHeight = mainDisplayInfo.Height;

            /*difficultyLabel.BackgroundColor = Color.NavajoWhite;
            difficultyLabel.VerticalTextAlignment = TextAlignment.Center;
            difficultyLabel.HorizontalTextAlignment = TextAlignment.Center;
            difficultyLabel.FontAttributes = FontAttributes.Bold;
            AbsoluteLayout.SetLayoutBounds(difficultyLabel, new Rectangle(0.5, 0.0, 1, 0.2));
            AbsoluteLayout.SetLayoutFlags(difficultyLabel, AbsoluteLayoutFlags.All);
            AutomationProperties.SetIsInAccessibleTree(difficultyLabel, true);*/

            var customCell = new DataTemplate(typeof(CustomCellLevels));
            customCell.SetBinding(CustomCellLevels.NameProperty, "Key");


            var difficultyList = new ListView();
            difficultyList.TabIndex = 2;
            difficultyList.ItemTemplate = customCell;
            difficultyList.ItemsSource = Model.Modell.globalEsercizi.exercises[id].difficulty_level;
            difficultyList.BackgroundColor = Color.Transparent;
            difficultyList.SelectionMode = ListViewSelectionMode.Single;
            difficultyList.ItemTapped += onTap;
            difficultyList.RowHeight = 60;
            screen.Children.Add(difficultyList);

            //AbsoluteLayout.SetLayoutBounds(difficultyList, new Rectangle(0.5, 0.2, 1, 1));
            //AbsoluteLayout.SetLayoutFlags(difficultyList, AbsoluteLayoutFlags.All);

            /*AbsoluteLayout.SetLayoutBounds(backButton, new Rectangle(0.05, 0.95, 0.15, 0.15));
            AbsoluteLayout.SetLayoutFlags(backButton, AbsoluteLayoutFlags.All);
            backButton.Source = ImageSource.FromResource("XamarinMelodies.Immagini.back.jpg");
            backButton.BackgroundColor = Color.Transparent;
            backButton.Clicked += goBackList;
            AutomationProperties.SetName(backButton, "back");*/

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.iOS)
            {
                service.ChangeFocus(backButton);
            }
        }

        private void onTap(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine($"Cliccato: {e.ItemIndex}");
            GoToEsAsync(idEs, e.ItemIndex);
            ((ListView)sender).SelectedItem = null;

        }


        private async void GoToEsAsync(int id, int idDiff)
        {
            int difficulty = (int)Model.Modell.globalEsercizi.exercises[id].difficulty_level.ElementAt(idDiff).Value;
            var ExercisePage = new ExercisePage(id: id, difficulty: difficulty);
            await Navigation.PushAsync(ExercisePage);
        }

        async void goBackList(object sender, EventArgs e)
        {
            Debug.Write("Cliccato BACK");
            await Navigation.PopAsync();

        }

    }
}
