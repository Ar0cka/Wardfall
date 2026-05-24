using System;
using UnityEngine;

namespace Game.Data.UnitConfigs.UnitInfoClasses
{
    [Serializable]
    public class UnitChecker
    {
        public float aroundUnit;
        public float cellRadius;

        public LayerMask targetLayer;
    }
}