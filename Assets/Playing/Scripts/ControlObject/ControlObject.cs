using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObject : MonoBehaviour
{
    public MPXObject MyObject;

    public virtual void Clear() { }

    private void OnDestroy()
    {
        Clear();
    }
}
