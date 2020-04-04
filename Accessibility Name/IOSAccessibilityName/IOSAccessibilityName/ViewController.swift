//
//  ViewController.swift
//  IOSAccessibilityName
//
//  Created by Paolo Pecis on 01/03/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    var b1,b2 : UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()
        
        b1 = UIButton(frame: CGRect(x: 50, y: 50, width: 250, height: 50))
        b1.setTitle("USELESS BUTTON WITH NO DESCRIPTION", for: .normal)
        view.addSubview(b1)
        
        b2 = UIButton(frame: CGRect(x: 50, y: 150, width: 250, height: 50))
        b2.setTitle("I HAVE A DESCRIPTION", for: .normal)
        b2.accessibilityLabel = "This is my accessible label"
        b2.accessibilityHint = "This is my accessible hint"
        view.addSubview(b2)
        
    }


}

