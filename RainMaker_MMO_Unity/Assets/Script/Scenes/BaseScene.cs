using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    Define.Scene _sceneType = Define.Scene.Unknown;
    public Define.Scene SceneType
    {
        get
        {
            return _sceneType;
        }
        protected set
        {
            _sceneType = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
