using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPXDrawObject : MPXObject
{
    public const string MY_LAYER = "FoundationObject";


    public override void Init()
    {
        MyLayer = LayerMask.NameToLayer(MY_LAYER);

        AttachSelectScript();
 

        base.Init();

    }
}
