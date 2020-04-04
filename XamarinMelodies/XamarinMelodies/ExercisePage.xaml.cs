using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinMelodies.Model;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace XamarinMelodies
{
    public partial class ExercisePage : ContentPage
    {
        ExerciseModel esercizio;    //oggetto esercizio
        string codeEs;

        public static AbsoluteLayout containerEs;   //container esercizio

        public IChangeAccessibilityFocus changeFocusService;  //dependency service per il cambio di focus
        ImageButton backButton;
        public IAccessibilitySpeak accessSpeak;
        public IAccessibleName accessibleName;

        Random rnd = new Random();
        int difficultyEs;
        public static ISimpleAudioPlayer dogPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public static ISimpleAudioPlayer catPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer wrongAnswer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer correctAnswer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public static ISimpleAudioPlayer popSound1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer popSound2 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer eraser = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();


        public static int isToggleButton = 2;  //per controllare se è esercizio IOD per Custom Renderer
        public static ToggleButton[] toggleButtons;     //mi serve per cambiare attributo Animal quando attivo o disattivo

        int randomES;
        public double screen_width = DeviceDisplay.MainDisplayInfo.Width;
        public double screen_height = DeviceDisplay.MainDisplayInfo.Height;

        double dimAnimalsCC;
        double dogsCC;
        Button moreDogCC;
        Button moreCatCC;
        Button equalNCC;

        int randomDogsIOD;
        List<ToggleButton> dogButtonsIOD = new List<ToggleButton>();

        Label titleView;
        public ExercisePage(int id, int difficulty)     //passo l'id come parametro del costruttore pagina
        {
            InitializeComponent();
            this.BackgroundImageSource = ImageSource.FromResource("XamarinMelodies.Immagini.bg1.jpg");

            int passato2 = id;
            difficultyEs = difficulty;
            Debug.WriteLine($"Passato: {passato2}");

            changeFocusService = DependencyService.Get<IChangeAccessibilityFocus>(DependencyFetchTarget.NewInstance);
            accessSpeak = DependencyService.Get<IAccessibilitySpeak>(DependencyFetchTarget.NewInstance);
            accessibleName = DependencyService.Get<IAccessibleName>(DependencyFetchTarget.NewInstance);


            esercizio = Modell.globalEsercizi.exercises[id];
            codeEs = esercizio.code;

            NavigationPage.SetHasNavigationBar(this, false);

            //CREO LA NAVBAR PERSONALIZZATA
            NavigationPage.SetHasBackButton(this, false);
            var navBarView = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = MainPage.navBarColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 55,
                Spacing = 10,
                Padding = new Thickness(20, 5, 0, 2),
            };
            backButton = new ImageButton
            {
                Source = MainPage.navImage,
                BackgroundColor = Color.Transparent,
                HeightRequest = 35,
                TabIndex = 0,
            };
            backButton.Clicked += goBackList;
            navBarView.Children.Add(backButton);
            AutomationProperties.SetIsInAccessibleTree(backButton, true);
            AutomationProperties.SetName(backButton, "back");
            titleView = new Label
            {
                Text = esercizio.description.ToUpper(),
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = MainPage.navColor,
                TabIndex = 0,
            };
            AutomationProperties.SetIsInAccessibleTree(titleView, true);
            //AutomationProperties.SetName(titleView, titleView.Text);
            navBarView.Children.Add(titleView);
            //NavigationPage.SetTitleView(this, navBarView);

            AbsoluteLayout.SetLayoutBounds(navBarView, new Rectangle(0, 0, 1, 0.15));
            AbsoluteLayout.SetLayoutFlags(navBarView, AbsoluteLayoutFlags.All);
            screen.Children.Add(navBarView);

            Debug.WriteLine($"DEBUG: Width = {screen_width}");

            containerEs = new AbsoluteLayout();
            screen.Children.Add(containerEs);

            AbsoluteLayout.SetLayoutBounds(containerEs, new Rectangle(0.5, 0.43, 0.9, 0.63));
            AbsoluteLayout.SetLayoutFlags(containerEs, AbsoluteLayoutFlags.All);
            containerEs.BackgroundColor = Color.GreenYellow.MultiplyAlpha(0.35);
            Debug.WriteLine($"DEBUG: containerEs = {containerEs.Width}, {containerEs.MinimumWidthRequest}, {containerEs.WidthRequest}");

            /*DescritpionLabel.Text = esercizio.description.ToUpper();
            AbsoluteLayout.SetLayoutBounds(DescritpionLabel, new Rectangle(0.5, 0.05, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(DescritpionLabel, AbsoluteLayoutFlags.All);
            DescritpionLabel.BackgroundColor = Color.Azure;
            AutomationProperties.SetIsInAccessibleTree(DescritpionLabel, true);*/

            /*AbsoluteLayout.SetLayoutBounds(backButton, new Rectangle(0.05, 0.95, 0.15, 0.15));
            AbsoluteLayout.SetLayoutFlags(backButton, AbsoluteLayoutFlags.All);
            backButton.Source = ImageSource.FromResource("XamarinMelodies.Immagini.back.jpg");
            backButton.BackgroundColor = Color.Transparent;
            backButton.Clicked += goBackList;
            AutomationProperties.SetName(backButton, "back");
            backButton.TabIndex = 35;*/
            doneButton.TabIndex = 40;

            AbsoluteLayout.SetLayoutBounds(doneButton, new Rectangle(0.5, 0.97, 0.30, 0.14));
            AbsoluteLayout.SetLayoutFlags(doneButton, AbsoluteLayoutFlags.All);

            //carico i vari suoni
            dogPlayer.Load(GetStreamFromFile("dogsound.mp3"));
            catPlayer.Load(GetStreamFromFile("catsound.mp3"));
            wrongAnswer.Load(GetStreamFromFile("wrong_answer.mp3"));
            correctAnswer.Load(GetStreamFromFile("correct_answer.mp3"));
            popSound1.Load(GetStreamFromFile("blop.mp3"));
            popSound2.Load(GetStreamFromFile("blop2.mp3"));
            eraser.Load(GetStreamFromFile("eraser.mp3"));

            switch (codeEs)
            {
                case "cc":
                    Debug.WriteLine("Cliccato es CC");
                    isToggleButton = 0;
                    comparedCounting();
                    break;
                case "iod":
                    Debug.WriteLine("Cliccato es IOD");
                    isToggleButton = 1;
                    insertOrDelete();
                    break;
                case "cts":
                    Debug.WriteLine("Cliccato es CTS");
                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        completeTheSentenceDnD();
                    }
                    else
                    {
                        completeTheSentence();

                    }

                    break;
                case "co":
                    Debug.WriteLine("Cliccato es CO");
                    columnOperations();
                    break;
                default:
                    Console.WriteLine("Error passing es");
                    break;
            }


        }

        //FUNZIONE CC COMPARED COUNTING
        public void comparedCounting()
        {
            Debug.WriteLine("DEBUG: codice creazione CC");
            doneButton.IsVisible = false;

            //bottone risposta DOG
            moreDogCC = new Button
            {
                BackgroundColor = Color.LightGreen,
                Text = "DOG",
                TabIndex = 40,
                BorderColor = Color.Black,
                BorderWidth = 2,
            };
            moreDogCC.Clicked += checkCC;
            screen.Children.Add(moreDogCC);
            AbsoluteLayout.SetLayoutBounds(moreDogCC, new Rectangle(0.1, 0.95, 0.225, 0.14));
            AbsoluteLayout.SetLayoutFlags(moreDogCC, AbsoluteLayoutFlags.All);

            //bottone rispsota CAT
            moreCatCC = new Button
            {
                BackgroundColor = Color.LightGreen,
                Text = "CAT",
                TabIndex = 41,
                BorderColor = Color.Black,
                BorderWidth = 2,
            };
            moreCatCC.Clicked += checkCC;
            screen.Children.Add(moreCatCC);
            AbsoluteLayout.SetLayoutBounds(moreCatCC, new Rectangle(0.50, 0.95, 0.225, 0.14));
            AbsoluteLayout.SetLayoutFlags(moreCatCC, AbsoluteLayoutFlags.All);

            //bottone risposta EQUAL
            equalNCC = new Button
            {
                BackgroundColor = Color.LightGreen,
                Text = "EQUAL NUMBER",
                TabIndex = 42,
                BorderColor = Color.Black,
                BorderWidth = 2,
            };
            equalNCC.Clicked += checkCC;
            screen.Children.Add(equalNCC);
            AbsoluteLayout.SetLayoutBounds(equalNCC, new Rectangle(0.9, 0.95, 0.225, 0.14));
            AbsoluteLayout.SetLayoutFlags(equalNCC, AbsoluteLayoutFlags.All);


            //creo l'array di frame degli animali da stampare e lo randomizzo
            dimAnimalsCC = difficultyEs;
            IAccessibleImage[] arrayAnimals = generateArrayAnimals((int)dimAnimalsCC);
            IAccessibleImage[] randomAnimals = arrayAnimals.OrderBy(x => rnd.Next()).ToArray();

            //stampo i frame a schermo
            double x_coord = 0;
            double y_coord = 0;
            int j = 0;
            int index = 5;
            foreach (IAccessibleImage f in randomAnimals)
            {
                //creo i frame per i contorni in cui inserire le accessibleimages
                Frame frame = new Frame
                {
                    Content = f,
                    BorderColor = Color.Green,
                    BackgroundColor = Color.FromHex("dbae27"),
                    Padding = 3,
                };



                AbsoluteLayout.SetLayoutBounds(frame, new Rectangle(x_coord, y_coord, 0.20, 0.33));
                AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.All);
                f.TabIndex = index + 1;
                containerEs.Children.Add(frame);

                j++;
                if (j == 5)
                {
                    x_coord = 0;
                    y_coord += 0.495;
                    j = 0;
                }
                else
                {
                    x_coord += 0.25;
                }
                index++;
            }
        }


        //funzione CC per creare l'array degli animali
        public IAccessibleImage[] generateArrayAnimals(int maxA)
        {
            int dogs = rnd.Next(1, maxA);
            dogsCC = dogs;
            int cats = maxA - dogs;
            Debug.WriteLine($"DEBUG: cani: {dogs}, gatti:{cats}");

            //creo i tapgesture per riprodurre i versi
            TapGestureRecognizer tapDog = new TapGestureRecognizer();
            tapDog.Tapped += dogTapped;

            TapGestureRecognizer tapCat = new TapGestureRecognizer();
            tapCat.Tapped += catTapped;

            IAccessibleImage[] animals = new IAccessibleImage[maxA];

            //INSERISCO I CANI
            for (int i = 0; i < dogs; i++)
            {
                /*Frame f = new Frame
                {
                    BorderColor = Color.Blue,
                    Padding = 4,
                    Content = new Image
                    {
                        Source = ImageSource.FromResource("XamarinMelodies.Immagini.dog.png"),
                        Aspect = Aspect.AspectFit,
                    },

                };*/

                IAccessibleImage f = new IAccessibleImage("dog")
                {
                    Source = ImageSource.FromResource("XamarinMelodies.Immagini.dog.png"),
                    Aspect = Aspect.AspectFit,
                };

                AutomationProperties.SetIsInAccessibleTree(f, true);
                AutomationProperties.SetName(f, "\0");

                f.GestureRecognizers.Add(tapDog);

                animals[i] = f;

            }

            //INSERISCO I GATTI
            for (int i = dogs; i < maxA; i++)
            {
                /*Frame f = new Frame
                {
                    BorderColor = Color.Blue,
                    Padding = 4,
                    Content = new Image
                    {
                        Source = ImageSource.FromResource("XamarinMelodies.Immagini.cat.png"),
                        Aspect = Aspect.AspectFit,
                    },

                };*/

                IAccessibleImage f = new IAccessibleImage("cat")
                {
                    Source = ImageSource.FromResource("XamarinMelodies.Immagini.cat.png"),
                    Aspect = Aspect.AspectFit,
                };

                AutomationProperties.SetIsInAccessibleTree(f, true);
                AutomationProperties.SetName(f, "\0");

                f.GestureRecognizers.Add(tapCat);

                animals[i] = f;

            }

            return animals;
        }

        //metodi che mi rirpoducono i versi degli animali al tap della figura
        private void dogTapped(object sender, EventArgs e)
        {
            dogPlayer.Play();
        }

        private void catTapped(object sender, EventArgs e)
        {
            catPlayer.Play();
        }

        //funzione CC per controllare la risposta data.
        private async void checkCC(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Debug.WriteLine($"DEBUG: cliccato bottone con index: {b.TabIndex}, e testo: {b.Text} ci sono {dogsCC} cani su {dimAnimalsCC}");

            switch (b.Text)
            {
                case "DOG": //MORE DOGS
                    if(dogsCC > dimAnimalsCC / 2)
                    {
                        Debug.WriteLine("RISPOSTA CORRETTA");
                        showPopupCorrect(b, EventArgs.Empty);
                    }
                    else
                    {
                        Debug.WriteLine("RISPOSTA ERRATA");
                        showPopupWrong();
                    }
                    break;
                case "CAT": //MORE CATS
                    if (dogsCC < dimAnimalsCC / 2)
                    {
                        Debug.WriteLine("RISPOSTA CORRETTA");
                        showPopupCorrect(b, EventArgs.Empty);
                    }
                    else
                    {
                        Debug.WriteLine("RISPOSTA ERRATA");
                        showPopupWrong();
                    }
                    break;
                case "EQUAL NUMBER": //EQUAL NUMBER
                    if (dogsCC == dimAnimalsCC / 2)
                    {
                        Debug.WriteLine("RISPOSTA CORRETTA");
                        showPopupCorrect(b, EventArgs.Empty);
                    }
                    else
                    {
                        Debug.WriteLine("RISPOSTA ERRATA");
                        showPopupWrong();
                    }
                    break;
            }
            
        }



