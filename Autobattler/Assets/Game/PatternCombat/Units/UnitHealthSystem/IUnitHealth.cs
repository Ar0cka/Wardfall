using Game.Data.UnitConfigs.UnitInfoClasses;

namespace Game.PatternCombat.Units.UnitHealthSystem
{
    public interface IUnitHealth
    {
        public int CurrentHealth { get; }
        public UnitHitPointsState HitPointsState { get; }
    }
}