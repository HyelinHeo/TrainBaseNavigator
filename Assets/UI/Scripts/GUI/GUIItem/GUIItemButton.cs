using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GUIItemButton : GUIItem
{
    public Button Btn;
    public Image BtnImage;
    public Text BtnText;

    public UnityEvent OnClick { get { return Btn.onClick; } }

    public void SetText(string txt)
    {
        BtnText.text = txt;
    }

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
    }
}
