using Game.Data.Behaviors;
using Game.PatternCombat.Units.UnitBehavior;
using UnityEngine;

namespace Game.Data.UnitConfigs
{
    [CreateAssetMenu(fileName = "UnitBehavior", menuName = "Behavior", order = 0)]
    public class UnitBehaviorConfig : BaseUnitBehaviorConfig<IUnitBehavior>
    {
        [Header("Distance behavior data")]
        [field:SerializeField] public UnitDistanceBehaviorData distanceData { get; private set; }
        
        [Header("Priority Units data")]
        [field:SerializeField] public UnitTypeBehaviorData UnitTypeBehaviorData { get; private set; }

        [Header("Priority HitPoints State data")]
        [field: SerializeField] public UnitHitPointsBehaviorData hpBehaviorData { get; private set; }

        public override IUnitBehavior CreateUnitBehavior()
        {
            return new UnitBehavior();
        }
    }
}