using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoundationMode : ChangeMode<FoundationMode>
{
    public override void Change()
    {
        FoundationModeManager.Inst.ChangeMode(MyMode);
    }
}
