using System.Collections.Generic;
using Game.Core.BaseUnits;
using Game.Data.UnitConfigs;
using Game.PatternCombat.Units.UnitInfoManager;
using Game.Services;
using UnityEngine;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public class UnitBehavior : IUnitBehavior
    {
        private WeightCalculator _weightCalculator;
        private UnitBehaviorConfig _behaviorConfig;
        
        public void Initialize(WeightCalculator weightCalculator, UnitBehaviorConfig behaviorConfig)
        {
            _weightCalculator = weightCalculator;
            _behaviorConfig = behaviorConfig;
        }
        public virtual float GetUnitWeight(InputBehaviorInfo behaviorInfo)
        {
            var healthScore = _weightCalculator.UnitHpWeight(_behaviorConfig.hpBehaviorData,
                behaviorInfo.EnemyUnit.GetUnitHitPointsState());
            
            var distance = Vector2.Distance(behaviorInfo.CurrentUnit.GetUnitObject().transform.position,
                behaviorInfo.EnemyUnit.GetUnitObject().transform.position);

            var distanceScore = _weightCalculator.Distance(_behaviorConfig.distanceData.maxDistance, distance);
            var unitTypeScore = _weightCalculator.UnitTypeWeight(_behaviorConfig.UnitTypeBehaviorData,
                behaviorInfo.EnemyUnit.GetUnitType());
            
            return healthScore + distanceScore + unitTypeScore;
        }
    }
}