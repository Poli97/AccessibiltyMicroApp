//
//  ViewController.swift
//  IOSHideFocus
//
//  Created by Paolo Pecis on 01/03/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    var b1, b2, b3 : UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        b1 = UIButton(frame: CGRect(x: 50, y: 50, width: 250, height: 20))
        b1.setTitle("Press to hide BUTTON 2 focus", for: .normal)
        view.addSubview(b1)
        b1.addTarget(self, action: #selector(hideFocus), for: .touchDown)

        
        b2 = UIButton(frame: CGRect(x: 50, y: 150, width: 250, height: 20))
        b2.setTitle("BUTTON 2", for: .normal)
        view.addSubview(b2)
        
        b3 = UIButton(frame: CGRect(x: 50, y: 250, width: 250, height: 20))
        b3.setTitle("BUTTON 3", for: .normal)
        view.addSubview(b3)
        
    }
    
    @objc func hideFocus(){
        b2.isAccessibilityElement = false
        b2.setTitle("I am no more focusable", for: .normal)
    }

}

