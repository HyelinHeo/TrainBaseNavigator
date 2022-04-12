using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTarget : GUIWindow
{
    public InputMouse MyInput;
    public Image TargetImg;
    private Camera myCamera;


    void Start()
    {
        CameraModeManager.Inst.OnChangeMode.AddListener(OnChangeCamera);
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
        MyInput.OnRaycastHit.AddListener(OnRaycastHit);
        MyInput.OnRaycastNoHit.AddListener(OnRaycastNoHit);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        if (mode == EditMode.FOUNDATION)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void OnRaycastNoHit()
    {
        Hide();
    }

    private void OnChangeCamera(CameraMode mode)
    {
        myCamera = MPXCamera.CurrentCamera.MyCam;
        if (mode == CameraMode.CAM_2D)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void OnRaycastHit(GameObject go)
    {
        if (myCamera != null 
            && CameraModeManager.Inst.CurrentMode == CameraMode.CAM_2D 
            && EditModeManager.Inst.CurrentMode == EditMode.FOUNDATION)
        {
            Show();

            Vector3 p = myCamera.WorldToScreenPoint(MyInput.HitPos);
            TargetImg.transform.position = p;
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        if (!TargetImg.enabled)
            TargetImg.enabled = true;
    }

    public override void Hide()
    {
        if (TargetImg.enabled)
            TargetImg.enabled = false;
    }
}
