using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int maxhp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> makeDict()
        {
            Dictionary<int, Stat> Dict = new Dictionary<int, Stat>();

            foreach (Stat _stat in stats)
            {
                Dict.Add(_stat.level, _stat);
            }

            return Dict;
        }
    }
}

#endregion