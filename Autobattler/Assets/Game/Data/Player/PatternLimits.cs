using System;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "pattern settings", menuName = "Patterns/PlayerPatternSettings")]
    public class PatternLimits : ScriptableObject
    {
        [field:SerializeField] public PatternLimitsInfo PatternLimitsInfo { get; private set; }

        public PatternLimitsInfo Clone()
        {
            return new PatternLimitsInfo
            {
                maxGeneralPatterns = PatternLimitsInfo.maxGeneralPatterns,
                maxSamplePatterns = PatternLimitsInfo.maxSamplePatterns
            };
        }
    }

    [Serializable]
    public class PatternLimitsInfo
    {
        public int maxSamplePatterns;
        public int maxGeneralPatterns;
    }
}