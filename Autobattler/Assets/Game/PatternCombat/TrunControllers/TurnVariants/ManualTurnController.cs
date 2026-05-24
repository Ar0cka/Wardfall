using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Core.BaseTurnController;
using Game.Core.BaseUnits;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.Units.UnitBehavior;
using UnityEngine;

namespace Game.PatternCombat.TrunControllers.TurnVariants
{
    public class ManualTurnController : BaseTurnController
    {
        private Action _onUnitTurn;
        
        public override void InitializeTurnController(IUnitRegister unitRegister, IPathService pathService,
            ref Action<PlayerTurnType, IBehaviorController> playerTurnType)
        {
            IsPlayerTurn = true;

            UnitsRegister = unitRegister;
            PathService = pathService;
            StartPlayerTurn = playerTurnType;
        }
        public override async UniTask Turn()
        {
            if (IsTurn || IsPlayerTurn)
                return;

            IsTurn = true;
            
            if (UnitsQueue.Count <= 0)
                CreateTurn();
            
            while (UnitsQueue.Count > 0)
            {
                var unitController = UnitsQueue.Dequeue();

                if (unitController.GetParentType() == UnitParent.Player)
                    await AwaitPlayerTurn(unitController);

                var enemyType = unitController.GetParentType() == UnitParent.Player
                    ? UnitParent.Enemy
                    : UnitParent.Player;
                
                await unitController.Action(PathService, UnitsRegister.GetUnits(enemyType).Values.ToList());
            }
            
            StartPlayerTurn.Invoke(PlayerTurnType.StartTurn, null);
        }
    }
}