using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

/// <summary>
/// 리스트 동적 생성 담당
/// </summary>
public class GUIPlacementItemList : GUIWindow, IMPXList<GUIPlacementItem>
{
    private List<GUIPlacementItem> items;

    public int Count { get { if(items != null) { return items.Count; } return 0; } }

    private void Awake()
    {
        items = new List<GUIPlacementItem>();
    }

    private void Start()
    {
        GUIPlacementItem[] objs = GetComponentsInChildren<GUIPlacementItem>();
        items = new List<GUIPlacementItem>(objs);
    }

    public void Add(GUIPlacementItem item)
    {
        items.Add(item);
    }

    public void Clear()
    {
        if (items != null)
        {
            items.Clear();
        }
    }

    public bool Remove(GUIPlacementItem item)
    {
        if (items.Contains(item))
        {
            return items.Remove(item);
        }
        return false;
    }

    /// <summary>
    /// MPX오브젝트 프리팹 이름으로 가져오는 함수
    /// </summary>
    public MPXObject FindPlacementItemObject(string objName)
    {
        GUIPlacementItem item = Find(objName);
        if (item != null)
        {
            return item.ItemObject;
        }
        return null;
    }

    public GUIPlacementItem Find(string objName)
    {
        if (items != null)
        {
            return items.Find(o => o.ItemObject.name == objName);
        }
        return null;
    }

    public int FindPlacementItemIndex(string objName)
    {
        if (items != null)
        {
            return items.FindIndex(o => o.ItemObject.name == objName);
        }
        return -1;
    }
}