//FUNZIONE IOD INSERT OR DELETE
        public void insertOrDelete()
        {
            Debug.WriteLine("DEBUG: codice creazione IOD");
            randomDogsIOD = rnd.Next(2,difficultyEs);
            toggleButtons = new ToggleButton[difficultyEs];

            titleView.Text = titleView.Text + " " + randomDogsIOD + " dogs";
            //AutomationProperties.SetName(titleView, titleView.Text);

            int cont = difficultyEs;
            double x_coord = 0;
            double y_coord = 0;

            int j = 0;

            for (int i = 0; i < cont; i++)
            {
                bool t;
                string a;
                if(rnd.NextDouble() < 0.5)
                {
                    t = true;
                    a = "dog";
                }
                else
                {
                    t = false;
                    a = "nil";
                }

                ToggleButton b = new ToggleButton(index: (i), toggled: t, animal: a);
                toggleButtons[i] = b;

                Frame frame = new Frame
                {
                    Content = b,
                    BorderColor = Color.Green,
                    BackgroundColor = Color.FromHex("dbae27"),
                    Padding = 3,
                };

                AbsoluteLayout.SetLayoutBounds(frame, new Rectangle(x_coord, y_coord, 0.25, 0.33));
                AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.All);

                containerEs.Children.Add(frame);

                j++;
                if (j == 4)
                {
                    x_coord = 0;
                    y_coord += 0.495;
                    j = 0;
                }
                else
                {
                    x_coord += 0.33;
                }

                dogButtonsIOD.Add(b);
            }

            doneButton.Clicked += checkIOD;
        }

        public async void checkIOD(object sender, EventArgs e)
        {
            Debug.WriteLine("DEBUG: cliccato DONE IOD");
            int cont = 0;
            foreach(ToggleButton t in dogButtonsIOD)
            {
                if(t.isToggled == true)
                {
                    cont++;
                }
            }
            Debug.WriteLine($"DEBUG: cani presenti: {cont}");

            if(cont == randomDogsIOD)
            {
                showPopupCorrect(sender, e);
            }
            else
            {
                showPopupWrong();
            }
        }



        int randomCTS;
        public Label emptyLabelCTS;
        public static Label firstLabelND;
        public List<DragBox> risposteCTS = new List<DragBox>();

