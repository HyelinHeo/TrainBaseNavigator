using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEditMode : ChangeMode<EditMode>
{
    public override void Change()
    {
        if (EditModeManager.Inst.CurrentMode == EditMode.NAVIGATION)
        {
            NavigationModeManager.Inst.OnExitNavigationMode.Invoke();
        }
        EditModeManager.Inst.ChangeMode(MyMode);
    }
}
