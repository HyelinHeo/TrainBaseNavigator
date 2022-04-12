using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUINavigation : GUIWindow
{
    public Image BtnFirstImg;
    public Image BtnPlayImg;
    public Image BtnPauseImg;
    public Image BtnEndImg;

    public GUIPlayPoint GuiPlayPoint;
    public GUIPlayTime GuiPlayTime;


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
        NavigationModeManager.Inst.OnChangeMode.AddListener(OnChangePlayMode);
    }


    private void OnChangePlayMode(NavigationMode mode)
    {
        switch (mode)
        {
            case NavigationMode.DEFAULT:
                BtnFirstImg.color = DefaultColor;
                BtnPlayImg.color = DefaultColor;
                BtnPauseImg.color = DefaultColor;
                BtnEndImg.color = DefaultColor;
                break;
            case NavigationMode.PLAY:
                BtnFirstImg.color = DefaultColor;
                BtnPlayImg.color = HighlightColor;
                BtnPauseImg.color = DefaultColor;
                BtnEndImg.color = DefaultColor;
                break;
            case NavigationMode.PAUSE:
                BtnFirstImg.color = DefaultColor;
                BtnPlayImg.color = DefaultColor;
                BtnPauseImg.color = HighlightColor;
                BtnEndImg.color = DefaultColor;
                break;
            case NavigationMode.FIRST:
                BtnFirstImg.color = HighlightColor;
                BtnPlayImg.color = DefaultColor;
                BtnPauseImg.color = DefaultColor;
                BtnEndImg.color = DefaultColor;
                break;
            case NavigationMode.END:
                BtnFirstImg.color = DefaultColor;
                BtnPlayImg.color = DefaultColor;
                BtnPauseImg.color = DefaultColor;
                BtnEndImg.color = HighlightColor;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        NavigationModeManager.Inst.OnChangeMode.RemoveListener(OnChangePlayMode);
    }
}
