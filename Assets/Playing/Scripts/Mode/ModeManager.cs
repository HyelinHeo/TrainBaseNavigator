using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class ModeManager<T1, T2> : MonoBehaviour where T1 : MonoBehaviour
{
    private static T1 g_Instance = default(T1);
    public static T1 Inst
    {
        get
        {
            if (g_Instance == null)
            {
                T1 t = GameObject.FindObjectOfType(typeof(T1)) as T1;
                if (t == null)
                {
                    string strName = typeof(T1).ToString();
                    GameObject go = new GameObject(string.Format("[{0}]", strName));
                    g_Instance = go.AddComponent<T1>();
                }
                else
                {
                    g_Instance = t;
                }
            }

            return g_Instance;
        }
    }

    [SerializeField]
    protected T2 currentMode;
    public T2 CurrentMode { get { return currentMode; } }

    public UnityEvent<T2> OnChangeMode = new UnityEvent<T2>();

    public virtual void ChangeMode(T2 mode)
    {
        if (!currentMode.Equals(mode))
        {
            currentMode = mode;
            OnChangeMode.Invoke(CurrentMode);
        }
    }

 
    private void OnDestroy()
    {
        if (g_Instance != null)
            Destroy(g_Instance);

        g_Instance = null;
    }
}
