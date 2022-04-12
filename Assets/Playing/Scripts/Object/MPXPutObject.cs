using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MPXPutObject : MPXObject
{
    public RotateObject Rot;
    public NavMeshAgent Agent;
    public PlayObject PlayObj;
    public MoveObject MoveObj;

    public const string MY_LAYER = "EditObject";


    private void Awake()
    {
        EditModeManager.Inst.OnChangeMode.AddListener(InitChangeEditMode);
    }

    public override void Init()
    {
        MyLayer = LayerMask.NameToLayer(MY_LAYER);

        if (Col == null)
            Col = GetComponentInChildren<Collider>();

        AttachSelectScript();
        Rigid = SelectObj.gameObject.GetComponent<Rigidbody>();
        if (Rigid == null)
        {
            Rigid = SelectObj.gameObject.AddComponent<Rigidbody>();
            Rigid.constraints = RigidbodyConstraints.FreezeAll;
        }
        base.Init();

        if (Rot == null)
        {
            Rot = gameObject.AddComponent<RotateObject>();
            Rot.MyObject = this;
        }

        if (MoveObj == null)
        {
            MoveObj = gameObject.AddComponent<MoveObject>();
            MoveObj.MyObject = this;
        }

        if (PlayObj == null)
        {
            PlayObj = gameObject.AddComponent<PlayObject>();
            PlayObj.MyObject = this;
        }

        InitChangeEditMode(EditModeManager.Inst.CurrentMode);
    }

    public override void InitChangeEditMode(EditMode mode)
    {
        if (Rot != null && MoveObj != null && PlayObj != null)
        {
            switch (mode)
            {
                case EditMode.PLACEMENT:
                    Rot.enabled = true;
                    MoveObj.enabled = true;
                    PlayObj.enabled = false;
                    break;
                case EditMode.NAVIGATION:
                    Rot.enabled = false;
                    MoveObj.enabled = false;
                    PlayObj.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }


}
