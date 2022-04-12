using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIReportItemList : GUIPlayWindow, IMPXList<GUIReportItem>
{
    private List<GUIReportItem> items;

    public int Count { get { if (items != null) { return items.Count; } return 0; } }

    private void Awake()
    {
        items = new List<GUIReportItem>();
    }

    private void Start()
    {
        GUIReportItem[] objs = GetComponentsInChildren<GUIReportItem>();
        items = new List<GUIReportItem>(objs);
    }

    public void Add(GUIReportItem item)
    {
        items.Add(item);
    }

    /// <summary>
    /// 리스트에서 삭제, 게임오브젝트 파괴
    /// </summary>
    public void Clear()
    {
        if (items != null)
        {
            for (int i = 0; i < Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();
        }
    }

    public bool Remove(GUIReportItem item)
    {
        if (items.Contains(item))
        {
            return items.Remove(item);
        }
        return false;
    }

    public T InstantiateGUI<T>(T ori) where T : GUIReportItem
    {
        T p = Instantiate(ori);
        p.transform.SetParent(this.transform);
        p.transform.localScale = Vector3.one;
        p.transform.localPosition = Vector3.zero;
        Add(p);
        return p;
    }
}
