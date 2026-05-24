using Game.PatternCombat.Units.UnitBehavior;
using UnityEngine;

namespace Game.Data.UnitConfigs
{
    
    public abstract class BaseUnitBehaviorConfig<TUnitBehavior> : ScriptableObject where TUnitBehavior : IUnitBehavior
    {
        [Header("Unit multiplire")]
        [field:SerializeField] public float UnitMultiplire { get; private set; }
        
        public abstract TUnitBehavior CreateUnitBehavior();
    }
}