using System;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public class IAccessibleImage : Image
    {
        public string Animal { get; set; }
        public IAccessibleImage(string animal)
        {
            this.Animal = animal;
            
        }
    }
}
