using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : ControlObject
{
    public float Speed = 10f;
    public bool Run;

    void Start()
    {

    }

    public float wheel;
    void Update()
    {
        wheel = Input.GetAxis("Mouse ScrollWheel");
        if (InputKey.Inst.IsFunc2)
        {
            Run = true;
        }
        else
        {
            Run = false;
        }

        //if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        //{
        //    Run = true;
        //}
        //else
        //{
        //    Run = false;
        //}

        if (Run)
        {
            //if (Input.GetMouseButton(1))
            //{
            //    transform.Rotate(Vector3.up * Time.deltaTime * Speed);
            //}
        }
    }
}
