using UnityEngine;
using System.Collections;

public abstract class XMLManager<T> : Singleton<T> where T : XMLManager<T>
{
    public string BasePath;

    private void Awake()
    {
        BasePath = Application.streamingAssetsPath + "/";
    }

    public virtual bool Save(string fullPath) { return false; }

    public virtual IEnumerator Load(string fullPath) { yield break; }

}
