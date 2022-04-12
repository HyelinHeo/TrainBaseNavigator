using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObject : MonoBehaviour
{
    public InputMouse MouseInput;

    public MPXPutObject SelectObject;

    void Start()
    {
    }

    private void Init()
    {
        SelectObject = null;
    }

    private void OnMoveMouse(Vector3 pos)
    {
        if (SelectObject != null)
        {
            SelectObject.transform.position = pos;
            if (Input.GetMouseButton(0))
            {
                SelectObject = null;
            }
        }
    }
}
