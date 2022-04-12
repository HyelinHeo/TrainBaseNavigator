using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatorBasePlane : CreatorProperty
{
    /// <summary>
    /// 초기값
    /// </summary>
    public Vector3 Scale;


    public override void Init()
    {
        base.Init();
    }

    public override void InitNew()
    {
        base.InitNew();
        Create();
    }

    protected override void InitProperties()
    {
        MPXProperty scalex = new MPXProperty();
        scalex.Name = MPXPlane.PROPERTY_SCALE_X;
        scalex.Value = Scale.x;
        scalex.UseUI = false;

        Add(scalex);

        MPXProperty scaley = new MPXProperty();
        scaley.Name = MPXPlane.PROPERTY_SCALE_Y;
        scaley.Value = Scale.y;
        scalex.UseUI = false;

        Add(scaley);

        MPXProperty scalez = new MPXProperty();
        scalez.Name = MPXPlane.PROPERTY_SCALE_Z;
        scalez.Value = Scale.z;
        scalex.UseUI = false;

        Add(scalez);
    }

    public override MPXObject Create(List<MPXProperty> p)
    {
        Scale.x = GetProperty(MPXPlane.PROPERTY_SCALE_X).Value;
        Scale.y = GetProperty(MPXPlane.PROPERTY_SCALE_Y).Value;
        Scale.z = GetProperty(MPXPlane.PROPERTY_SCALE_Z).Value;

        MPXObject go = base.Create(p);
        go.gameObject.AddComponent<NavMeshSurface>();

        return go;
    }

   

    public override MPXObject Create()
    {
        if (Scale.x > 0 && Scale.z > 0)
        {
            MPXObject go = base.Create();
            //go.SetProperties(properties);
            go.transform.localScale = Scale;
            go.gameObject.AddComponent<NavMeshSurface>();

            return go;
        }
        return null;
    }
}
