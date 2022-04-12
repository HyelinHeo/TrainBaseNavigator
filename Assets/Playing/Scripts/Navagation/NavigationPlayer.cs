using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class NavigationPlayer : Singleton<NavigationPlayer>, IMPXList<PlayObject>
{
    public NavigationBaker naviBaker;

    public UnityEvent<PlayObject> OnEndNavigationAll = new UnityEvent<PlayObject>();

    List<PlayObject> targetList;

    public int CurrentIndex;

    public int Count => throw new NotImplementedException();

    void Start()
    {
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
        NavigationModeManager.Inst.OnChangeMode.AddListener(OnChangeNaviMode);
        NavigationModeManager.Inst.OnExitNavigationMode.AddListener(OnExitNavigationMode);
    }

    private void OnExitNavigationMode()
    {
        OnChangeNaviMode(NavigationMode.FIRST);
    }

    private void OnChangeNaviMode(NavigationMode mode)
    {
        switch (mode)
        {
            case NavigationMode.PLAY:
                Time.timeScale = 1;
                break;
            case NavigationMode.PAUSE:
                Time.timeScale = 0;
                break;
            case NavigationMode.FIRST:
                Time.timeScale = 1;
                break;
            case NavigationMode.END:
                Time.timeScale = 10;

                break;
            default:
                break;
        }
    }

    private void OnChangeEditMode(EditMode mode)
    {
        if (mode == EditMode.NAVIGATION)
        {
            Debug.Log("Bake");
            naviBaker.Bake();
        }
    }

    public void Play()
    {

    }

    public void Add(PlayObject item)
    {
        if (targetList == null)
            targetList = new List<PlayObject>();

        if (!targetList.Contains(item))
        {
            targetList.Add(item);
            item.OnEndNavigation.AddListener(OnEndNavigationPlayObject);
        }

    }

    private void OnEndNavigationPlayObject(PlayObject obj)
    {
        OnEndNavigationAll.Invoke(obj);
    }

    public void Clear()
    {
        if (targetList != null)
        {
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].Clear();
            }
            targetList.Clear();
        }
    }

    public bool Remove(PlayObject item)
    {
        if (targetList != null)
        {
            item.OnEndNavigation.RemoveListener(OnEndNavigationPlayObject);
            return targetList.Remove(item);
        }
        return false;
    }
}
