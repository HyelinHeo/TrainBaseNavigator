using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager 
{
    public const string WROK_FILE_NEW = "New";
    public const string WORK_FILE_NAME = "WorkFileName";

    public static void SetString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }

    public static string GetString(string key, string defaultStr)
    {
        return PlayerPrefs.GetString(key, defaultStr);
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }
}
