//
//  ViewController.swift
//  IOSAccessibilityOrder
//
//  Created by Paolo Pecis on 28/02/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    var b1, b2, b3, changeorder : UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()
        
        changeorder = UIButton(frame: CGRect(x: 10, y: 50, width: 290, height: 50))
        changeorder.setTitle("press to change order", for: .normal)
        changeorder.backgroundColor = .red
        view.addSubview(changeorder)
        changeorder.addTarget(self, action: #selector(changeOrder), for: .touchDown)

        
        b1 = UIButton(frame: CGRect(x: 50, y: 150, width: 250, height: 50))
        b1.setTitle("1° TO BE READ", for: .normal)
        b1.backgroundColor = .orange
        view.addSubview(b1)
        
        b2 = UIButton(frame: CGRect(x: 50, y: 250, width: 250, height: 50))
        b2.setTitle("2° TO BE READ", for: .normal)
        b2.backgroundColor = .orange
        view.addSubview(b2)
        
        b3 = UIButton(frame: CGRect(x: 50, y: 350, width: 250, height: 50))
        b3.setTitle("3° TO BE READ", for: .normal)
        b3.backgroundColor = .orange
        view.addSubview(b3)
        
    }
    
    @objc func changeOrder(sender : UIButton){
        self.view.accessibilityElements = [changeorder, b3, b1, b2]
        b3.setTitle("CHANGED TO 1°", for: .normal)
        b1.setTitle("CHANGED TO 2°", for: .normal)
        b2.setTitle("CHANGED TO 3°", for: .normal)

    }


}

