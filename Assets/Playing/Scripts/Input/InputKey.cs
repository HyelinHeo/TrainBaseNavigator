using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputKey : Singleton<InputKey>
{
    public bool IsFunc1;
    public bool IsFunc2;

    public bool IsArrowUp;

    public UnityEvent OnKeyDownDelete = new UnityEvent();
    public UnityEvent OnKeyDownCancel = new UnityEvent();


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            IsFunc1 = true;
        } else
        {
            IsFunc1 = false;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            IsFunc2 = true;
        }
        else
        {
            IsFunc2 = false;
        }


        if (Input.GetKeyDown(KeyCode.Delete))
        {
            OnKeyDownDelete.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonUp(1))
        {
            OnKeyDownCancel.Invoke();
        }

      
        if (Input.GetMouseButtonDown(1))
        {
            IsArrowUp = true;
        } else
        {
            IsArrowUp = false;
        }
    }
}
