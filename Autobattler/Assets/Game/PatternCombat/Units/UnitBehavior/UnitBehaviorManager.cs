using System.Collections.Generic;
using Game.Core.BaseUnits;
using Game.Data.PatternsSO;
using Game.Data.UnitConfigs;
using Game.PatternCombat.Units.UnitInfoManager;
using UnityEngine;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public class BehaviorControllerController : MonoBehaviour, IBehaviorController
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private UnitBehaviorConfig unitBehaviorConfig;
        
        private IUnitBehavior _unitBehavior;
        private UnitConfig _unitConfig;

        private PatternSampleConfig _effectedBaseBehavior;
        
        private GUID _uniqueId;
        
        public void Initialize(UnitConfig unitConfig, GUID uniqueId)
        {
            _unitConfig = unitConfig;
            _uniqueId = uniqueId;
        }
        
        public void PatternEffected(PatternSampleConfig baseBehaviorData)
        {
            _effectedBaseBehavior = baseBehaviorData;
        }

        public BaseUnitController ChooseTarget(List<BaseUnitController> enemyList, IUnitInfoProvider currentUnit)
        {
            var dictionary = new Dictionary<float, BaseUnitController>();

            foreach (var enemy in enemyList)
            {
                var inputInfo = new InputBehaviorInfo(currentUnit, enemy.GetUnitInfoProvider());
                var unitWeight = _unitBehavior.GetUnitWeight(inputInfo);

                dictionary.TryAdd(unitWeight, enemy);
            }
            
            return null;
        }
    }
}