using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GUIMessageBox : GUIWindow
{
    public Text TxtTitle;
    public Text TxtBody;

    public GUIItemButton[] buttons;

    [SerializeField]
    private MessageButtonType buttonType;

    [SerializeField]
    private MessageType messageType;

    private readonly string[] arrOK = new string[] { "OK" };
    private readonly string[] arrOK_CANCEL = new string[] { "CANCEL", "OK" };
    private readonly string[] arrYES_NO = new string[] { "YES", "NO" };


    public enum MessageButtonType
    {
        OK,
        OK_CANCEL,
        YES_NO
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error
    }


    public void Show(MessageType type, string msg, MessageButtonType btnType)
    {
        messageType = type;
        Show(msg, btnType);
    }

    public void Show(string msg, MessageButtonType btnType)
    {
        TxtBody.text = msg;
        buttonType = btnType;
        Show();
    }


    public override void Init()
    {
        base.Init();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].OnClick.AddListener(() => OnClick(i));
        }
        Hide();
    }

    private void OnClick(int idx)
    {
        Hide();
    }

    public override void Show()
    {
        base.Show();

        switch (messageType)
        {
            case MessageType.Info:
                TxtTitle.text = "Info";
                break;
            case MessageType.Warning:
                TxtTitle.text = "Warning"; 
                break;
            case MessageType.Error:
                TxtTitle.text = "Error";
                break;
            default:
                break;
        }

        switch (buttonType)
        {
            case MessageButtonType.OK:
                ShowButttons(arrOK);
                break;
            case MessageButtonType.OK_CANCEL:
                ShowButttons(arrOK_CANCEL);
                break;
            case MessageButtonType.YES_NO:
                ShowButttons(arrYES_NO);
                break;
            default:
                break;
        }
    }

    void ShowButttons(string[] btnText)
    {
        int count = btnText.Length;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].SetText(btnText[i]);
            } else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
