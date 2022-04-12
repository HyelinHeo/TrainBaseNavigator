using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 시뮬레이션 하기 위한 상태 정의
/// </summary>
public enum NavigationMode
{
    DEFAULT,
    PLAY,
    PAUSE,
    FIRST,
    END
}

public class NavigationModeManager : ModeManager<NavigationModeManager, NavigationMode>
{
    public UnityEvent OnExitNavigationMode = new UnityEvent();

    private void Start()
    {
        currentMode = NavigationMode.DEFAULT;
        ChangeMode(NavigationMode.DEFAULT);
    }

    public override void ChangeMode(NavigationMode type)
    {
        base.ChangeMode(type);

    }
}
