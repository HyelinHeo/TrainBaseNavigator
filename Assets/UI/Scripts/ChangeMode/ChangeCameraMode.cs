using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : ChangeMode<CameraMode>
{
    public override void Change()
    {
        CameraModeManager.Inst.ChangeMode(MyMode);
    }
}
