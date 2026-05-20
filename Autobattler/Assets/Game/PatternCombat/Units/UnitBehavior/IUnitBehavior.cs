using System.Collections.Generic;
using Game.Core.BaseUnits;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public interface IUnitBehavior
    {
        public void Initialize();
        public BaseUnitController ChoosePriorityType(List<BaseUnitController> units);
    }
}