using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    HashSet<GameObject> _monster = new HashSet<GameObject>();
    
    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer
    {
        get
        {
            return _player;
        }
    }

    public GameObject Spawn(Define.eWorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.instantiate(path, parent);

        switch(type)
        {
            case Define.eWorldObject.Monster:
                {
                    _monster.Add(go);

                    if (OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(1);
                }
                break;

            case Define.eWorldObject.Player:
                {
                    _player = go;
                }
                break;
        }

        return go;
    }

    public Define.eWorldObject GetWorldObjectType(GameObject go)
    {
        BaseController controller = go.GetComponent<BaseController>();
        if (controller == null)
            return Define.eWorldObject.UnKnown;

        return controller.WorldObjectType;

    }

    public void DeSpawn(GameObject go)
    {
        Define.eWorldObject type = GetWorldObjectType(go);

        switch (type) 
        {
            case Define.eWorldObject.Monster:
                {
                    if (_monster.Contains(go))
                    {
                        _monster.Remove(go);

                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;

            case Define.eWorldObject.Player:
                {
                    if(_player == go)
                    {
                        _player = null;
                    }
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
