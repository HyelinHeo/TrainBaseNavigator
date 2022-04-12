using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectObject : ControlObject
{
    public static List<SelectObject> SelectObjects;
    public static int SelectObjectCount
    {
        get
        {
            if (SelectObjects != null) return SelectObjects.Count;
            else return 0;
        }
    }

    public static bool UseSelection;

    [SerializeField]
    private bool isSelect = false;
    public bool IsSelect { get { return isSelect; } }
    //public Camera myCamera;

    public UnityEvent OnSelected = new UnityEvent();
    public UnityEvent UnSelected = new UnityEvent();

    public static UnityEvent<SelectObject> AddSelectObjected = new UnityEvent<SelectObject>();
    public static UnityEvent<SelectObject> RemoveSelectObjected = new UnityEvent<SelectObject>();
    public UnityEvent OnRefresh = new UnityEvent();


    void Awake()
    {
        Init();
        AddSelectObjected.AddListener(AddSelectedObject);
        InputMouse.Inst.OnClickNoHit.AddListener(OnRayCastNoHit);
        testShadow.Inst.OnDestroyAll.AddListener(DestroyThis);
    }


    private void OnRayCastNoHit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isSelect)
                UnSelect();

        }
    }

    private void AddSelectedObject(SelectObject obj)
    {
        if (!InputKey.Inst.IsFunc1)
        {
            if (obj != this && isSelect)
            {
                UnSelect();

            }
        }
    }

    private void Init()
    {
        isSelect = false;
    }

    private void OnMouseUp()
    {
        if (MyObject.UseSelect && UseSelection)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!isSelect)
                {
                    Select();
                }
                else
                {
                    UnSelect();
                }
            }
        }
    }


    public void Select()
    {
        SelectOnly();
        OnSelected.Invoke();
    }

    public void UnSelect()
    {
        UnSelectOnly();
        UnSelected.Invoke();
    }

    public void SelectOnly()
    {
        isSelect = true;
        AddSelectObject(this);
    }

    public void UnSelectOnly()
    {
        isSelect = false;
        RemoveSelectObject(this);
    }

    public static void AddSelectObject(SelectObject obj)
    {
        if (SelectObjects == null)
            SelectObjects = new List<SelectObject>();

        SelectObjects.Add(obj);
        AddSelectObjected.Invoke(obj);
    }

    public static void RemoveSelectObject(SelectObject obj)
    {
        if (SelectObjects != null)
        {
            if (SelectObjects.Contains(obj))
            {
                SelectObjects.Remove(obj);
            }
        }

    }

    public static void ClearSelectObjects()
    {
        if (SelectObjects != null)
        {
            for (int i = 0; i < SelectObjects.Count; i++)
            {
                SelectObjects[i].UnSelect();
            }
            SelectObjects.Clear();
        }
    }

    public static SelectObject FindSelectObject(SelectObject obj)
    {
        if (SelectObjects != null)
        {
            return SelectObjects.Find(o => o.Equals(obj));
        }

        return null;
    }

    private void OnDestroy()
    {
        AddSelectObjected.RemoveListener(AddSelectedObject);
        InputMouse.Inst.OnClickNoHit.RemoveListener(OnRayCastNoHit);
    }

    public void Refresh()
    {
        OnRefresh.Invoke();
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
