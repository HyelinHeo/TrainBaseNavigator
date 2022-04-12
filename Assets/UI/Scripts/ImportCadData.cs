using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImportCadData : MonoBehaviour
{
    public MPXPlane mpxPlane;
    public Material CadDataMat;

    private MeshRenderer MyRenderer;

    private Material defaultMaterial;
    bool isChange = false;

    private void Start()
    {
        MyRenderer = this.gameObject.GetComponent<MeshRenderer>();
        defaultMaterial = MyRenderer.material;
    }

    private void Update()
    {
        //cad image on/off
        if (Input.GetKeyUp(KeyCode.F2))
        {
            Change();
        }
    }

    private void Change()
    {
        if (isChange)
            RevertPlaneMaterial();
        else
            ChangePlaneMaterial();

        isChange = !isChange;
    }

    void ChangePlaneMaterial()
    {
        if (CadDataMat != null)
        {
            MyRenderer.material = CadDataMat;
        }
        else
            Debug.LogError("not found : change plane material");
    }

    public void RevertPlaneMaterial()
    {
        if (defaultMaterial != null)
        {
            MyRenderer.material = defaultMaterial;
        }
        else
            Debug.LogError("not found : default plane material");
    }
}
