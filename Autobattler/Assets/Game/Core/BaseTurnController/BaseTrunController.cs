using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.BaseUnits;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units;
using Game.PatternCombat.Units.UnitBehavior;
using Zenject;

namespace Game.Core.BaseTurnController
{
    public abstract class BaseTurnController : ITurnController
    {
        protected Queue<BaseUnitController> UnitsQueue = new();
        
        protected int TurnCount = 0;
        protected bool IsTurn = false;
        protected bool IsPlayerTurn = false;

        protected IUnitRegister UnitsRegister;
        protected IPathService PathService;

        protected Action<PlayerTurnType, IBehaviorController> StartPlayerTurn;
        
        public abstract void InitializeTurnController(IUnitRegister unitRegister, 
            IPathService pathService, ref Action<PlayerTurnType> playerTurnType);

        public abstract UniTask Turn();
        public async UniTask AwaitPlayerTurn(BaseUnitController currentUnit)
        {
            IsPlayerTurn = true;
            
            StartPlayerTurn?.Invoke(PlayerTurnType.UnitTurn, );
            
            await UniTask.WaitUntil(() => !IsPlayerTurn);
        }
        public virtual void CreateTurn()
        {
            UnitsQueue.Clear();
            
            var units = GetAllUnits();

            units.Sort((a, b) => b.GetUnitInfo().UnitInfo.unitConfig.Stats.initiative
                .CompareTo(a.GetUnitInfo().UnitInfo.unitConfig.Stats.initiative));
            
            foreach (var unit in units)
            {
                UnitsQueue.Enqueue(unit);
            }
            
            TurnCount++;

            IsPlayerTurn = true;
            IsTurn = false;
        }
        
        public bool IsTurnActive() => IsTurn;
        public int GetCurrentTurn() => TurnCount;
        public void PlayerTurnIsEnd()
        {
            IsPlayerTurn = false;
        }

        protected List<BaseUnitController> GetAllUnits()
        {
            List<BaseUnitController> units = new();
            units.AddRange(UnitsRegister.GetUnits(UnitParent.Player).Values);
            units.AddRange(UnitsRegister.GetUnits(UnitParent.Enemy).Values);

            return units;
        }
    }

    public interface ITurnController
    {
        public void InitializeTurnController(IUnitRegister unitRegister, IPathService pathService, 
            ref Action<PlayerTurnType> playerTurnType);
        public UniTask AwaitPlayerTurn();
        public UniTask Turn();
        
        public void PlayerTurnIsEnd();
        public bool IsTurnActive();
        public int GetCurrentTurn();
    }
}