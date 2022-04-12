using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour, IMPXList<MPXObject>
{
    public InputMouse MyInputMouse;
    public MPXObject Prefab;

    protected MPXObject CurrentObject;

    public bool UseCreator;


    /// <summary>
    /// 새작업시 실행
    /// </summary>
    public virtual void InitNew() { }

    /// <summary>
    /// 불러오기 실행
    /// </summary>
    public virtual void InitLoad() { }

    public virtual MPXObject Create()
    {
        MPXObject obj = CreatePreview();
        if (obj != null)
        {
            obj.Init();
        }
        obj.SetLayer();

        return obj;
    }

    public virtual MPXObject CreatePreview()
    {
        if (Prefab != null)
        {
            MPXObject go = Instantiate(Prefab);
            go.PrefabName = Prefab.name;
            go.transform.SetParent(transform);
            go.SetIgnoreLayer();

            return go;
        }
        else
        {
            Debug.LogWarning("prefab not found.");
            return null;
        }
    }

    protected virtual void Start()
    {
        Init();
    }


    public virtual void Init()
    {
        CurrentObject = null;
    }

    public virtual void DestoryObject()
    {
        if (CurrentObject != null)
        {
            Destroy(CurrentObject.gameObject);
        }
        CurrentObject = null;
    }

    public void CreateObject() { Create(); }


    #region IMPXList interface 구현

    public void Add(MPXObject item)
    {
        if (objs == null)
            objs = new List<MPXObject>();

        objs.Add(item);
    }

    public virtual void Clear()
    {
        if (objs != null)
        {
            objs.Clear();
        }
    }

    public bool Remove(MPXObject item)
    {
        if (objs != null)
        {
            return objs.Remove(item);
        }
        return false;
    }

    private List<MPXObject> objs;

    public int Count { get { if (objs != null) { return objs.Count; } return 0; } }

    #endregion


}
