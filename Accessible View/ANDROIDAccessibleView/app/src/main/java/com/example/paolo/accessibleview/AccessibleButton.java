package com.example.paolo.accessibleview;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.AppCompatButton;
import android.util.Log;
import android.view.accessibility.AccessibilityEvent;
import android.widget.Button;

public class AccessibleButton extends AppCompatButton {

    public AccessibleButton(Context context) {
        super(context);
        setBackgroundColor(Color.RED);
    }

    @Override
    public void onInitializeAccessibilityEvent(AccessibilityEvent event) {
        super.onInitializeAccessibilityEvent(event);
        if (event.getEventType() == AccessibilityEvent.TYPE_VIEW_ACCESSIBILITY_FOCUSED) {
            Log.d("ACCESSIBILITY", "I am now in FOCUS");
            setText("I AM IN FOCUS");
            setBackgroundColor(Color.GREEN);
        }
        else if (event.getEventType() == AccessibilityEvent.TYPE_VIEW_ACCESSIBILITY_FOCUS_CLEARED) {
            Log.d("ACCESSIBILITY", "I lost the FOCUS");
            setText("I AM NO MORE IN FOCUS");
            setBackgroundColor(Color.RED);
        }
    }
}
