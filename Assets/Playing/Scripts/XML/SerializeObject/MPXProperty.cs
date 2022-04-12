
using System.Xml.Serialization;
using System;
using UnityEngine.VR;

/// <summary>
/// GUI와 Mono객체간 데이터 전달을 위한 클래스
/// </summary>
[Serializable]
/// 
public class MPXProperty : XMLObject
{
    [XmlAttribute]
    public PropertyItemType ItemType;

    [XmlAttribute]
    public bool UseUI = true;

    [XmlAttribute]
    public string Name;

    [XmlAttribute]
    public float Value;

    [XmlAttribute]
    public string Prefix;

    [XmlAttribute]
    public string Suffix;

    [XmlAttribute]
    public float MinValue = float.MinValue;

    [XmlAttribute]
    public float MaxValue = float.MaxValue;

    public const string UNIT_M = "M";
    public const string UNIT_KG = "Kg";

    public MPXProperty() { }

    public MPXProperty(MPXProperty p)
    {
        SetValue(p);
    }

    public override string ToString()
    {
        return string.Format("name: {0}, value: {1}", Name, Value);
    }

    public void SetValue(MPXProperty p)
    {
        ItemType = p.ItemType;
        UseUI = p.UseUI;
        Name = p.Name;

        Value = p.Value;
        Prefix = p.Prefix;
        Suffix = p.Suffix;
        MinValue = p.MinValue;
        MaxValue = p.MaxValue;
    }

    public void SetOnlyValue(MPXProperty p)
    {
        Value = p.Value;
        Prefix = p.Prefix;
        Suffix = p.Suffix;
        MinValue = p.MinValue;
        MaxValue = p.MaxValue;
    }
}
