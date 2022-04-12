using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNavigationMode : ChangeMode<NavigationMode>
{
    public override void Change()
    {
        NavigationModeManager.Inst.ChangeMode(MyMode);
    }
}
