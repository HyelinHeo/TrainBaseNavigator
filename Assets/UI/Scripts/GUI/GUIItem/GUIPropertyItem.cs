using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum PropertyItemType
{
    Slider = 0,
    Dropdown = 1,
    TextField = 2
}
/// <summary>
/// 아이템 스크립트의 부모
/// </summary>
public class GUIPropertyItem : GUIItem
{
    public int ItemID;
    public Text ItemName;
    public string ItemValue;

    public UnityEvent<GUIPropertyItem> OnValueChanged = new UnityEvent<GUIPropertyItem>();
    public UnityEvent OnControlMouseStart = new UnityEvent();
    public UnityEvent OnControlMouseEnd = new UnityEvent();

    public virtual void Init()
    {
        ItemID = 0;
        ItemName.text = string.Empty;
        ItemValue = string.Empty;
    }


    public virtual float GetValue()
    {
        float val = 0;
        float.TryParse(ItemValue, out val);
        return val;
    }

    public virtual string GetName()
    {
        return ItemName.text;
    }

    public virtual void SetName(string title)
    {
        ItemName.text = title;
    }

    public virtual void SetValue(string title, string val)
    {
        ItemName.text = title;
        ItemValue = val;
    }

    public virtual void SetValue(MPXProperty property) {
        SetValue(property.Name, property.Value.ToString());
    }

    public virtual MPXProperty GetProperty() {
        MPXProperty p = new MPXProperty();
        p.Name = GetName();
        p.Value = GetValue();

        return p;
    }
}
