using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp {
        get { return _exp; } set
        { 
            _exp = value;

            // 레벨 업 체크!
            int level = Level;
            while(true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;

                if (_exp < stat.totalExp)
                    break;

                ++level;
            }

            if(level != Level)
            {
                Debug.Log("Level UP!");
                Level = level;
                SetStat(Level);
            }
        } 
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _defense = 5;
        _speed = 0.1f;

        _gold = 0;
        _exp = 0;

        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Data.Stat stat = dict[level];

        _hp = stat.maxhp;
        _maxHp = stat.maxhp;
        _attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
