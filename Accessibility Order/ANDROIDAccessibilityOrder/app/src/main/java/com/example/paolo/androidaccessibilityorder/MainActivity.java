package com.example.paolo.androidaccessibilityorder;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.accessibility.AccessibilityEvent;
import android.view.accessibility.AccessibilityManager;
import android.widget.Button;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    Button b1,b2,b3, changeorder;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        LinearLayout linearlayout = findViewById(R.id.linearlayout);

        changeorder = new Button(this);
        changeorder.setId(R.id.changeOrderButton); // Add the ID so that accessibility traversal can identify this view
        changeorder.setText("Presso to change order");
        changeorder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.i("BUTTON","Clicked change order button");
                b3.invalidate();
                b1.invalidate();
                b2.invalidate();
                //changeorder.setAccessibilityTraversalBefore(b3.getId());
                b3.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
                b3.setAccessibilityTraversalAfter(changeorder.getId());
                //b3.setAccessibilityTraversalBefore(b1.getId());
                b1.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
                b1.setAccessibilityTraversalAfter(b3.getId());
                //b1.setAccessibilityTraversalBefore(b2.getId());
                b2.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
                b2.setAccessibilityTraversalAfter(b1.getId());
                changeorder.setText("Order changed b3,b1,b2");


            }
        });
        linearlayout.addView(changeorder);
        Log.i("PIPPO","ciao");
        AccessibilityManager am = ((AccessibilityManager) getSystemService(ACCESSIBILITY_SERVICE));
        if(am.isEnabled()){
            Log.i("PIPPO","talkback attivo");
        }
        if(am.isTouchExplorationEnabled()){
            Log.i("PIPPO","Exploration attiva");
        }

        b1 = new Button(this);
        b1.setId(R.id.button1); // Add the ID so that accessibility traversal can identify this view
        b1.setText("BUTTON 1");
        linearlayout.addView(b1);


        b2 = new Button(this);
        b2.setId(R.id.button2); // Add the ID so that accessibility traversal can identify this view
        b2.setText("BUTTON 2");
        linearlayout.addView(b2);


        b3 = new Button(this);
        b3.setId(R.id.button3); // Add the ID so that accessibility traversal can identify this view
        b3.setText("BUTTON 3");
        linearlayout.addView(b3);


        }
}
