using System.Collections.Generic;
using Game.Data.Behaviors;
using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;
using UnityEngine;

namespace Game.Services
{
    public class WeightCalculator
    {
        public float Distance(float maxDistance, float currentDistance)
        {
            return Mathf.Clamp01(1 - currentDistance / maxDistance);
        }

        public float UnitTypeWeight(UnitTypeBehaviorData unitsTypeConfig, UnitType targetUnitType)
        {
            if (unitsTypeConfig.priorityTypes.TryGetValue(targetUnitType, out var value))
                return value;

            return unitsTypeConfig.baseWeight;
        }

        public float UnitHpWeight(UnitHitPointsBehaviorData hitPointsData, UnitHitPointsState currentUnitState)
        {
            if (hitPointsData.priorityHpTypes.TryGetValue(currentUnitState, out var value))
                return value;
            
            return hitPointsData.baseWeight;
        }
    }
}