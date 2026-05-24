using System;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units.UnitBehavior;

namespace Game.Core.BaseTurnController
{
    public class TurnFactory
    {
        public ITurnController CreateTurnController<TType>(IUnitRegister unitRegister, 
            IPathService pathService, ref Action<PlayerTurnType, IBehaviorController> playerAction) where TType : ITurnController, new()
        {
            var turnController = new TType();
            
            turnController.InitializeTurnController(unitRegister, pathService, ref playerAction);

            return turnController;
        }
    }
}