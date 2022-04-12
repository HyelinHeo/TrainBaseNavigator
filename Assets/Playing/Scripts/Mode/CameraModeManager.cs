using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    NONE,
    CAM_2D,
    CAM_3D
}


public class CameraModeManager : ModeManager<CameraModeManager, CameraMode>
{
    private void Start()
    {
        currentMode = CameraMode.NONE;
        ChangeMode(CameraMode.CAM_2D);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (CurrentMode == CameraMode.CAM_3D)
            {
                ChangeMode(CameraMode.CAM_2D);
            }
            else
            {
                ChangeMode(CameraMode.CAM_3D);
            }
        }
    }
}
