﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatorWall : CreatorFoundation
{
    /// <summary>
    /// 초기값 두께
    /// </summary>
    public float Thickness;

    /// <summary>
    /// 초기값 높이
    /// </summary>
    public float Height;

    public Vector3 StartPosition;
    public Vector3 EndPosition;

    [SerializeField]
    private bool isStart;

    public MPXMesh CurrentMesh;

    public GUIProperty GuiProperty;


    public enum NearAxis
    {
        NONE,
        X,
        Z,
    }

    protected override void Start()
    {
        base.Start();
        GuiProperty = GUIManager.Inst.Get<GUIProperty>();

        GuiProperty.OnHide.AddListener(GUIPropertyOnHide);

        InputKey.Inst.OnKeyDownCancel.AddListener(OnKeyDownCancel);
    }

    private void OnKeyDownCancel()
    {
        if (isStart)
        {
            DestoryObject();
            Init();

            isStart = false;
        }
    }

    private void GUIPropertyOnHide()
    {
        SelectObject.ClearSelectObjects();
    }

    public override void Init()
    {
        base.Init();

        MyMode = FoundationMode.Wall;

        CurrentMesh = null;
        StartPosition = Vector3.zero;
        EndPosition = Vector3.zero;

        isStart = false;
    }

    protected override void InitProperties()
    {
        MPXProperty p1 = new MPXProperty();
        p1.Name = MPXMesh.PROPERTY_THICKNESS;
        p1.Value = Thickness;
        p1.MinValue = 0.01f;
        p1.MaxValue = 10f;
        p1.Suffix = MPXProperty.UNIT_M;

        Add(p1);

        MPXProperty p2 = new MPXProperty();
        p2.Name = MPXMesh.PROPERTY_HEIGHT;
        p2.Value = Height;
        p2.MinValue = 0.01f;
        p2.MaxValue = 10f;
        p2.Suffix = MPXProperty.UNIT_M;

        Add(p2);

        MPXProperty startPosx = new MPXProperty();
        startPosx.Name = MPXMesh.PROPERTY_START_POS_X;
        startPosx.Value = StartPosition.x;
        startPosx.UseUI = false;

        Add(startPosx);

        MPXProperty startPosy = new MPXProperty();
        startPosy.Name = MPXMesh.PROPERTY_START_POS_Y;
        startPosy.Value = StartPosition.y;
        startPosy.UseUI = false;

        Add(startPosy);

        MPXProperty startPosz = new MPXProperty();
        startPosz.Name = MPXMesh.PROPERTY_START_POS_Z;
        startPosz.Value = StartPosition.z;
        startPosz.UseUI = false;

        Add(startPosz);

        MPXProperty endPosx = new MPXProperty();
        endPosx.Name = MPXMesh.PROPERTY_END_POS_X;
        endPosx.Value = EndPosition.x;
        endPosx.UseUI = false;

        Add(endPosx);

        MPXProperty endPosy = new MPXProperty();
        endPosy.Name = MPXMesh.PROPERTY_END_POS_Y;
        endPosy.Value = EndPosition.y;
        endPosy.UseUI = false;

        Add(endPosy);

        MPXProperty endPosz = new MPXProperty();
        endPosz.Name = MPXMesh.PROPERTY_END_POS_Z;
        endPosz.Value = EndPosition.z;
        endPosz.UseUI = false;

        Add(endPosz);

        Debug.Log("InitProperties: " + gameObject);
    }

    public readonly float NEAR_UNIT = 0.3f;

    private NearAxis CheckNearAxis(Vector3 dir)
    {
        if (Mathf.Abs(Vector3.Dot(Vector3.forward, dir)) < NEAR_UNIT)
        {
            return NearAxis.Z;
        }
        else if (Mathf.Abs(Vector3.Dot(Vector3.right, dir)) < NEAR_UNIT)
        {
            return NearAxis.X;
        }

        return NearAxis.NONE;
    }

    public override void SetValue(MPXProperty p)
    {
        GetProperty(p.Name).Value = p.Value;

        switch (p.Name)
        {
            case MPXMesh.PROPERTY_THICKNESS:
                Thickness = p.Value;
                break;
            case MPXMesh.PROPERTY_HEIGHT:
                Height = p.Value;
                break;
            case MPXMesh.PROPERTY_START_POS_X:
                StartPosition.x = p.Value;
                break;
            case MPXMesh.PROPERTY_START_POS_Y:
                StartPosition.y = p.Value;
                break;
            case MPXMesh.PROPERTY_START_POS_Z:
                StartPosition.z = p.Value;
                break;
            case MPXMesh.PROPERTY_END_POS_X:
                EndPosition.x = p.Value;
                break;
            case MPXMesh.PROPERTY_END_POS_Y:
                EndPosition.y = p.Value;
                break;
            case MPXMesh.PROPERTY_END_POS_Z:
                EndPosition.z = p.Value;
                break;
            default:
                break;
        }
    }


    Vector3 inputPos;
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (UseCreator)
        {

            inputPos = MyInputMouse.HitPos;
            if (Input.GetMouseButtonDown(0))
            {
                if (!isStart)
                {
                    StartPosition = inputPos;
                    isStart = true;
                }
                else
                {
                    EndPosition = inputPos;

                    Create();
                    Init();
                }
            }
            else
            {
                if (isStart)
                {
                    EndPosition = inputPos;

                    CreatePreview();
                }
            }
        }
    }

    public override void DestoryObject()
    {
        base.DestoryObject();
    }

    public override MPXObject Create()
    {
        MPXObject obj = base.Create();

        // 생성 후 바로 속성창에서 수정 가능하게 하기 위함
        obj.SelectObj.Select();

        return obj;
    }

    public override MPXObject CreatePreview()
    {
        if (StartPosition != EndPosition)
        {
            StartPosition.y = 0;
            EndPosition.y = 0;

            // 스냅 기능
            NearAxis axis = CheckNearAxis(Vector3.Normalize(EndPosition - StartPosition));

            if (axis == NearAxis.Z)
            {
                EndPosition.z = StartPosition.z;
            }
            else if (axis == NearAxis.X)
            {
                EndPosition.x = StartPosition.x;
            }

            if (CurrentObject == null)
            {
                SetValue(MPXMesh.PROPERTY_THICKNESS, Thickness);
                SetValue(MPXMesh.PROPERTY_HEIGHT, Height);

                CurrentObject = base.CreatePreview();
                CurrentMesh = CurrentObject.GetComponent<MPXMesh>();
            }

            SetValue(MPXMesh.PROPERTY_START_POS_X, StartPosition.x);
            SetValue(MPXMesh.PROPERTY_START_POS_Y, StartPosition.y);
            SetValue(MPXMesh.PROPERTY_START_POS_Z, StartPosition.z);
            SetValue(MPXMesh.PROPERTY_END_POS_X, EndPosition.x);
            SetValue(MPXMesh.PROPERTY_END_POS_Y, EndPosition.y);
            SetValue(MPXMesh.PROPERTY_END_POS_Z, EndPosition.z);

            CurrentMesh.StartPos = StartPosition;
            CurrentMesh.EndPos = EndPosition;
            CurrentMesh.Draw();
        }

        return CurrentMesh;
    }

    public override MPXObject Create(List<MPXProperty> p)
    {
        SetPropertiesOnlyValue(p);

        Thickness = GetProperty(MPXMesh.PROPERTY_THICKNESS).Value;
        Height = GetProperty(MPXMesh.PROPERTY_HEIGHT).Value;

        StartPosition.x = GetProperty(MPXMesh.PROPERTY_START_POS_X).Value;
        StartPosition.y = GetProperty(MPXMesh.PROPERTY_START_POS_Y).Value;
        StartPosition.z = GetProperty(MPXMesh.PROPERTY_START_POS_Z).Value;
        EndPosition.x = GetProperty(MPXMesh.PROPERTY_END_POS_X).Value;
        EndPosition.y = GetProperty(MPXMesh.PROPERTY_END_POS_Y).Value;
        EndPosition.z = GetProperty(MPXMesh.PROPERTY_END_POS_Z).Value;

        return Create();
    }

}