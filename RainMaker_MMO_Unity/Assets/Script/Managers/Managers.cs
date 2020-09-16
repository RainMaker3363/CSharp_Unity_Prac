using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance;   // 유일성이 보장된다.
    static Managers Instance
    {
        get
        {
            Init();
            return s_Instance;
        }
    }

    #region Contetns
    GameManager _game = new GameManager();
    public static GameManager Game
    {
        get
        {
            return Instance._game;
        }
    }
    #endregion

    #region Core
    #region InputManager
    private InputManager _input = new InputManager();
    public static InputManager input
    {
        get
        {
            return Instance._input;
        }
    }
    #endregion

    #region ResourceManager
    private ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource
    {
        get
        {
            return Instance._resource;
        }
    }
    #endregion

    #region UIManager
    private UIManager _ui = new UIManager();
    public static UIManager UI
    {
        get
        {
            return Instance._ui;
        }
    }
    #endregion

    #region SceneManagerEx
    private SceneManagerEx _Scene = new SceneManagerEx();
    public static SceneManagerEx Scene
    {
        get
        {
            return Instance._Scene;
        }
    }
    #endregion

    #region SoundManager
    private SoundManager _Sound = new SoundManager();
    public static SoundManager Sound
    {
        get
        {
            return Instance._Sound;
        }
    }
    #endregion

    #region PoolManager
    private PoolManager _Pool = new PoolManager();
    public static PoolManager Pool
    {
        get
        {
            return Instance._Pool;
        }
    }
    #endregion

    #region DataManager
    private DataManager _Data = new DataManager();
    public static DataManager Data
    {
        get
        {
            return Instance._Data;
        }
    }
    #endregion
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_Instance == null)
        {
            var obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(obj);
            s_Instance = obj.GetComponent<Managers>();

            s_Instance._Sound.Init();
            s_Instance._Pool.Init();
            s_Instance._Data.Init();
        }

        
    }

    static void Clear()
    {
        Sound.Clear();
        input.Clear();
        Scene.Clear();
        UI.Clear();

        Pool.Clear();
    }
}
