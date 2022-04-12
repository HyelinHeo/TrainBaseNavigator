using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPXPlane : MPXDrawObject
{
    public const string PROPERTY_SCALE_X = "scalex";
    public const string PROPERTY_SCALE_Y = "scaley";
    public const string PROPERTY_SCALE_Z = "scalez";
   

    private void Start()
    {
 
    }

    public override void Init()
    {
        base.Init();
        
        UseSelect = false;

        MyLayer = LayerMask.NameToLayer("FoundationObject");

        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        if (mode == EditMode.NAVIGATION)
        {
            UseSelect = true;
        }
        else
        {
            UseSelect = false;
        }
    }
}
