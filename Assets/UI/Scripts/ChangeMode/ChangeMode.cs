using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeMode<T> : MonoBehaviour
{
    public T MyMode;

    public virtual void Change() { }
}
