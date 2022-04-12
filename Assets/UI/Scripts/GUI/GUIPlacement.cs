using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUIPlacement : GUIWindow
{
    public CreatorPutObject CreatorPut;
    private MPXObject selectObject;
    public UnityEvent<MPXObject> OnChangeSelect = new UnityEvent<MPXObject>();

    public GUIPlacementCategory Category;
    public GUIPlacementItemList Items;


    private void Start()
    {
        //CreatorPut을 동적으로 넣기위한 하드코딩_20201018 수정필요
        CreatorPut = GameObject.FindObjectOfType<CreatorPutObject>() as CreatorPutObject;
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public void Select(MPXObject go)
    {
        //if (selectObject != go)
        //{
        selectObject = go;

        CreatorPut.SetPrefab(selectObject);
        OnChangeSelect.Invoke(go);
        //}
    }
}
