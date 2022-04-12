using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CreatorPutObject : CreatorPlacement
{
    /// <summary>
    /// 오브젝트를 놓았을 때 이벤트
    /// </summary>
    public UnityEvent OnPutObject = new UnityEvent();
    public Vector3 InputPos = Vector3.zero;

    protected override void Start()
    {
        base.Start();

        MyInputMouse = InputMouse.Inst;
        InputKey.Inst.OnKeyDownCancel.AddListener(OnKeyDownCancel);
    }

    private void OnKeyDownCancel()
    {
        DestoryObject();
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        Prefab = null;
        OnPutObject.Invoke();
    }

    public override MPXObject Create()
    {
        MPXObject go = base.Create();
        go.Init();

        return go;
    }


    public override MPXObject CreatePreview()
    {
        if (InputPos != Vector3.zero)
        {
            if (CurrentObject == null)
            {
                CurrentObject = base.CreatePreview();
            }
            else
            {
                CurrentObject.Draw();
            }
            CurrentObject.transform.position = InputPos;
        }
        return CurrentObject;
    }

    public void SetPrefab(MPXObject obj)
    {
        DestoryObject();
        Init();

        Prefab = obj;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (UseCreator /*&& PlacementModeManager.Inst.CurrentMode == PlacementMode.PLACEMENT*/)
        {
            if (Prefab != null)
            {
                InputPos = MyInputMouse.HitPos;

                CreatePreview();

                if (Input.GetMouseButtonUp(0))
                {
                    Create();
                    Init();
                }
            }
        }
    }

    public override MPXObject Create(List<MPXProperty> p)
    {
        return base.Create(p);
    }
}
