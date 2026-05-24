using System;
using Game.Data.Behaviors;
using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;
using Game.PatternCombat.Units.UnitHealthSystem;
using UnityEngine;

namespace Game.PatternCombat.Units.UnitInfoManager
{
    public class UnitInfoProvider : IUnitInfoProvider
    {
        protected UnitConfig UnitConfig;
        protected GameObject UnitObject;
        protected IUnitHealth UnitHealth;
        
        public virtual void Initialize(UnitInfoInputData inputData)
        {
            if (inputData is null)
                throw new NullReferenceException(nameof(inputData));
            
            UnitConfig = inputData.UnitConfig;
            UnitObject = inputData.UnitObject;
            UnitHealth = inputData.UnitHealth;
        }
        
        public GameObject GetUnitObject() => UnitObject;
        public UnitType GetUnitType() => UnitConfig.UnitDefinition.unitType;
        public UnitHitPointsState GetUnitHitPointsState() => UnitHealth.HitPointsState;
    }

    public class UnitInfoInputData
    {
        public UnitInfoInputData(UnitConfig unitConfig, GameObject unitObject, 
            IUnitHealth unitHealth)
        {
            UnitConfig = unitConfig;
            UnitObject = unitObject;
            UnitHealth = unitHealth;
        }
        
        public UnitConfig UnitConfig;
        public GameObject UnitObject;
        public IUnitHealth UnitHealth;
        public UnitHitPointsBehaviorData UnitHealthData;
    }
}