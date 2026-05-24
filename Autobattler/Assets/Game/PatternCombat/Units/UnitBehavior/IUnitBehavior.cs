using System;
using System.Collections.Generic;
using Game.Core.BaseUnits;
using Game.Data.Behaviors;
using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;
using Game.PatternCombat.Units.UnitInfoManager;
using Game.Services;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public interface IUnitBehavior
    {
        public void Initialize(WeightCalculator weightCalculator, UnitBehaviorConfig behaviorConfig);
        public float GetUnitWeight(InputBehaviorInfo inputBehaviorInfo); 
    }
    
    public class InputBehaviorInfo
    {
        public InputBehaviorInfo(IUnitInfoProvider currentUnit, IUnitInfoProvider enemyUnit)
        {
            CurrentUnit = currentUnit;
            EnemyUnit = enemyUnit;
        }
        
        public IUnitInfoProvider CurrentUnit;
        public IUnitInfoProvider EnemyUnit;
    }
}