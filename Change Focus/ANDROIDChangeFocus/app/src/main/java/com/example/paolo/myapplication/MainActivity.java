package com.example.paolo.myapplication;

import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.accessibility.AccessibilityEvent;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

public class MainActivity extends AppCompatActivity {

    Button b1, b2, b3;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        LinearLayout linearlayout = findViewById(R.id.linearlayout);
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT
        );
        params.setMargins(10,20,10,10);

        b1 = new Button(this);
        b1.setText("Press to change focus to BUTTON 3");
        b1.setBackgroundColor(Color.GREEN);
        linearlayout.addView(b1);
        b1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                b3.sendAccessibilityEvent(AccessibilityEvent.WINDOWS_CHANGE_ACCESSIBILITY_FOCUSED);
            }
        });

        b2 = new Button(this);
        b2.setLayoutParams(params);
        b2.setText("BUTTON 2");
        b2.setBackgroundColor(Color.GREEN);
        linearlayout.addView(b2);

        b3 = new Button(this);
        b3.setLayoutParams(params);
        b3.setText("BUTTON 3");
        b3.setBackgroundColor(Color.GREEN);
        linearlayout.addView(b3);

    }
}
