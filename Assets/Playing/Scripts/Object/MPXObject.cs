using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MPXObject : MonoBehaviour, IMPXList<MPXProperty>
{
    public string ID;
    public SelectObject SelectObj;
    public Rigidbody Rigid;

    public List<MPXProperty> Properties;
    public Collider Col;

    public LayerMask MyLayer;

    public bool UseSelect = true;

    public string PrefabName;


    public bool IsSelect
    {
        get
        {
            if (SelectObj != null)
                return SelectObj.IsSelect;
            return false;
        }
    }

    public int Count { get { if (Properties != null) { return Properties.Count; } return 0; } }

    public Vector3 GetCenter()
    {
        if (Col != null)
        {
            return Col.bounds.center;
        }
        return Vector3.zero;
    }

    public Vector3 GetCenterForward()
    {
        if (Col != null)
        {
            return Col.bounds.center + transform.forward * Col.bounds.size.z / 2;
        }
        return Vector3.zero;
    }

    public static string NewID()
    {
        return Guid.NewGuid().ToString();
    }

    public virtual void Draw() { }

    private void Awake()
    {
        InputKey.Inst.OnKeyDownDelete.AddListener(OnKeyDownDelete);
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        InitChangeEditMode(mode);
    }

    protected virtual void OnKeyDownDelete()
    {
        if (SelectObj.IsSelect)
        {
            SelectObj.UnSelectOnly();
            Remove();
        }
    }

    public virtual void InitChangeEditMode(EditMode mode) { }

    public void AttachSelectScript()
    {
        if (SelectObj == null)
        {
            GameObject go = null;
            if (gameObject.GetComponent<MeshFilter>() != null)
            {
                go = gameObject;
            }
            else
            {
                Transform tr = transform.GetChild(0);
                go = tr.gameObject;
            }


            SelectObj = go.AddComponent<SelectObject>();
            SelectObj.MyObject = this;
            SelectObj.OnSelected.AddListener(OnSelected);
            SelectObj.UnSelected.AddListener(UnSelected);

            SelectObjectEffect eff = go.AddComponent<SelectObjectEffect>();
            Outline outLine = go.AddComponent<Outline>();

            eff.SelectObj = SelectObj;
            eff.SelectOutline = outLine;
        }
    }

    protected virtual void UnSelected()
    {
        if (Rigid != null)
            Rigid.isKinematic = false;

        SetLayer();
    }

    protected virtual void OnSelected()
    {
        if (Rigid != null) 
            Rigid.isKinematic = true;

        SetIgnoreLayer();
    }

    public virtual void Draw(List<MPXProperty> properties)
    {
        Draw();
    }

    public virtual void Remove()
    {
        Destroy(gameObject);
    }

    public MPXProperty FindProperty(string pName)
    {
        if (Properties != null)
        {
            return Properties.Find(o => o.Name == pName);
        }
        return null;
    }

    public int FindPropertyIndex(string pName)
    {
        if (Properties != null)
        {
            return Properties.FindIndex(o => o.Name == pName);
        }
        return -1;
    }

    public void ClearProperties()
    {
        if (Properties != null)
        {
            Properties.Clear();
        }
    }

    public void SetProperty(string pName, float val)
    {
        MPXProperty p = FindProperty(pName);
        if (p != null)
        {
            p.Value = val;
        }
        else
        {
            p = new MPXProperty();
            p.Name = pName;
            p.Value = val;

            Add(p);
        }
    }

    public void SetProperty(MPXProperty p)
    {
        MPXProperty property = FindProperty(p.Name);
        if (property != null)
        {
            property.Value = p.Value;
        }
        else
        {
            Add(p);
        }
    }

    public MPXProperty GetProperty(string pName)
    {
        return FindProperty(pName);
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


    public void SetProperties(List<MPXProperty> properties)
    {
        if (properties != null)
        {
            if (Properties == null)
            {
                Properties = new List<MPXProperty>(properties);
            }
            else
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    SetPropertyOnlyValue(properties[i]);
                }
            }
        }
    }

    public MPXProperty[] GetProperties()
    {
        if (Properties != null)
            return Properties.ToArray();

        return null;
    }

    public virtual void Init()
    {
        UseSelect = true;

        if (ID == null || string.IsNullOrEmpty(ID))
        {
            ID = NewID();
        }
    }

    public virtual void SetLayer() {
        Utility.SetLayer(gameObject, MyLayer);
    }

    public void SetIgnoreLayer() {
        Utility.SetLayerIgnore(gameObject);
    }

    public void Add(MPXProperty item)
    {
        if (Properties == null)
        {
            Properties = new List<MPXProperty>();
        }

        Properties.Add(item);
    }

    public void Clear()
    {
        if (Properties != null)
        {
            Properties.Clear();
        }
    }

    public bool Remove(MPXProperty item)
    {
        if (Properties != null)
            return Properties.Remove(item);

        return false;
    }
}
