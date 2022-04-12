using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq.Expressions;

public class CreatorProperty : Creator, IMPXList<MPXProperty>
{
    protected List<MPXProperty> properties;

    public List<MPXProperty> Properties { get { return properties; } }

    public int PropertyCount { get { if (properties != null) { return properties.Count; } return 0; } }


    public override void Init()
    {
        base.Init();

        if (PropertyCount == 0)
        {
            InitProperties();
        }
    }

    protected virtual void InitProperties() { }

    public virtual void SetValue(MPXProperty p) { }

    public virtual void SetValue(string pName, float value)
    {
        MPXProperty p = GetProperty(pName);
        if (p != null)
        {
            p.Value = value;
        }
        else
        {
            Debug.LogError("property name not foudn. " + pName);
        }
    }

    public void Add(MPXProperty item)
    {
        if (properties == null)
            properties = new List<MPXProperty>();

        properties.Add(item);
    }

    void SetPropertyOnlyValue(MPXProperty item)
    {
        MPXProperty p = GetProperty(item.Name);
        if (p != null)
        {
            p.SetOnlyValue(item);
        }
        else
        {
            Add(new MPXProperty(item));
        }
    }


    public bool Remove(MPXProperty item)
    {
        if (properties != null)
        {
            return properties.Remove(item);
        }
        return false;
    }

    public override void Clear()
    {
        base.Clear();

        if (properties != null)
        {
            properties.Clear();
        }
    }

    public void SetPropertiesOnlyValue(List<MPXProperty> p)
    {
        for (int i = 0; i < p.Count; i++)
        {
            SetPropertyOnlyValue(p[i]);
        }
    }

    public MPXProperty GetProperty(string pName)
    {
        if (properties != null)
        {
            return properties.Find(o => o.Name == pName);
        }
        return null;
    }

    public override MPXObject Create()
    {
        MPXObject obj = base.Create();
        obj.SetProperties(properties);

        return obj;
    }

    public override MPXObject CreatePreview()
    {
        MPXObject obj = base.CreatePreview();
        obj.SetProperties(properties);

        return obj;
    }

    /// <summary>
    /// 로드시 생성용 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public virtual MPXObject Create(List<MPXProperty> p)
    {
        SetPropertiesOnlyValue(p);
        if (Prefab != null)
        {
            MPXObject go = Instantiate(Prefab);
            go.PrefabName = Prefab.name;
            go.transform.SetParent(transform);
            go.Init();
            go.SetLayer();

            return go;
        }
        else
        {
            Debug.LogWarning("prefab not found.");
            return null;
        }
    }
}
