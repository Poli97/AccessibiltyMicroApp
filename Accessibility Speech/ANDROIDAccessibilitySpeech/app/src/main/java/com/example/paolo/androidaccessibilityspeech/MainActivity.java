package com.example.paolo.androidaccessibilityspeech;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    Button speakButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        LinearLayout linearlayout = findViewById(R.id.linearlayout);

        speakButton = new Button(this);
        speakButton.setText("PRESS ME TO SPEAK");
        speakButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                v.announceForAccessibility("This is an accessibility speak in Android");
            }
        });

        linearlayout.addView(speakButton);
    }
}
