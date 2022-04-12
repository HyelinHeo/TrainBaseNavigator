using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FoundationMode
{
    NONE,
    Plane = 0,
    Wall = 1,
    Door = 2,
    Rail = 3,
    ImportCad = 4
}

public class FoundationModeManager : ModeManager<FoundationModeManager, FoundationMode>
{
    private void Start()
    {
        currentMode = FoundationMode.NONE;
    }

    public override void ChangeMode(FoundationMode mode)
    {
        base.ChangeMode(mode);

        if (currentMode == FoundationMode.NONE)
        {
            SelectObject.UseSelection = true;
        }
        else
        {
            SelectObject.UseSelection = false;
        }
    }
}
