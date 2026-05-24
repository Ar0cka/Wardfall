using System;
using Game.Data.UnitConfigs;
using UnityEngine.Rendering;

namespace Game.Data.Behaviors
{
    [Serializable]
    public class UnitTypeBehaviorData
    {
        public SerializedDictionary<UnitType, float> priorityTypes;
        public float baseWeight;
    }
}