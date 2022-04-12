using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//public enum Measure
//{
//    CM,
//    M
//}

public class GUIPropertySlider : GUIPropertyItem
{
    public Slider PropertySlider;
    public InputField TxtSliderCount;
    public Text TxtPrefix;
    public Text TxtSuffix;
    public float SliderMinValue = 0.01f;
    public float SliderMaxValue = 10f;

    public string Measure = "M";
    public const int DEFAULT_Value = 5;


    public override void Init()
    {
        base.Init();

        PropertySlider.minValue = SliderMinValue;
        PropertySlider.maxValue = SliderMaxValue;
        PropertySlider.value = DEFAULT_Value;

        ItemValue = DEFAULT_Value.ToString();
        TxtSuffix.text = Measure;
        OnSliderValueChanged();
    }

    public float GetValueFloat()
    {
        return PropertySlider.value;
    }

    public void OnTextValueChanged()
    {
        float value = float.Parse(TxtSliderCount.text);

        if (value < SliderMinValue)
            value = SliderMinValue;
        else if (value > SliderMaxValue)
            value = SliderMaxValue;

        SetValue(value);
    }

    public void ChangeValue(float min, float max)
    {
        //if (min != float.MinValue)
        //    SliderMinValue = min;
        //if (max != float.MaxValue)
        //    SliderMaxValue = max;
        InitSliderValue();
        PropertySlider.minValue = min;
        PropertySlider.maxValue = max;
        SliderMinValue = min;
        SliderMaxValue = max;
    }

    /// <summary>
    /// stack overflow 방지하기 위해 밸류값 초기화
    /// </summary>
    void InitSliderValue()
    {
        PropertySlider.minValue = float.MinValue;
        PropertySlider.maxValue = float.MaxValue;
    }

    public void OnSliderValueChanged()
    {
        SetValue(PropertySlider.value);
    }

    public void SetValue(float value)
    {
        string demVal = string.Format("{0:N2}", value);
        float val = float.Parse(demVal);
        ItemValue = val.ToString();
        PropertySlider.value = val;
        TxtSliderCount.text = val.ToString();

        OnValueChanged.Invoke(this);
    }

    public void SetValue(string title, string val, string pre, string suf, float min, float max)
    {
        ChangeValue(min, max);
        OnChangeMeasure(pre, suf);
        SetValue(float.Parse(val));
        base.SetValue(title, val);
    }

    //M,CM 단위 바꿀 때
    public void OnChangeMeasure(string prefix, string suffix)
    {
        if (prefix != null)
            TxtPrefix.text = prefix;
        else
            TxtPrefix.text = string.Empty;

        if (suffix != null)
            TxtSuffix.text = suffix;
        else
            TxtSuffix.text = string.Empty;

    }

    public override MPXProperty GetProperty()
    {
        MPXProperty p = base.GetProperty();
        p.Value = GetValueFloat();
        p.ItemType = PropertyItemType.Slider;

        return p;
    }

    public override void SetValue(MPXProperty property)
    {
        SetValue(property.Name, property.Value.ToString(), property.Prefix, property.Suffix, property.MinValue, property.MaxValue);
    }

    public void RefreshEffect()
    {
        OnControlMouseEnd.Invoke();
        //for (int i = 0; i < SelectObject.SelectObjects.Count; i++)
        //{
        //    MPXObject obj = SelectObject.SelectObjects[i].MyObject;
        //    obj.GetComponent<SelectObjectEffect>().SelectOutline.Refresh();
        //}
    }
}
