using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectEffect : MonoBehaviour
{
    public Outline SelectOutline;
    public SelectObject SelectObj;

    private void Start()
    {
        CameraModeManager.Inst.OnChangeMode.AddListener(OnChangeCamera);
        SelectObj.OnSelected.AddListener(OnSelected);
        SelectObj.UnSelected.AddListener(UnSelected);
        OutlineEffectInit();
        

        GUIManager.Inst.Get<GUIProperty>().OnItemControlMouseStart.AddListener(OnStartSlider);
        GUIManager.Inst.Get<GUIProperty>().OnItemControlMouseEnd.AddListener(OnEndSlider);
    }

    /// <summary>
    /// 현재 선택된 개체만 아웃라인 효과 refresh
    /// </summary>
    private void OnEndSlider()
    {
        if (SelectOutline.enabled)
        {
            on();
        }
    }

    private void on()
    {
        SelectOutline.Refresh();
    }

    private void OnStartSlider()
    {
        off();
    }

    private void off()
    {
        //SelectOutline.Refresh();
    }

    private void OnChangeCamera(CameraMode mode)
    {
        switch (mode)
        {
            case CameraMode.NONE:
                break;
            case CameraMode.CAM_2D:
                SelectEffectCamera2D();
                break;
            case CameraMode.CAM_3D:
                SelectEffectCamera3D();
                break;
            default:
                break;
        }
    }

    private void OnSelected()
    {
        SelectOutline.enabled = true;
        on();
    }

    private void UnSelected()
    {
        SelectOutline.enabled = false;
    }

    /// <summary>
    /// 아웃라인 효과 초기화
    /// Initialization effect which called 'outline'
    /// </summary>
    public void OutlineEffectInit()
    {
        //Init 20201014_HL
        if (CameraModeManager.Inst.CurrentMode==CameraMode.CAM_2D)
        {
            SelectEffectCamera2D();
        }
        else if (CameraModeManager.Inst.CurrentMode == CameraMode.CAM_3D)
        {
            SelectEffectCamera3D();
        }
        SelectOutline.OutlineMode = Outline.Mode.OutlineVisible;
        SelectOutline.enabled = false;
    }

    /// <summary>
    /// 선택시 효과(2D camera)
    /// effect when select object(2D camera)
    /// </summary>
    public void SelectEffectCamera2D()
    {
        SelectOutline.OutlineColor = Color.yellow;
        SelectOutline.OutlineWidth = 0.5f;
    }

    /// <summary>
    /// 선택시 효과(3D camera)
    /// effect when select object(3D camera)
    /// </summary>
    public void SelectEffectCamera3D()
    {
        SelectOutline.OutlineColor = Color.yellow;
        SelectOutline.OutlineWidth = 8.0f;
    }
    
    /// <summary>
    /// 마우스 오버 시 효과(2D camera)
    /// effect when mouse over(2D camera)
    /// </summary>
    public void MouseOverEffectCamera2D()
    {
        SelectOutline.OutlineColor = Color.green;
        SelectOutline.OutlineWidth = 0.5f;
    }

    /// <summary>
    /// 마우스 오버 시 효과(3D camera)
    /// effect when mouse over(3D camera)
    /// </summary>
    public void MouseOverEffectCamera3D()
    {
        SelectOutline.OutlineColor = Color.green;
        SelectOutline.OutlineWidth = 5.0f;
    }

    private void OnDestroy()
    {
        SelectObj.OnSelected.RemoveListener(OnSelected);
        SelectObj.UnSelected.RemoveListener(UnSelected);
    }

}
