//
//  ViewController.swift
//  IOSAccessibilitySpeech
//
//  Created by Paolo Pecis on 28/02/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    var speakButton : UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        speakButton = UIButton(frame: CGRect(x: 50, y: 50, width: 250, height: 50))
        speakButton.setTitle("Press me to speak", for: .normal)
        speakButton.addTarget(self, action: #selector(speakAccessibleText), for: .touchDown)
        view.addSubview(speakButton)

    }
    
    @objc func speakAccessibleText(sender: UIButton){
        print("Accessibility speak button pressed")
        UIAccessibility.post(notification: .screenChanged, argument: "This is an accessibility speak in iOS")
    }

}

