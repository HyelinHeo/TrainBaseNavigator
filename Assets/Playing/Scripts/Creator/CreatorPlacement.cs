using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorPlacement : CreatorProperty
{

    protected override void Start()
    {
        base.Start();

        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
    }

    public override MPXObject Create()
    {
        return base.Create();
    }

    public override MPXObject CreatePreview()
    {
        return base.CreatePreview();
    }

    private void OnChangeEditMode(EditMode mode)
    {
        if (mode == EditMode.PLACEMENT)
        {
            UseCreator = true;
        }
        else
        {
            UseCreator = false;
        }
    }
}
