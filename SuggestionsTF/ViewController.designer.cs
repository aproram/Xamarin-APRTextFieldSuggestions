// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SuggestionsTF
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txt1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txt2 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (txt1 != null) {
                txt1.Dispose ();
                txt1 = null;
            }

            if (txt2 != null) {
                txt2.Dispose ();
                txt2 = null;
            }
        }
    }
}