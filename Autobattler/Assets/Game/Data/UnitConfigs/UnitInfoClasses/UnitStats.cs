using System;

namespace Game.Data.UnitConfigs.UnitInfoClasses
{
    [Serializable]
    public class UnitStats
    {
        public int health;
        public int attack;
        public int defense;
        public int actionPoints;
        public int initiative;
    }
}