using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum eWorldObject
    {
        UnKnown,
        Player,
        Monster
    }

    public enum eState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }


    public enum eLayer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum eCameraMode
    {
        QuarterView,
    }
    
    public enum eMouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
}
