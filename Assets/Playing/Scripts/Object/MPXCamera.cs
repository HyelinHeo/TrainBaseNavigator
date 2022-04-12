using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPXCamera : MPXObject
{
    public static MPXCamera CurrentCamera;

    public CameraMode MyMode;
    public Camera MyCam;
    public CameraOperate CamOper;

    void Awake()
    {
        CameraModeManager.Inst.OnChangeMode.AddListener(OnCameraModeChange);
    }

    public Vector3 WorldToScreen(Vector3 worldPos) { 
        if (MyCam != null)
        {
            return MyCam.WorldToScreenPoint(worldPos);
        }
        return Vector3.zero;
    }

    private void OnCameraModeChange(CameraMode mode)
    {
        if (mode == MyMode)
        {
            MyCam.enabled = true;
            CamOper.enabled = true;

            CurrentCamera = this;
        }
        else
        {
            MyCam.enabled = false;
            CamOper.enabled = false;
        }
    }
}
