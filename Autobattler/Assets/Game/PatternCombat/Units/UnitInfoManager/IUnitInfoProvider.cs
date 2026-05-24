using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;
using UnityEngine;

namespace Game.PatternCombat.Units.UnitInfoManager
{
    public interface IUnitInfoProvider
    {
        public GameObject GetUnitObject();
        public UnitType GetUnitType();
        public UnitHitPointsState GetUnitHitPointsState(); 
    }
}