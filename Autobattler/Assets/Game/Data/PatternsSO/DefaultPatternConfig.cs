using UnityEngine;

namespace Game.Data.PatternsSO
{
    public abstract class DefaultPatternConfig<TPatternType> : ScriptableObject
    {
        public TPatternType patternType;

        public abstract TPatternType CreatePattern();
    }
}