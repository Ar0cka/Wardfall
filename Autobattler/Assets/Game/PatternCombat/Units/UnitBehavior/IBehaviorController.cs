using Game.Data.Patterns;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public interface IBehaviorController
    {
        public void PatternEffected(SamplePatternData patternData);
    }
}