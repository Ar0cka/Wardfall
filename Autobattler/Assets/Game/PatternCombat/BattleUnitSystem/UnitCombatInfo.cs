using System;
using Game.Data.UnitConfigs;
using Game.PatternCombat.Grid;
using Grid;

namespace Game.PatternCombat.BattleUnitSystem
{
    [Serializable]
    public class UnitCombatInfo
    {
        public UnitWorldInfo UnitWorldConfig { get; private set; }
        public UnitParent UnitParent { get; private set; }
        public int Count { get; private set; }
        public GridData UnitPosition { get; private set; }
        
        public UnitCombatInfo(UnitWorldInfo worldWorldConfig, UnitParent parent)
        {
            UnitWorldConfig = worldWorldConfig;
            Count = worldWorldConfig.unitCount;

            UnitParent = parent;
        }

        public void SetPosition(GridData gridData)
        {
            UnitPosition = gridData;
        }
    }
}