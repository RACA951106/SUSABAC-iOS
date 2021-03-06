﻿using System;
using System.Drawing;
using UIKit;

namespace CABASUS
{
    public class CustomKeyboard : UIToolbar
    {
        public UIView prevTextFieldOrView { get; set; }
        public UIView currentTextFieldOrView { get; set; }
        public UIView nextTextFieldOrView { get; set; }

        public CustomKeyboard() : base() { }

        public CustomKeyboard(UIView current, UIView previous, UIView next)
        {
            this.currentTextFieldOrView = current;
            this.prevTextFieldOrView = previous;
            this.nextTextFieldOrView = next;
            SetupToolbar();
        }

        void SetupToolbar()
        {
            Frame = new RectangleF(0.0f, 0.0f, 320, 44.0f);
            TintColor = UIColor.DarkGray;
            Translucent = false;

            if (prevTextFieldOrView == null && nextTextFieldOrView == null) 
            {
                Items = new UIBarButtonItem[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                    {
                        currentTextFieldOrView.ResignFirstResponder();
                    })
                };
            }
            else
            {
                Items = new UIBarButtonItem[]
                {
                    new UIBarButtonItem("Prev",
                        UIBarButtonItemStyle.Bordered, delegate
                        {
                            prevTextFieldOrView.BecomeFirstResponder();
                        })
                        { Enabled = prevTextFieldOrView != null },
                    new UIBarButtonItem("Next",
                        UIBarButtonItemStyle.Bordered, delegate
                        {
                            nextTextFieldOrView.BecomeFirstResponder();
                        })
                        { Enabled = nextTextFieldOrView != null },
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                    {
                        currentTextFieldOrView.ResignFirstResponder();
                    })
                };
            }
        }
    }
}
