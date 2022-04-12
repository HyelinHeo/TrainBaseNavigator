using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorFoundation : CreatorProperty
{
    public FoundationMode MyMode;

    protected override void Start()
    {
        base.Start();

        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
        FoundationModeManager.Inst.OnChangeMode.AddListener(o => OnChangeFoundationMode(o));
        CameraModeManager.Inst.OnChangeMode.AddListener(OnCameraChanged);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        SetUse();
    }

    private void OnCameraChanged(CameraMode mode)
    {
        SetUse();
    }


    public void OnChangeFoundationMode(FoundationMode mode)
    {
        SetUse();
    }

    public override MPXObject Create()
    {
        MPXObject obj = base.Create();

        return obj;
    }

    void SetUse()
    {
        if (CameraModeManager.Inst.CurrentMode == CameraMode.CAM_2D 
            && EditModeManager.Inst.CurrentMode == EditMode.FOUNDATION)
        {
            if (FoundationModeManager.Inst.CurrentMode == MyMode)
            {
                UseCreator = true;
            }
            else
            {
                UseCreator = false;
            }
        }
        else
        {
            UseCreator = false;
        }
    }
}