//FUNZIONE CTS COMPLETE THE SENTENCE PER ANDROID
        public void completeTheSentence()
        {
            randomCTS = rnd.Next(0, esercizio.ctsex.Length);

            StackLayout boxFrase = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#009e2a").MultiplyAlpha(0.90),
                HorizontalOptions = LayoutOptions.Center,
                Padding = 5,
            };

            AbsoluteLayout.SetLayoutBounds(boxFrase, new Rectangle(0, 0.35, 1, 0.30));
            AbsoluteLayout.SetLayoutFlags(boxFrase, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(boxFrase);

            firstLabelND = new Label
            {
                Text = esercizio.ctsex[randomCTS].first_sentence,
                //BackgroundColor = Color.Yellow,
                VerticalOptions = LayoutOptions.Center,
                TabIndex = 10,
            };
            AutomationProperties.SetIsInAccessibleTree(firstLabelND, true);
            boxFrase.Children.Add(firstLabelND);

            TapGestureRecognizer tapgesture = new TapGestureRecognizer();
            tapgesture.Tapped += clickEmptyBox;

            emptyLabelCTS = new Label
            {
                BackgroundColor = Color.Gray,
                WidthRequest = 150,
                HeightRequest = screen_height * 0.22,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TabIndex = 15,
            };

            AutomationProperties.SetIsInAccessibleTree(emptyLabelCTS, true);
            //AutomationProperties.SetHelpText(emptyLabelCTS, "empty box");
            
            boxFrase.Children.Add(emptyLabelCTS);
            emptyLabelCTS.GestureRecognizers.Add(tapgesture);

            Label secondLabel = new Label
            {
                Text = esercizio.ctsex[randomCTS].second_sentence,
                //BackgroundColor = Color.Yellow,
                VerticalOptions = LayoutOptions.Center,
                TabIndex = 20
            };
            AutomationProperties.SetIsInAccessibleTree(secondLabel, true);
            boxFrase.Children.Add(secondLabel);

            StackLayout boxRisposte = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                //BackgroundColor = Color.Orange,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 15,
            };
            AbsoluteLayout.SetLayoutBounds(boxRisposte, new Rectangle(0, 1, 1, 0.30));
            AbsoluteLayout.SetLayoutFlags(boxRisposte, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(boxRisposte);

            string[] risposte = esercizio.ctsex[randomCTS].words.Keys.ToArray();
            risposte = risposte.OrderBy(x => rnd.Next()).ToArray();
            //inserisco i box risposta
            for (int i = 0; i < 4; i++)
            {
                DragBox risposta = new DragBox(this, risposte[i]);
                risposta.TabIndex = 25 + i;
                boxRisposte.Children.Add(risposta);
                risposteCTS.Add(risposta);
            }

            doneButton.Clicked += checkCTS;
        }

        //funzione che rimuove parola inserita
        private async void clickEmptyBox(object sender, EventArgs e)
        {
            Debug.WriteLine("DEBUG: cliccato emptyBox");

            Label box = sender as Label;
            if(box.BackgroundColor != Color.Gray)
            {
                foreach(DragBox d in risposteCTS)
                {
                    d.canBeSelected = true;
                    if (d.isSelected)
                    {
                        d.isSelected = false;
                        d.Text = box.Text;
                        accessSpeak.AccSpeak(box, $"{emptyLabelCTS.Text} removed");
                        d.BackgroundColor = Color.FromHex("#fca128");
                        emptyLabelCTS.Text = "";
                        emptyLabelCTS.BackgroundColor = Color.Gray;
                        //AutomationProperties.SetName(d, "");
                        //AutomationProperties.SetName(emptyLabelCTS, "empty box");
                        //AutomationProperties.SetHelpText(emptyLabelCTS, "");
                        accessibleName.setAccessName(d, "");
                        accessibleName.setAccessName(emptyLabelCTS, "empty box");
                        accessibleName.setAccessHint(emptyLabelCTS, "");

                        await Task.Delay(1000);
                        changeFocusService.ChangeFocus(firstLabelND);
                        
                    }
                }
            }
        }

        //funzione controllo correttezza es CTS
        private void checkCTS(object sender, EventArgs e)
        {
            if(emptyLabelCTS.BackgroundColor != Color.Gray)
            {
                if (esercizio.ctsex[randomCTS].words[$"{emptyLabelCTS.Text}"] == true)
                {
                    showPopupCorrect(sender: sender, EventArgs.Empty);
                }
                else
                {
                    showPopupWrong();
                }
            }
            else
            {
                showPopupEmpty();
            }

        }



