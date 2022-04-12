using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GUIFoundation : GUIWindow
{
    
    public override void Show()
    {
        base.Show();

        FoundationModeManager.Inst.ChangeMode(FoundationMode.Plane);
    }


    public override void Hide()
    {
        base.Hide();
    }


}
