using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GUIChangeView : GUIWindow
{
    public Image Image2D;
    public Image Image3D;
    

    public override void Show()
    {
        CameraModeManager.Inst.OnChangeMode.AddListener(OnCameraModeChange);
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        Show();
        base.Init();
    }

    private void OnCameraModeChange(CameraMode camMode)
    {
        if (camMode==CameraMode.CAM_2D)
        {
            OnClick2D();
        }
        else if (camMode == CameraMode.CAM_3D)
        {
            OnClick3D();
        }
    }

    public void OnClick2D()
    {
        Image2D.color = HighlightColor;
        Image3D.color = DefaultColor;
    }

    public void OnClick3D()
    {
        Image2D.color = DefaultColor;
        Image3D.color = HighlightColor; 
    }

    private void OnDestroy()
    {
        CameraModeManager.Inst.OnChangeMode.RemoveListener(OnCameraModeChange);
    }
}