//FUNZIONE COMPLETE THE SENTENCE CON DRAG AND DROP PER IOS
        int randomCTSDD;
        public static Label emptyLabelCTSDD;
        public static List<IDragLabel> risposteCTSDD;
        public static List<BoxView> initcoord;
        public static bool boxIsEmpty;
        public static bool[] isDragged;
        public static bool CTSopened = false;
        public static Label firstLabel;
        Label secondLabel;

        private void completeTheSentenceDnD()
        {
            randomCTSDD = rnd.Next(0, esercizio.ctsex.Length);
            CTSopened = true;
            isDragged = new bool[4];
            boxIsEmpty = true;
            initcoord = new List<BoxView>();
            risposteCTSDD = new List<IDragLabel>();


            StackLayout boxFrase = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#009e2a").MultiplyAlpha(0.40),
                HorizontalOptions = LayoutOptions.Center,
                Padding = 5,
            };
            AutomationProperties.SetIsInAccessibleTree(boxFrase, true);
            AbsoluteLayout.SetLayoutBounds(boxFrase, new Rectangle(0, 0.35, 1, 0.20));
            AbsoluteLayout.SetLayoutFlags(boxFrase, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(boxFrase);

            firstLabel = new Label
            {
                Text = esercizio.ctsex[randomCTSDD].first_sentence,
                //BackgroundColor = Color.Yellow,
                VerticalOptions = LayoutOptions.Center,
                TabIndex = 10,
            };
            AutomationProperties.SetIsInAccessibleTree(firstLabel, true);
            boxFrase.Children.Add(firstLabel);

            emptyLabelCTSDD = new Label
            {
                BackgroundColor = Color.Gray,
                WidthRequest = 150,
                HeightRequest = 250,
                TabIndex = 11,
                
            };
            //emptyLabelCTSDD.SizeChanged += emptylabesizechanged;

            AutomationProperties.SetIsInAccessibleTree(emptyLabelCTSDD, true);
            AutomationProperties.SetName(emptyLabelCTSDD, "empty box");
            boxFrase.Children.Add(emptyLabelCTSDD);

            secondLabel = new Label
            {
                Text = esercizio.ctsex[randomCTSDD].second_sentence,
                //BackgroundColor = Color.Yellow,
                VerticalOptions = LayoutOptions.Center,
                TabIndex = 12,
            };
            AutomationProperties.SetIsInAccessibleTree(secondLabel, true);
            boxFrase.Children.Add(secondLabel);

            StackLayout boxRisposte = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                //BackgroundColor = Color.Orange,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 15,
            };
            AutomationProperties.SetIsInAccessibleTree(boxRisposte, true);
            AbsoluteLayout.SetLayoutBounds(boxRisposte, new Rectangle(0, 1, 1, 0.30));
            AbsoluteLayout.SetLayoutFlags(boxRisposte, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(boxRisposte);

            string[] risposte = esercizio.ctsex[randomCTSDD].words.Keys.ToArray();
            risposte = risposte.OrderBy(x => rnd.Next()).ToArray();
            //inserisco i box risposta

            double cX = 0;
            for (int i = 0; i < 4; i++)
            {
                BoxView graybox = new BoxView
                {
                    BackgroundColor = Color.Gray,
                };

                AbsoluteLayout.SetLayoutBounds(graybox, new Rectangle(cX, 1, 0.2, 0.2));
                AbsoluteLayout.SetLayoutFlags(graybox, AbsoluteLayoutFlags.All);
                containerEs.Children.Add(graybox);
                initcoord.Add(graybox);

                IDragLabel risposta = new IDragLabel(risposte[i], this, i);
                risposta.BackgroundColor = Color.FromHex("#f5f587");
                risposta.Text = risposte[i];
                risposta.HorizontalTextAlignment = TextAlignment.Center;
                risposta.VerticalTextAlignment = TextAlignment.Center;
                AbsoluteLayout.SetLayoutBounds(risposta, new Rectangle(cX, 1, 0.2, 0.20));
                AbsoluteLayout.SetLayoutFlags(risposta, AbsoluteLayoutFlags.All);
                AutomationProperties.SetIsInAccessibleTree(risposta, true);
                containerEs.Children.Add(risposta);
                risposteCTSDD.Add(risposta);

                risposta.TabIndex = 15 + i;

                cX += 0.325;
            }

            doneButton.Clicked += checkCTSDD;
        }

        private void checkCTSDD(object sender, EventArgs e)
        {
            string sel = " ";
            int i = 0;
            foreach(bool b in isDragged)
            {
                if (b)
                {
                    sel = risposteCTSDD[i].Text;
                    
                }
                i++;
            }

            if(sel != " ")
            {
                if (esercizio.ctsex[randomCTSDD].words[sel] == true)
                {
                    Debug.WriteLine("DEBUG: CTS corretto");
                    showPopupCorrect(sender: sender, EventArgs.Empty);
                }
                else
                {
                    showPopupWrong();
                }
            }
            else
            {
                showPopupEmpty();
            }


        }

        public Label clickedBoxCO = null;
        public string signCO;
        public string stringn1CO;
        public string stringrisCO;
        public int[] numericN1CO;
        public int[] numericRisCO;
        public int operando1;
        public int risultato;
        public Button bottone1;

        public static AbsoluteLayout keyboardCO;
        List<Label> insertCO;
        int boxClickedCO;     //mi serve per salvare l'indice del box risposta premuto per poter cambiare il focus dopo l'inserimento

