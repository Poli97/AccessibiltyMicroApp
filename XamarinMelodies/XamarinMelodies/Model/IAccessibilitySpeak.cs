using System;
using Xamarin.Forms;

namespace XamarinMelodies.Model
{
    public interface IAccessibilitySpeak
    {
        void AccSpeak(View v, string s);
    }
}
