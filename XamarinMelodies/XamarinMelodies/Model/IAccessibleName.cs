using System;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public interface IAccessibleName
    {
        void setAccessName(View v, string s);
        void setAccessHint(View v, string s);
    }
}
