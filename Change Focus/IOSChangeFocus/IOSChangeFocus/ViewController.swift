//
//  ViewController.swift
//  IOSChangeFocus
//
//  Created by Paolo Pecis on 27/02/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    public var b1, b2, b3 : UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        b1 = UIButton(frame: CGRect(x: 50, y: 50, width: 250, height: 50))
        b1.setTitle("Press to change focus to BUTTON 3", for: UIControl.State.normal)
        b1.backgroundColor = .green
        b1.addTarget(self, action: #selector(changeFocusTob3), for: .touchDown)
        view.addSubview(b1)
        
        b2 = UIButton(frame: CGRect(x: 50, y: 150, width: 250, height: 50))
        b2.setTitle("BUTTON 2", for: UIControl.State.normal)
        b2.backgroundColor = .green
        view.addSubview(b2)
        
        b3 = UIButton(frame: CGRect(x: 50, y: 250, width: 250, height: 50))
        b3.setTitle("BUTTON 3", for: UIControl.State.normal)
        b3.backgroundColor = .green
        view.addSubview(b3)
    }
    
    @objc func changeFocusTob3(sender: UIButton) {
        print("Pressed to change the focus on button 3")
        UIAccessibility.post(notification: UIAccessibility.Notification.screenChanged, argument: b3);

    }


}

