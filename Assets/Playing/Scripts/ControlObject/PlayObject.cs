using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class PlayObject : ControlObject
{
    public List<PlayObject> targetList;
    public int TargetCount
    {
        get
        {
            if (targetList != null)
            {
                return targetList.Count;
            }
            return 0;
        }
    }


    public NavMeshAgent Agent;

    public Vector3 Target;
    public PlayObject TargetObj;

    public int TargetIndex;

    public bool IsStart;
    public bool IsNavigation;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private Quaternion startRot;

    public UnityEvent OnSelectStart = new UnityEvent();
    public UnityEvent UnSelect
    {
        get
        {
            if (MyObject != null && MyObject.SelectObj != null)
                return MyObject.SelectObj.UnSelected;
            return null;
        }
    }

    public static PlayObject SelectPlayObject;

    public NavigationPointer MyPointer;
    public GUIPlayTime GuiPlayTime;


    [SerializeField]
    private Vector3 centerForward;

    public UnityEvent<PlayObject> OnEndNavigation = new UnityEvent<PlayObject>();

    [SerializeField]
    private float navigationTime;
    public float NavigationTime { get { return navigationTime; } }
    public List<float> NavigationTimes = new List<float>();
    float timeTest = 0;

    void Start()
    {
        if (Agent == null)
        {
            Agent = gameObject.AddComponent<NavMeshAgent>();
            Agent.stoppingDistance = 0.2f;
        }


        if (MyPointer == null)
        {
            MyPointer = gameObject.AddComponent<NavigationPointer>();
            MyPointer.PlayObj = this;
        }

        Init();

        NavigationModeManager.Inst.OnChangeMode.AddListener(OnChangePlayMode);
        NavigationModeManager.Inst.OnExitNavigationMode.AddListener(OnExitNavigationMode);
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);

        if (MyObject != null && MyObject.SelectObj != null)
        {
            MyObject.SelectObj.OnSelected.AddListener(OnSelected);
            MyObject.SelectObj.UnSelected.AddListener(UnSelected);
        }

        centerForward = MyObject.GetCenterForward();
    }

    private void OnExitNavigationMode()
    {
        OnChangePlayMode(NavigationMode.FIRST);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        if (mode != EditMode.NAVIGATION)
        {
            HidePlayTime();
        } else
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
    }

    void ShowPlayTime()
    {
        if (GuiPlayTime == null)
        {
            GUINavigation navi = GUIManager.Inst.Get<GUINavigation>();
            GuiPlayTime = Instantiate(navi.GuiPlayTime);
            GuiPlayTime.transform.SetParent(GUIManager.Inst.Get<GUIPlaying>().PlayingNavigation.transform);
            GuiPlayTime.Target = gameObject;
        }

        GuiPlayTime.Show();
    }

    void HidePlayTime()
    {
        if (GuiPlayTime != null)
            GuiPlayTime.Hide();
    }

    void ShowTargetPointerAll()
    {
        if (targetList != null)
        {
            InputKey.Inst.IsFunc1 = true;
            for (int i = 0; i < targetList.Count; i++)
            {
                if (i == 0)
                {
                    targetList[i].MyPointer.ShowStartPointer();

                }
                else
                {
                    targetList[i].MyPointer.Show(i, targetList[i - 1].MyObject, targetList[i].MyObject);
                }
            }
            InputKey.Inst.IsFunc1 = false;
        }
    }

    void HideTargetPointerAll()
    {
        if (targetList != null)
        {
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].MyPointer.Hide();
            }
        }
    }

    private void UnSelected()
    {
        HideTargetPointerAll();
    }

    private void OnSelected()
    {
        if (!InputKey.Inst.IsFunc1)
        {
            startPos = transform.position;
            startRot = transform.rotation;
            
            SelectPlayObject = this;
            AddTarget(this);

            NavigationPlayer.Inst.Add(this);

            ShowTargetPointerAll();
        }
        else
        {
            if (SelectPlayObject != null)
            {
                SelectPlayObject.AddTarget(this);
                SelectPlayObject.ShowTargetPointerAll();
            }
        }
    }

    MPXProperty pSpeed;

    private void OnChangePlayMode(NavigationMode mode)
    {
        MyObject.SetLayer();

        if (TargetCount > 0 && Agent.isOnNavMesh)
        {
            if (mode == NavigationMode.PLAY)
            {
                IsNavigation = true;
                ShowPlayTime();

                pSpeed = MyObject.GetProperty("Speed");
                Agent.speed = pSpeed.Value;
                if (IsStart) NextTarget();
            }
            else if (mode == NavigationMode.PAUSE)
            {
            }
            else if (mode == NavigationMode.FIRST)
            {
            }
            else if (mode == NavigationMode.END)
            {
                if (IsStart) NextTarget();
            }
        }

        if (mode == NavigationMode.FIRST)
        {
            InitPlay();

            Agent.Warp(startPos);

            transform.rotation = startRot;
        }
    }

    RaycastHit hit;

    public const float FORWARD_DIST = 1f;

    void Update()
    {
        if (NavigationModeManager.Inst.CurrentMode == NavigationMode.PLAY)
        {
            if (IsNavigation)
            {
                if (TargetObj != null)
                {
                    if (Agent != null)
                    {
                        Agent.destination = Target;
                    }

                    centerForward = MyObject.GetCenterForward();
                    if (Physics.Raycast(centerForward, transform.forward, out hit, FORWARD_DIST))
                    {
                        if (hit.collider.GetComponent<PlayObject>() == TargetObj
                            || hit.collider.GetComponentInParent<PlayObject>() == TargetObj)
                        {
                            NavigationTimes.Add(timeTest);
                            timeTest = 0;
                            NextTarget();
                        }
                    }

                    navigationTime += Time.deltaTime;
                    timeTest += Time.deltaTime;

                    if (GuiPlayTime != null)
                    {
                        GuiPlayTime.SetText(navigationTime);
                    }
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(centerForward, 0.1f);
        Gizmos.DrawLine(centerForward, centerForward + transform.forward * FORWARD_DIST);
    }


    public override void Clear()
    {
        base.Clear();

        if (targetList != null)
        {
            targetList.Clear();
            NavigationPlayer.Inst.Remove(this);
        }
    }

    public int AddTarget(PlayObject obj)
    {
        if (targetList == null)
            targetList = new List<PlayObject>();

        if (!targetList.Contains(obj))
        {
            targetList.Add(obj);
            NavigationPlayer.Inst.Add(this);
        }

        return TargetCount;
    }


    void Init()
    {
        InitPlay();

        Clear();
    }

    void InitPlay()
    {
        IsNavigation = false;
        navigationTime = 0;

        if (NavigationTimes != null)
            NavigationTimes.Clear();
        else
            NavigationTimes = new List<float>();

        TargetIndex = 0;
        Target = Vector3.zero;
        TargetObj = null;
        IsStart = true;

        if (Agent.isOnNavMesh)
            Agent.isStopped = true;

        Agent.speed = 0;

        HidePlayTime();
    }


    void NextTarget()
    {
        if (Agent.isStopped)
            Agent.isStopped = false;

        if (targetList != null && targetList.Count > 0)
        {
            TargetIndex++;
            if (TargetIndex < targetList.Count)
            {
                TargetObj = targetList[TargetIndex];
                Target = TargetObj.transform.position;

                if (IsStart)
                    IsStart = false;
            }
            else if (TargetIndex >= TargetCount)
            {
                Agent.isStopped = true;
                IsNavigation = false;

                HidePlayTime();
                OnEndNavigation.Invoke(this);
            }
        }
    }
}
