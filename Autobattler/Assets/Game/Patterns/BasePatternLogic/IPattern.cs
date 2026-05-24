using System;
using Game.Core.BaseUnits;
using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;

namespace Game.Patterns.BasePatternLogic
{
    public interface IPattern
    {
        public float GetUnitWeight(PatternInputData inputData);
    }

    [Serializable]
    public class PatternInputData
    {
        public float distance;
        public UnitType unitType;
        public UnitHitPointsState hitState;
        public BaseUnitController unitController;
    }
}