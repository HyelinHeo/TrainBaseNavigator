using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Events;

public class XMLSaveFileManager : XMLManager<XMLSaveFileManager>
{
    public string FileName;

    private XMLSaveFile xml;
    public XMLSaveFile Xml { get { return xml; } }

    public MPXObjectManager Objects;

    private string fileExt = ".xml";

    public UnityEvent OnLoadFailed = new UnityEvent();



    public override bool Save(string fullPath)
    {
        if (!Directory.Exists(BasePath))
        {
            Directory.CreateDirectory(BasePath);
        }

        if (ZinSerializerForXML.Serialization<XMLSaveFile>(xml, fullPath))
        {
            Debug.LogFormat("Save OK: {0}", fullPath);
            return true;
        }
        else
        {
            Debug.LogErrorFormat("Save failed: {0}", fullPath);
            return false;
        }

    }

    void LoadFailed()
    {
        NewFileName();
        OnLoadFailed.Invoke();
    }

    public IEnumerator LoadFile()
    {
        if (!string.IsNullOrEmpty(FileName))
        {
            if (Directory.Exists(BasePath))
            {
                string file = BasePath + FileName + fileExt;
                if (File.Exists(file))
                {
                    yield return StartCoroutine(Load(file));
                }
                else
                {
                    Debug.LogError("file not found. " + file);
                    LoadFailed();
                }
            }
            else
            {
                Debug.LogError("folder not found. " + BasePath);
                LoadFailed();
            }
        }
        else
        {
            LoadFailed();
        }
    }

    public override IEnumerator Load(string fullPath)
    {
        //string oriPath = fullPath;
        //string path = "file:///" + oriPath;
        //WWW www = new WWW(path);

        //yield return www;

        yield return null;

        string text = File.ReadAllText(fullPath);

        if (!string.IsNullOrEmpty(text))
        {
            xml = ZinSerializerForXML.Deserialization<XMLSaveFile>(text);
            if (xml != null)
                Debug.LogFormat("load file OK: {0}", fullPath);
            else
                Debug.LogErrorFormat("Load Object Setting File Failed. XML file error: {0}", fullPath);
        }
        else
        {
            xml = null;
            Debug.LogErrorFormat("Load Error: {0}", fullPath);
            LoadFailed();
        }
        //www.Dispose();
        //www = null;
    }

    public void NewFileName()
    {
        FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
    }

    public bool Save()
    {
        MPXObject[] objs = Objects.GetObjectAll();

        if (xml == null)
            xml = new XMLSaveFile();

        xml.Clear();
        for (int i = 0; i < objs.Length; i++)
        {
            xml.Add(objs[i]);
        }

        string fileName = FileName + fileExt;
        if (Save(BasePath + fileName))
        {
            PrefsManager.SetString(PrefsManager.WORK_FILE_NAME, FileName);
            PrefsManager.Save();

            return true;
        }
        return false;
    }
}
