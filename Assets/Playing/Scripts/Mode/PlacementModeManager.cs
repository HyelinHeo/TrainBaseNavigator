using UnityEngine;
using System.Collections;

public enum PlacementMode
{
    NONE,
    PLACEMENT
}

public class PlacementModeManager : ModeManager<PlacementModeManager, PlacementMode>
{
    private void Start()
    {
        currentMode = PlacementMode.NONE;
    }

    public override void ChangeMode(PlacementMode mode)
    {
        base.ChangeMode(mode);
    }

}
