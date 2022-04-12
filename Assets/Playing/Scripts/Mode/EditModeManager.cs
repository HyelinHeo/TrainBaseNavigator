using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EditMode
{
    DEFAULT,
    FOUNDATION,//기초 공사 모드
    PLACEMENT,//배치 모드
    NAVIGATION//네비게이터 모드
}

public class EditModeManager : ModeManager<EditModeManager, EditMode>
{
    private void Start()
    {
        currentMode = EditMode.DEFAULT;
        ChangeMode(EditMode.FOUNDATION);
    }

    public override void ChangeMode(EditMode mode)
    {
        base.ChangeMode(mode);

        if (mode == EditMode.FOUNDATION || mode == EditMode.DEFAULT)
        {
            SelectObject.UseSelection = false;
        }
        else
        {
            SelectObject.UseSelection = true;
        }

        if (mode == EditMode.NAVIGATION)
        {
            NavigationModeManager.Inst.ChangeMode(NavigationMode.DEFAULT);
        }
    }
}
