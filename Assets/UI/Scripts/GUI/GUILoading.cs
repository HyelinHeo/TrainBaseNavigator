using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILoading : GUIWindow
{
    public Image bar;
    public Text loadingText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    [Range(0, 1f)] public float vignetteEfectVolue; // Must be a value between 0 and 1
    public Image vignetteEfect;

    public float ProgressValue;


    private void Start()
    {
        //vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r, vignetteEfect.color.g, vignetteEfect.color.b, vignetteEfectVolue);

    }


    IEnumerator transitionImage()
    {
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            yield return new WaitForSeconds(LoopTime);
            for (int j = 0; j < backgroundImages.Length; j++)
                backgroundImages[j].SetActive(false);
            backgroundImages[i].SetActive(true);
        }
    }


    public override void Init()
    {
        base.Init();
    }

    public override void Show()
    {
        base.Show();

        ProgressValue = 0;

        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void ChangeState()
    {
        base.ChangeState();
    }

    public void SetProgressValue(float val)
    {
        ProgressValue = val;

        SetProgressValue();
    }

    void SetProgressValue()
    {
        loadingText.text = string.Format("{0:0.0}%", (ProgressValue * 100));
        bar.fillAmount = ProgressValue;
    }

}
