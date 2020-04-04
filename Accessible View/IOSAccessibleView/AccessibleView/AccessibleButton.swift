//
//  AccessibleImage.swift
//  AccessibleView
//
//  Created by Paolo Pecis on 27/02/2020.
//  Copyright © 2020 Paolo Pecis. All rights reserved.
//

import UIKit

class AccessibleButton: UIButton {
    override init(frame: CGRect) {
        super.init(frame: frame)
        backgroundColor = .red
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    override func accessibilityElementDidBecomeFocused() {
        print("I AM in FOCUS")
        setTitle("I AM IN FOCUS ", for: UIControl.State.normal)
        backgroundColor = .green
    }
    
    override func accessibilityElementDidLoseFocus() {
        print("I LOST the FOCUS")
        setTitle("I AM NO MORE IN FOCUS ", for: UIControl.State.normal)
        backgroundColor = .red
    }
}
