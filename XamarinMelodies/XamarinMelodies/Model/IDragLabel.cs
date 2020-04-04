using System;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class IDragLabel : Label
    {
        public string Testo;
        public ExercisePage Pagina;
        public int Codice;

        public IDragLabel(string s, ExercisePage p, int code)
        {
            Testo = s;
            Pagina = p;
            Codice = code;
        }
    }
}
