using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;


public class XMLMPXObject : XMLObject
{
    [XmlAttribute]
    public string ID;

    [XmlAttribute]
    public string Name;

    [XmlAttribute]
    public EditMode EditType;

    [XmlAttribute]
    public FoundationMode FoundationType;

    public Vector3 Position;
    public Vector3 Size;
    public Vector3 Rotation;

    public List<MPXProperty> Properties;

    public string PrefabName;

    public XMLMPXObject() { }

    public virtual void SetValue(MPXObject obj)
    {
        if (obj is MPXPutObject)
        {
            EditType = EditMode.PLACEMENT;
            FoundationType = FoundationMode.NONE;
        }
        else if (obj is MPXMesh)
        {
            EditType = EditMode.FOUNDATION;
            FoundationType = FoundationMode.Wall;
        }
        else if (obj is MPXPlane)
        {
            EditType = EditMode.FOUNDATION;
            FoundationType = FoundationMode.Plane;
        }

        ID = obj.ID;
        Name = obj.name;
        Position = obj.transform.position;
        Rotation = obj.transform.eulerAngles;
        Size = obj.transform.localScale;
        PrefabName = obj.PrefabName;

        if (obj.Properties != null)
            Properties = obj.Properties;
    }
}
