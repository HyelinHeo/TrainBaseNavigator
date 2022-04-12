using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputMouse : Singleton<InputMouse>
{
    private Camera myCamera;

    public UnityEvent<GameObject> OnRaycastHit = new UnityEvent<GameObject>();
    public UnityEvent OnRaycastNoHit = new UnityEvent();
    public UnityEvent OnClickNoHit = new UnityEvent();
    public UnityEvent<GameObject> OnClickHit = new UnityEvent<GameObject>();

    void Awake()
    {
        CameraModeManager.Inst.OnChangeMode.AddListener(OnChangeCamera);
    }

    private void OnChangeCamera(CameraMode mode)
    {
        if (MPXCamera.CurrentCamera != null)
        {
            myCamera = MPXCamera.CurrentCamera.MyCam;
        }
    }

    RaycastHit hit;
    Vector3 mos;
    Vector3 dir;
    Vector3 hitPos;

    private LayerMask IgnoreLayer = ~(1 << 2);

    public Vector3 HitPos { get { return hitPos; } }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && myCamera != null)
        {
            mos = Input.mousePosition;
            mos.z = myCamera.farClipPlane;

            dir = myCamera.ScreenToWorldPoint(mos);
            if (Physics.Raycast(myCamera.transform.position, dir, out hit, mos.z, IgnoreLayer))
            {
                hitPos = hit.point;
                OnRaycastHit.Invoke(hit.collider.gameObject);
                if (Input.GetMouseButtonUp(0))
                {
                    OnClickHit.Invoke(hit.collider.gameObject);
                }
            }
            else
            {
                OnRaycastNoHit.Invoke();
                if (Input.GetMouseButtonUp(0))
                {
                    OnClickNoHit.Invoke();
                }

            }
        }
    }
}
