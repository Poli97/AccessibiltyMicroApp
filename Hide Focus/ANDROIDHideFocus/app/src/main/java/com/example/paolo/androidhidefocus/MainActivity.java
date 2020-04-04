package com.example.paolo.androidhidefocus;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    LinearLayout linearLayout;
    Button b1, b2, b3;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        linearLayout = findViewById(R.id.linearlayout);

        b1 = new Button(this);
        b1.setText("Press to hide BUTTON 2 focus");
        b1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                b2.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_NO);
                b2.setText("I am no more focusable");
            }
        });
        linearLayout.addView(b1);

        b2 = new Button(this);
        b2.setText("BUTTON 2");
        linearLayout.addView(b2);

        b3 = new Button(this);
        b3.setText("BUTTON 3");
        linearLayout.addView(b3);
    }
}