//FUNZIONE CO COLUMN OPERATIONS
        public void columnOperations()
        {
            Debug.WriteLine("DEBUG: codice creazione CO");
            signCO = "+";
            insertCO = new List<Label>();
            stringn1CO = "";
            stringrisCO = "";

            int n1 = rnd.Next(0, difficultyEs);
            stringn1CO = n1.ToString();
            Debug.WriteLine($"DEBUG: numero 1 = {stringn1CO}");
            /*if(n1 < 100)
            {
                stringn1CO = 0 + stringn1CO;
                if(n1 < 10)
                {
                    stringn1CO = 0 + stringn1CO;
                }
            }*/
            numericN1CO = stringn1CO.ToCharArray().Select(r => (int)Char.GetNumericValue(r)).ToArray();

            int n2 = rnd.Next(n1, difficultyEs);
            stringrisCO = n2.ToString();
            Debug.WriteLine($"DEBUG: numero 2 = {stringrisCO}");
            /*if (n2 < 100)
            {
                stringrisCO = 0 + stringrisCO;
                if(n2 < 10)
                {
                    stringrisCO = 0 + stringrisCO;
                }
            }*/
            numericRisCO = stringrisCO.ToCharArray().Select(s => (int)Char.GetNumericValue(s)).ToArray();
            operando1 = n1;
            risultato = n2;

            titleView.Text = $"How much must be added to {n1} to get {n2}";
            if(Device.RuntimePlatform == Device.iOS)
            {
                AutomationProperties.SetName(titleView, titleView.Text);
            }


            //creo la vista per la tastiera
            keyboardCO = new AbsoluteLayout();
            keyboardCO.IsVisible = false;
            keyboardCO.BackgroundColor = Color.LightBlue;
            AbsoluteLayout.SetLayoutBounds(keyboardCO, new Rectangle(0, 1, 1, 0.25));
            AbsoluteLayout.SetLayoutFlags(keyboardCO, AbsoluteLayoutFlags.All);
            AutomationProperties.SetIsInAccessibleTree(keyboardCO, true);
            containerEs.Children.Add(keyboardCO);

            //stampo la prima linea con i numeri
            stampaFrameCO(arrayN: numericN1CO, row: 0.01);

            //inserisco il frame con il segno
            Label segno = new Label
            {
                Text = signCO,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.LightSkyBlue,
                TabIndex = 10,
            };
            AutomationProperties.SetIsInAccessibleTree(segno, true);
            AbsoluteLayout.SetLayoutBounds(segno, new Rectangle(0.85, 0.01, 0.2, 0.2));
            AbsoluteLayout.SetLayoutFlags(segno, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(segno);

            //inserisco frame con uguale
            Label uguale = new Label
            {
                Text = "=",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.LightSkyBlue,
                TabIndex = 15,
            };
            AutomationProperties.SetIsInAccessibleTree(uguale, true);
            AbsoluteLayout.SetLayoutBounds(uguale, new Rectangle(0.85, 0.32, 0.2, 0.2));
            AbsoluteLayout.SetLayoutFlags(uguale, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(uguale);

            //stampo la seconda linea con i box in cui inserire i numeri
            double xx = 0.1;
            int nBoxDaInserire = 3;
            if(risultato - operando1 < 10)
            {
                nBoxDaInserire = 1;
                xx = 0.60;
            }
            else
            {
                if(risultato - operando1 < 100)
                {
                    nBoxDaInserire = 2;
                    xx = 0.35;
                }
            }
        
            for(int i = 0; i < nBoxDaInserire; i++)
            {
                Label ib = new Label();
                ib.Text = "";
                ib.BackgroundColor = Color.White;
                ib.BackgroundColor = Color.ForestGreen;
                ib.TabIndex = 10 + i;
                ib.HorizontalTextAlignment = TextAlignment.Center;
                ib.VerticalTextAlignment = TextAlignment.Center;
                TapGestureRecognizer tapped = new TapGestureRecognizer();
                ib.GestureRecognizers.Add(tapped);
                tapped.Tapped += openKeyboardCO;

                //ib.Clicked += openKeyboardCO;
                AutomationProperties.SetIsInAccessibleTree(ib, true);
                if(Device.RuntimePlatform == Device.iOS)
                {
                    AutomationProperties.SetName(ib, "empty box");
                    AutomationProperties.SetHelpText(ib, "double tap to insert");
                }

                AbsoluteLayout.SetLayoutBounds(ib, new Rectangle(xx, 0.32, 0.2, 0.2));
                AbsoluteLayout.SetLayoutFlags(ib, AbsoluteLayoutFlags.All);

                insertCO.Add(ib);
                containerEs.Children.Add(insertCO[i]);
                xx += 0.25;
            }

            //stampo la linea di separazione del risultato
            BoxView linea = new BoxView
            {
                BackgroundColor = Color.Black,
            };
            AbsoluteLayout.SetLayoutBounds(linea, new Rectangle(0.12, 0.50, 0.9, 0.01));
            AbsoluteLayout.SetLayoutFlags(linea, AbsoluteLayoutFlags.All);
            containerEs.Children.Add(linea);

            //stampo la terza linea con il risultato
            stampaFrameCO(arrayN:numericRisCO, row: 0.66);

            //creo l'array dei bottoni numerici per la keyboard
            List<Button> numeri = new List<Button>();
            for (int i = 0; i < 12; i++)
            {
                string n = "" + i;
                if (i == 10)
                {
                    n = "C";
                }
                if (i == 11)
                {
                    n = "X";
                }

                Button b = new Button
                {
                    BackgroundColor = Color.LightGoldenrodYellow,
                    BorderColor = Color.Brown,
                    BorderWidth = 1,
                    Padding = 1,
                    Text = n,
                    TabIndex = 20 + i,
                };

                if(b.Text == "C")
                {
                    AutomationProperties.SetName(b, "Delete");
                }
                if(b.Text == "X")
                {
                    AutomationProperties.SetName(b, "Close");
                }
                if(b.Text == "0")
                {
                    bottone1 = b;
                    AutomationProperties.SetName(b, "0");
                    AutomationProperties.SetHelpText(b, "First number of the keypad");
                }

                AutomationProperties.SetIsInAccessibleTree(b, true);
                b.IsEnabled = true;
                b.Clicked += setNumber;
                numeri.Add(b);
            }

            double x = 0, y = 0;
            int cont = 0;
            foreach(Button f in numeri)
            {
                AbsoluteLayout.SetLayoutBounds(f, new Rectangle(x, y, 0.16, 0.5));
                AbsoluteLayout.SetLayoutFlags(f, AbsoluteLayoutFlags.All);
                AutomationProperties.SetIsInAccessibleTree(f, true);
                keyboardCO.Children.Add(f);

                if(cont == 5)
                {
                    x = 0;
                    y = 1;
                }
                else
                {
                    x += 0.2;
                }
                cont++;
            }

            doneButton.Clicked += checkCO;

        }

        //funzione apertura tastiera
        private void openKeyboardCO(object sender, EventArgs e)
        {
            if(clickedBoxCO != null)
            {
                clickedBoxCO.BackgroundColor = Color.ForestGreen;
                clickedBoxCO = null;
            }

            Label b = sender as Label;
            Debug.WriteLine("DEBUG: aperta keyboard");
            b.BackgroundColor = Color.DarkGreen;
            keyboardCO.IsVisible = true;
            keyboardCO.IsEnabled = true;
            changeFocusService.ChangeFocus(bottone1);
            popSound1.Play();

            if (clickedBoxCO != null)
            {
                clickedBoxCO.BackgroundColor = Color.ForestGreen;
            }

            clickedBoxCO = b;
        }

        //funzione inserimento numeri da tastiera
        private void setNumber(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Debug.WriteLine($"DEBUG: cliccato {b.Text}");

            switch (b.Text)
            {
                case "C":
                    clickedBoxCO.Text = "";
                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        AutomationProperties.SetName(clickedBoxCO, "empty box");
                        AutomationProperties.SetHelpText(clickedBoxCO, "double tap to insert");
                    }
                    else
                    {
                        accessibleName.setAccessName(clickedBoxCO, "empty box");
                        accessibleName.setAccessHint(clickedBoxCO, "double tap to insert");
                    }
                    
                    eraser.Play();
                    break;
                case "X":
                    keyboardCO.IsVisible = false;
                    keyboardCO.IsEnabled = false;
                    clickedBoxCO.BackgroundColor = Color.ForestGreen;
                    clickedBoxCO = null;
                    popSound2.Play();
                    changeFocusService.ChangeFocus(backButton);
                    break;
                default:
                    clickedBoxCO.Text = b.Text;
                    clickedBoxCO.BackgroundColor = Color.ForestGreen;
                    keyboardCO.IsVisible = false;
                    keyboardCO.IsEnabled = false;

                    //AutomationProperties.SetName(insertCO[insertCO.IndexOf(clickedBoxCO)], " ");
                    //accessibleName.setAccessName(insertCO[insertCO.IndexOf(clickedBoxCO)], " ");
                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        AutomationProperties.SetName(insertCO[insertCO.IndexOf(clickedBoxCO)], $"{b.Text}");
                        AutomationProperties.SetHelpText(insertCO[insertCO.IndexOf(clickedBoxCO)], "double tap to change");
                    }
                    else
                    {
                        accessibleName.setAccessName(insertCO[insertCO.IndexOf(clickedBoxCO)], $"{b.Text}");
                        accessibleName.setAccessHint(insertCO[insertCO.IndexOf(clickedBoxCO)], "double tap to change");
                    }

                    changeFocusService.ChangeFocus(insertCO[insertCO.IndexOf(clickedBoxCO)]);

                    clickedBoxCO = null;
                    popSound2.Play();

                    break;
            }

        }

        //funzione stampa frame CO
        public void stampaFrameCO(int[] arrayN, double row)
        {
            Debug.WriteLine($"DEBUG: array passato: {arrayN.ToString()}");
            double x = 0.60;
            int jj;
            int index = 9;
            if(row == 0.66)
            {
                index = 19;
            }
            for (int i = 0; i < arrayN.Length; i++)
            {
                jj = (arrayN.Length - 1) - i;
                int n;
                //if (i > 0)
                //{
                    n = arrayN[jj];
                    Debug.WriteLine($"DEBUG: numero array = {n}");
                //}

                /*Label op1Label = new Label
                {
                    Text = "" + n,
                    TabIndex = 1,
                };*/

                Label op1 = new Label
                {
                    Text = "" + n,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Aqua,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                   
                   
                    //BorderColor = Color.Red,
                    //Padding = 4,
                    //IsEnabled = false,
                };

                /*if (i == 0)
                {
                    op1Label.Text = signCO;
                }*/
                

                AutomationProperties.SetIsInAccessibleTree(op1, true);
                //AutomationProperties.SetName(op1,$"{n}");
                AbsoluteLayout.SetLayoutBounds(op1, new Rectangle(x, row, 0.2, 0.2));
                AbsoluteLayout.SetLayoutFlags(op1, AbsoluteLayoutFlags.All);
                index -= 1;
                op1.TabIndex = index;

                containerEs.Children.Add(op1);
                Debug.WriteLine($"DEBUG: {op1.Text} : {op1.TabIndex}");
                /*if (row == 0.66 && i == 0)
                {
                    containerEs.Children.Remove(op1);
                }*/

                x -= 0.25;
            }

        }

        private async void checkCO(object sender, EventArgs e)
        {
            Debug.WriteLine("DEBUG: cliccato checkCO");
            String stringaInserita = "";
            foreach (Label b in insertCO)
            {
                string numero = b.Text;
                if (b.Text != "=")
                {
                    if(b.Text == "")
                    {
                        numero = "0";
                    }
                    stringaInserita += numero;
                }
            }
            Debug.WriteLine($"DEBUG: stringa da parsare: {stringaInserita}");
            int numeroInserito = Int32.Parse(stringaInserita);
            Debug.WriteLine($"DEBUG: numero inserito = {numeroInserito}");

            if((risultato - operando1) == numeroInserito)
            {
                showPopupCorrect(sender: sender, EventArgs.Empty);
            }
            else
            {
                showPopupWrong();
            }
        }




