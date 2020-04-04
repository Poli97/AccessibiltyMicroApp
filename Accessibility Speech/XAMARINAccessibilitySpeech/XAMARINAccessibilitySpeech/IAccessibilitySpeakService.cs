using System;
using Xamarin.Forms;

namespace XAMARINAccessibilitySpeech
{
    public interface IAccessibilitySpeakService
    {
        void speakText(View v, string s);
    }
}
