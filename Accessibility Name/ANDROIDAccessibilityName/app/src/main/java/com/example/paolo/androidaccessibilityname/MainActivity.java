package com.example.paolo.androidaccessibilityname;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    LinearLayout linearlayout;
    Button b1, b2;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        linearlayout = findViewById(R.id.linearlayout);

        b1 = new Button(this);
        b1.setText("BUTTON 1");
        b1.setContentDescription("This is my accessibility content description");
        linearlayout.addView(b1);

        b2 = new Button(this);
        b2.setText("BUTTON 2");
        linearlayout.addView(b2);
    }
}
