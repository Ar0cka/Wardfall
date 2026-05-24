using System;
using Game.Data.UnitConfigs.UnitInfoClasses;
using UnityEngine.Rendering;

namespace Game.Data.Behaviors
{
    [Serializable]
    public class UnitHitPointsBehaviorData
    {
        public SerializedDictionary<UnitHitPointsState, float> priorityHpTypes;
        public float baseWeight;
    }
}