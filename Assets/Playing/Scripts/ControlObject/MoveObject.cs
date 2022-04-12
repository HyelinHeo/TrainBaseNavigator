using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MoveObject : ControlObject
{
    private SelectObject select;

    private InputMouse MyInput;

    [SerializeField]
    private bool isMove;

    void Start()
    {
        MyInput = InputMouse.Inst;
        if (MyObject != null)
            select = MyObject.SelectObj;

        MyInput.OnRaycastHit.AddListener(OnRaycastHit);

        isMove = false;
    }

    private void OnRaycastHit(GameObject go)
    {
        if (EditModeManager.Inst.CurrentMode == EditMode.PLACEMENT)
        {
            if (select != null && select.IsSelect)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (!isMove)
                    {
                        isMove = true;
                    }
                    else
                    {
                        isMove = false;
                        select.UnSelect();
                    }
                }
            }
        }
    }

    void Update()
    {
        if (select != null && EditModeManager.Inst.CurrentMode == EditMode.PLACEMENT && select.IsSelect)
        {
            if (isMove)
            {
                transform.position = MyInput.HitPos;
            }
        }
    }
}
