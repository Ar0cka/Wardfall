using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.BaseTurnController;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.TrunControllers.TurnVariants;
using UnityEngine;
using Zenject;

namespace Game.PatternCombat.TrunControllers
{
    public class TurnManager : MonoBehaviour
    {
        [Inject] private TurnFactory _turnFactory;
        [Inject] private IUnitRegister _unitRegister;
        [Inject] private IPathService _pathService;
        
        private Dictionary<TurnControllerType, ITurnController> _turnControllers = new();
        
        private TurnControllerType _currentControllerType;
        private ITurnController _currentController;

        public void InitializeTurnManager(ref Action<TurnControllerType> onChangeType, ref Action<PlayerTurnType> endTurn, 
            ref Action<PlayerTurnType> startPlayerTurn)
        {
            onChangeType += ChangedControllerType;
            _currentControllerType = TurnControllerType.Manual;

            _turnControllers[TurnControllerType.Manual] = 
                _turnFactory.CreateTurnController<ManualTurnController>(_unitRegister, _pathService, 
                    ref startPlayerTurn);
            
            _currentController = _turnControllers[_currentControllerType];

            endTurn += (t) =>
            {
                _currentController.PlayerTurnIsEnd();
                
                Debug.Log("End player turn");

                if (t == PlayerTurnType.UnitTurn)
                    return;
                
                _currentController.Turn().Forget(e =>
                {
                    Debug.Log(e.Message);
                });
                
                Debug.Log("Start units turn");
            };
        }
        
        private void ChangedControllerType(TurnControllerType type)
        {
            _currentControllerType = type;

            if (_turnControllers.TryGetValue(type, out var value))
            {
                _currentController = value;
            }
        }
    }

    public enum TurnControllerType
    {
        Manual,
        ArmyTemplates,
        Automatic
    }

    public enum PlayerTurnType
    {
        StartTurn,
        UnitTurn
    }
}