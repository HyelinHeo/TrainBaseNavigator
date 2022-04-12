using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlaying : GUIPlayWindow
{
    public Transform PlayingFoundation;
    public Transform PlayingPlacement;
    public Transform PlayingNavigation;

    public override void Init()
    {
        base.Init();
        EditModeManager.Inst.OnChangeMode.AddListener(OnChangeEditMode);
    }

    private void OnChangeEditMode(EditMode mode)
    {
        switch (mode)
        {
            case EditMode.DEFAULT:
                PlayingFoundation.gameObject.SetActive(false);
                PlayingPlacement.gameObject.SetActive(false);
                PlayingNavigation.gameObject.SetActive(false);
                break;
            case EditMode.FOUNDATION:
                PlayingFoundation.gameObject.SetActive(true);
                PlayingPlacement.gameObject.SetActive(false);
                PlayingNavigation.gameObject.SetActive(false);
                break;
            case EditMode.PLACEMENT:
                PlayingFoundation.gameObject.SetActive(false);
                PlayingPlacement.gameObject.SetActive(true);
                PlayingNavigation.gameObject.SetActive(false);
                break;
            case EditMode.NAVIGATION:
                PlayingFoundation.gameObject.SetActive(false);
                PlayingPlacement.gameObject.SetActive(false);
                PlayingNavigation.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
