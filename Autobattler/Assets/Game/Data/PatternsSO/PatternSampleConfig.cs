using Game.Patterns.BasePatternLogic;
using UnityEngine;

namespace Game.Data.PatternsSO
{
    [CreateAssetMenu]
    public class PatternSampleConfig : DefaultPatternConfig<IPattern>
    {
        public override IPattern CreatePattern()
        {
            return new BaseSamplePattern();
        }
    }
}