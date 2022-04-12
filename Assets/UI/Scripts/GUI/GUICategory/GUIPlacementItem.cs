using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class GUIPlacementItem : GUIWindow, IPointerDownHandler
{
    //배경색, 선택됐음을 알려주기 위함.
    public Image ItemBackgroundImage;
    public RawImage ItemImage;
    public Texture ItemPreview;
    public string ItemStrName;
    public Text ItemName;
    //Width x Height x Depth
    public Text ItemSize;
    public GUIPlacement Placement;

    public MPXObject ItemObject;
    public int ItemCategoryIndex;
    //public string ItemCategoryText;

    private void Start()
    {
        Placement.Items.Add(this);
        Init();
    }

    private void OnDestroy()
    {
        Placement.Items.Remove(this);
        Placement.Category.OnCategoryChange.RemoveListener(ChangeItem);
        Placement.OnChangeSelect.RemoveListener(ChangeSelect);
        Placement.CreatorPut.OnPutObject.RemoveListener(OnPutMpxObject);
    }

    public override void Init()
    {
        Placement.Category.OnCategoryChange.AddListener(ChangeItem);
        Placement.OnChangeSelect.AddListener(ChangeSelect);
        Placement.CreatorPut.OnPutObject.AddListener(OnPutMpxObject);
        //초기값
        SetValue("");
        ChangeItem(0);
    }
    /// <summary>
    /// 오브젝트를 3D공간상에 놓은 후
    /// </summary>
    private void OnPutMpxObject()
    {
        ItemBackgroundImage.color = DefaultColor;
    }

    private void ChangeItem(int idx)
    {
        if (ItemCategoryIndex.Equals(idx))
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public virtual void SetIndex(int idx)
    {
        ItemCategoryIndex = idx;
    }

    public virtual int GetIndex()
    {
        return ItemCategoryIndex;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Placement.Select(ItemObject);
    }

    private void ChangeSelect(MPXObject selectItem)
    {
        if (selectItem.Equals(ItemObject))
        {
            ItemBackgroundImage.color = HighlightColor;
        }
        else
        {
            ItemBackgroundImage.color = DefaultColor;
        }
    }

    /// <summary>
    /// 기본값 세팅
    /// setting default value
    /// </summary>
    /// <param name="itemName"></param>
    public void SetValue(string itemName)
    {
        ItemImage.texture = ItemPreview;
        //ItemName.text = itemName;
        ItemName.text = ItemStrName;
        ItemSize.text = MeasureSize();
    }
    /// <summary>
    /// 모델링 사이즈 측정
    /// </summary>
    /// <returns></returns>
    string MeasureSize()
    {
        //측정방법 수정필요_20201015
        if (ItemObject != null)
        {
            Vector3 objSize = ItemObject.transform.localScale;
            //Output();
            return objSize.x + "X" + objSize.y + "X" + objSize.z;
        }
        return string.Empty;
    }

    //void Output()
    //{
    //    Bounds bounds = new Bounds();
    //    foreach (Collider collider in ItemObject.GetComponentsInChildren<Collider>())
    //    {
    //        bounds.Encapsulate(collider.bounds);
    //    }
    //    //Debug.Log(bounds.size);
    //    //Debug.Log(bounds);
    //    Gizmos.DrawCube(bounds.center, bounds.size);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Bounds totalBounds = new Bounds();

    //    Collider[] cols = GetComponentsInChildren<Collider>();

    //    for (int i = 0; i < cols.Length; i++)
    //    {
    //        if (i == 0)
    //            totalBounds = cols[i].bounds;
    //            else
    //            totalBounds.Encapsulate(cols[i].bounds);

    //    }

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(totalBounds.center, totalBounds.size);
    //    Debug.Log(totalBounds.size);
    //    Debug.Log(totalBounds);
    //}
}
