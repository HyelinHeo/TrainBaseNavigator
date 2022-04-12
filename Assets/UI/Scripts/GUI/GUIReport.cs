using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIReport : GUIWindow
{
    public GUIReportItemList GuiReportItemList;
    //prefabs
    public GUIReportTotalTime GuiReportTotalTime;
    public GUIReportHistory GuiReportHistory;


    void Start()
    {
        Debug.Log(this.name);
        NavigationModeManager.Inst.OnExitNavigationMode.AddListener(OnExitNavigationMode);
        NavigationPlayer.Inst.OnEndNavigationAll.AddListener(OnEndNavigation);
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        GuiReportItemList.Clear();
    }

    public override void Init()
    {
        base.Init();
        Hide();
    }

    private void OnExitNavigationMode()
    {
        Hide();
    }

    //private void OnSelectStartPlayObj()
    //{
    //    PlayObject.SelectPlayObject.OnEndNavigation.AddListener(OnEndNavigation);
    //}

    //private void OnChangeSelectPlayObj(PlayObject obj)
    //{
    //    obj.OnEndNavigation.RemoveListener(OnEndNavigation);
    //}

    private void OnEndNavigation(PlayObject obj)
    {
        GUIReportTotalTime reportTime = GuiReportItemList.InstantiateGUI(GuiReportTotalTime);
        reportTime.SetText(obj.NavigationTime);
        //0번째는 나 자신이므로 제외
        for (int i = 1; i < obj.TargetCount; i++)
        {
            GUIReportHistory reportHistory = GuiReportItemList.InstantiateGUI(GuiReportHistory);
            reportHistory.SetText(obj.targetList[i].MyObject.PrefabName, obj.NavigationTimes[i - 1]);
        }

        Show();
    }

    private void OnDestroy()
    {
        NavigationModeManager.Inst.OnExitNavigationMode.RemoveListener(OnExitNavigationMode);
    }
}
