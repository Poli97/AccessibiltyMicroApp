//
//  ViewController.swift
//  AccessibleView
//
//  Created by Paolo Pecis on 27/02/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    
        var notAccessibleButton : UIButton
        notAccessibleButton = AccessibleButton(frame: CGRect(x: 50, y: 50, width: 250, height: 50))
        notAccessibleButton.backgroundColor = .gray
        notAccessibleButton.setTitle("USELESS BUTTON", for: UIControl.State.normal)
        view.addSubview(notAccessibleButton)
        
        var accessibleButton : AccessibleButton
        accessibleButton = AccessibleButton(frame: CGRect(x: 50, y: 150, width: 250, height: 50))
        accessibleButton.setTitle("NOT IN FOCUS", for: UIControl.State.normal)
        view.addSubview(accessibleButton)
        
        accessibleButton.accessibilityTraits = UIAccessibilityTraits.button
        accessibleButton.accessibilityHint = "ciao"
        
        if(UIAccessibility.isVoiceOverRunning){
            
        }
    }


}

