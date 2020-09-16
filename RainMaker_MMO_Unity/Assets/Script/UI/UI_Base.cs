using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Start()
    {
        Init();
    }

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; ++i)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(this.gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(this.gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to Bind : {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }

    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }

    protected GameObject GetObject(int idx)
    {
        return Get<GameObject>(idx);
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> _call, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler handle = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                {
                    handle.OnPointerClickHandler -= _call;
                    handle.OnPointerClickHandler += _call;
                }
                break;

            case Define.UIEvent.Drag:
                {
                    handle.OnDragHandler -= _call;
                    handle.OnDragHandler += _call;
                }
                break;
        }
    }
}
