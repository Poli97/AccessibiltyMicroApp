using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class ExerciseModel
    {

        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        //public int[] difficulty_level { get; set; }
        public Dictionary<string, double> difficulty_level { get; set; }
        public CtsEx[] ctsex;

        public ImageSource pencil { get; set; } = ImageSource.FromResource("XamarinMelodies.Immagini.pencil.png");
        public ImageSource scissors { get; set; } = ImageSource.FromResource("XamarinMelodies.Immagini.scissors.png");

        public static implicit operator List<object>(ExerciseModel v)
        {
            throw new NotImplementedException();
        }
    }

    public class CtsEx
    {
        public string first_sentence;
        public string second_sentence;
        public Dictionary<string, bool> words;
    }

    public class Esercizi
    {

        public List<ExerciseModel> exercises { get; set; }

    }

    public static class Modell
    {
        public static Esercizi globalEsercizi = GetEsercizi();


        public static Esercizi GetEsercizi()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "XamarinMelodies.Model.EserciziJSON.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                Esercizi exercises = JsonConvert.DeserializeObject<Esercizi>(result);
                Console.WriteLine(exercises.exercises[0].name);
                return exercises;
            }

        }

    }
}
