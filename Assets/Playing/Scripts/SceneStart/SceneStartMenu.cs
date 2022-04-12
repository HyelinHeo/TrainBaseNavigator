using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneStartMenu : SceneStart<SceneStartMenu>
{
    /// <summary>
    /// 메뉴 씬 로딩 완료될 때 호출
    /// 현재는 로딩할 오브젝트가 없으므로 start()에서 처리
    /// </summary>
    public UnityEvent OnStartProcessComplete = new UnityEvent();

    private void Start()
    {
        OnStartProcessComplete.Invoke();
    }
}
