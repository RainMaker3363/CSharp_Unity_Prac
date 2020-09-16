using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool bResursive = false)
    {
        Transform trans = FindChild<Transform>(go, name, bResursive);
        if (trans != null)
            return trans.gameObject;

        return null;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool bResursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(bResursive == false)
        {
            for(int i = 0; i< go.transform.childCount; ++i)
            {
                Transform trans = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || trans.name == name)
                {
                    T comp = trans.GetComponent<T>();
                    if (comp != null)
                        return comp;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            go.AddComponent<T>();
            return go.GetComponent<T>();
        }
        else
            return component;
    }
}
