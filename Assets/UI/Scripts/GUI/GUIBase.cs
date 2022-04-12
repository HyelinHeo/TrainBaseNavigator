using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GUIBase : MonoBehaviour
{
    public Animation MyAnim;
    public GameObject MYPanel;

    public UnityEvent OnShowEndAnimation = new UnityEvent();
    public UnityEvent OnHideEndAnimation = new UnityEvent();

    public UnityEvent OnShow = new UnityEvent();
    public UnityEvent OnHide = new UnityEvent();

    [SerializeField]
    private bool isShow = true;
    public bool IsShow { get { return isShow; } }

    private void Start()
    {
        Init();
    }

    public virtual void Show()
    {
        if (MyAnim != null)
        {
            StartCoroutine(ShowAnim());
        }
        if (MYPanel != null)
        {
            MYPanel.SetActive(true);
        }
        isShow = true;

        OnShow.Invoke();
    }

    public virtual void Hide()
    {
        if (MyAnim != null)
        {
            StartCoroutine(HideAnim());
        }
        if (MYPanel != null)
        {
            MYPanel.SetActive(false);
        }
        isShow = false;

        OnHide.Invoke();
    }
    public virtual void Init()
    {
        //if (MYPanel != null)
        //{
        //    MYPanel.SetActive(true);
        //}
    }

    public virtual void ChangeState()
    {
        if (isShow)
        {
            StartCoroutine(HideAnim());
        }
        else
        {
            StartCoroutine(ShowAnim());
        }
        isShow = !isShow;
    }

    private IEnumerator ShowAnim()
    {
        MyAnim.Play("Show");

        float len = MyAnim.GetClip("Show").length;
        yield return new WaitForSeconds(len);
        OnShowEndAnimation.Invoke();
    }

    private IEnumerator HideAnim()
    {
        MyAnim.Play("Hide");

        float len = MyAnim.GetClip("Hide").length;
        yield return new WaitForSeconds(len);
        OnHideEndAnimation.Invoke();
    }
}
