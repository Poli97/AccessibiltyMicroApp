# Xamarin Custom Renderer overridden methods not called
Problem with Xamarin forms using custom renderer to handle native Accessibility events: overridden methods never get called.

Context (in case you are not familiar with screen-readers). Screen-readers are available on iOS (VoiceOver) and Android (TalkBack) to support accessibility to people with sever visual impairments. In a nutshell, when the screen-reader is activated and the user touches a UI elements, it gets the “accessibility focus" (the “on tap” event is not fired) and then, when the user double taps, the “on tap” event is fired. In native iOS and Android code it is possible to override methods to specify what to do when a UI element gets or loses the accessibility focus. The methods are called OnPopulateAccessibilityEvent for Android and AccessibilityElementDidBecomeFocused for iOS.
Android Java documentation: https://developer.android.com/reference/android/support/v4/view/AccessibilityDelegateCompat#onpopulateaccessibilityevent
Android C# Microsoft Xamarin documentation: https://docs.microsoft.com/en-us/dotnet/api/android.views.view.onpopulateaccessibilityevent?view=xamarin-android-sdk-9
iOS Swift documentation: https://developer.apple.com/documentation/objectivec/nsobject/1615183-accessibilityelementdidbecomefoc
iOS C# Microsoft Xamarin documentation: https://docs.microsoft.com/en-us/dotnet/api/uikit.uiresponder.accessibilityelementdidbecomefocused?view=xamarin-ios-sdk-12


Problem 1. I couldn’t find a method in Xamarin Forms to manage the accessibility focus events (when the UI element gets or loses the accessibility focus). Do you know if such methods exist in Xamarin Forms? In case they don’t, we believe that they are very important for the development of accessibile applications, so we would like to suggest to add them. Do you know which is the procedure to suggest the Xamarin team to implement a new functionality?

Problem 2. In order to overcome Problem 1, I am trying to make a Custom Renderer that uses native buttons. The custom rendered seems to be correctly initialized (the right message is logged) and shown on the screen. However, the overridden methods are not called. Am I doing something wrong? 
