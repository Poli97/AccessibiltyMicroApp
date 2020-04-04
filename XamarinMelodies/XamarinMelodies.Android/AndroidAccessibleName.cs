using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMelodies.Droid;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(AndroidAccessibleName))]
namespace XamarinMelodies.Droid
{
    public class AndroidAccessibleName : IAccessibleName
    {
        string description = " ";
        public void setAccessName(View v, string s)
        {
            v.GetRenderer().View.ContentDescription = s;
        }

        public void setAccessHint(View v, string s)
        {
            if(v.GetRenderer().View.ContentDescription != "")
            {
                description = v.GetRenderer().View.ContentDescription;
            }
            v.GetRenderer().View.ContentDescription = description + ". " + s;
        }
    }
}