//FUNZIONI GENERALI UTILITY, POPUB E BOTTONI

        //da eseguire quando compaiono gli elementi a schermo
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.iOS)
            {
                changeFocusService.ChangeFocus(backButton);
            }

            if(codeEs == "cts" & Device.RuntimePlatform == Device.Android)
            {
                accessibleName.setAccessName(emptyLabelCTS, "empty box");

            }
            if(codeEs == "co" & Device.RuntimePlatform == Device.Android)
            {
                foreach (Label ib in insertCO)
                {
                    accessibleName.setAccessName(ib, "empty box");
                    accessibleName.setAccessHint(ib, "double tap to insert");
                }
            }
            
            

        }

        async void goBackList(object sender, EventArgs e)
        {
            isToggleButton = 2;     //lo metto a false, significa che non sono più nell'esercizio IOD
            Debug.Write("Cliccato BACK");
            if(codeEs == "cts" & CTSopened)
            {
                CTSopened = false;
                risposteCTSDD.Clear();
                initcoord.Clear();
                
            }
            await Navigation.PopAsync();

        }


        async void showPopupCorrect(object sender, EventArgs e)
        {
            isToggleButton = 2;     //lo metto a false, significa che non sono più nell'esercizio IOD
            correctAnswer.Play();
            await DisplayAlert("CORRECT", "", "OK");
            if (codeEs == "cts" & CTSopened)
            {
                CTSopened = false;
                risposteCTSDD.Clear();
                initcoord.Clear();

            }
            await Navigation.PopAsync();
        }

        async void showPopupWrong()
        {
            wrongAnswer.Play();
            await DisplayAlert("WRONG", "", "REPEAT");
            if (codeEs == "cts")
            {
                changeFocusService.ChangeFocus(backButton);
            }

        }

        async void showPopupEmpty()
        {
            wrongAnswer.Play();
            await DisplayAlert("EMPTY ANSWER", "", "INSERT SOMETHING");
            if(codeEs == "cts")
            {
                changeFocusService.ChangeFocus(backButton);
            }
        }

        //utility per estrarre file audio dall resource
        public static Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("XamarinMelodies.Suoni." + filename);
            return stream;
        }


    }
}
