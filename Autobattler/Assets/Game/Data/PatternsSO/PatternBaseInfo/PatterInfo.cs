using System;
using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.PatternsSO.PatternBaseInfo
{
    [Serializable]
    public class PatterInfo
    {
        [SerializeField] public string patternId;
        [SerializeField] public string patternName;
        [SerializeField] public string patternDescription;

        [SerializeField] public List<UnitType> effectingUnits;
    }
}