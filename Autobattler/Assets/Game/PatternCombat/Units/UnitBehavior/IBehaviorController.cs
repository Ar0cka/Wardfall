using System.Collections.Generic;
using Game.Core.BaseUnits;
using Game.Data.PatternsSO;
using Game.PatternCombat.Units.UnitInfoManager;

namespace Game.PatternCombat.Units.UnitBehavior
{
    public interface IBehaviorController
    {
        public void PatternEffected(PatternSampleConfig baseBehaviorData);
        public BaseUnitController ChooseTarget(List<BaseUnitController> enemyList, IUnitInfoProvider currentUnit);
    }
}