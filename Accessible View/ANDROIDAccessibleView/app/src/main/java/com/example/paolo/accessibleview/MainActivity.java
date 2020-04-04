package com.example.paolo.accessibleview;

import android.graphics.Color;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        LinearLayout linearlayout = findViewById(R.id.linearlayout);

        Button notAccessibleBUtton = new Button(this);
        notAccessibleBUtton.setText("USELESS BUTTON");
        linearlayout.addView(notAccessibleBUtton);

        AccessibleButton accessibleButton = new AccessibleButton(this);
        accessibleButton.setText("NOT IN FOCUS");
        linearlayout.addView(accessibleButton);
    }
}
