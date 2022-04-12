using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

public class XMLSaveFile : XMLObject, IMPXList<XMLMPXObject>
{
    public List<XMLMPXObject> Objects;

    public XMLSaveFile() { }

    public int Count { get { if (Objects != null) { return Objects.Count; } return 0; } }

    public void Add(XMLMPXObject obj)
    {
        if (Objects == null)
            Objects = new List<XMLMPXObject>();

        Objects.Add(obj);
    }

    public void Add(MPXObject obj)
    {
        XMLMPXObject xml = new XMLMPXObject();
        xml.SetValue(obj);

        Add(xml);
    }

    public void Clear()
    {
        if (Objects != null)
        {
            Objects.Clear();
        }
    }

    public bool Remove(XMLMPXObject item)
    {
        if (Objects != null)
        {
            return Objects.Remove(item);
        }
        return false;
    }
}
