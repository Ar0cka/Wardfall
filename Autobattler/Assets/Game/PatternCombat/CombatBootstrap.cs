using System;
using System.Collections.Generic;
using Game.Core.SceneManagerWorld;
using Game.Core.SceneManagerWorld.SendData;
using Game.Data.UnitConfigs;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.CombatEndSystem;
using Game.PatternCombat.Player;
using Game.PatternCombat.TrunControllers;
using Grid;
using UnityEngine;
using Zenject;

namespace Game.PatternCombat
{
    public class CombatBootstrap : MonoBehaviour
    {
        [Header("Test")] 
        [SerializeField] private List<UnitWorldInfo> playerUnits;
        [SerializeField] private List<UnitWorldInfo> enemyUnits;
        
        [SerializeField] private InitializeBattleUnits unitsFactory;
        [SerializeField] private EndCombatUI endCombatUi;

        [SerializeField] private PlayerTurnController playerTurnController;
        [SerializeField] private TurnTypeManager turnTypeManager;
        [SerializeField] private TurnManager turnManager;

        [SerializeField] private GridSystem gridSystem;
        
        [Inject] private UnitsRegister _unitsRegister;
        
        private EndChecker _endChecker = new();
        
        private void Awake()
        {
            // if (SwitchScene.Instance == null)
            //     throw new NullReferenceException();
            //
            // var units = SwitchScene.Instance.GetData();

            gridSystem.Initialize();
            
            var units = Test();
            
            unitsFactory.CreateArmy(UnitParent.Player, units.playerUnits);
            unitsFactory.CreateArmy(UnitParent.Enemy, units.enemyUnits);
            
            _endChecker.SubscribeToRegister(_unitsRegister);
            endCombatUi.Initialize(ref _endChecker.OnEndCombat);
            
            playerTurnController.Initialize();
            turnManager.InitializeTurnManager(ref turnTypeManager.OnChangeTurnType, ref playerTurnController.EndPlayerTurn, ref playerTurnController.StartPlayerTurn);
            
            //TODO Initialize Spell System
            //TODO Initialize Pattern System
            //TODO Create First turn queue
            
            //TODO Initialize Hud UI
            //TODO Initialize End Battle with saving send data for calculate lost units after fight
        }
        
        private SendToBattleData Test()
        {
            return new SendToBattleData(playerUnits, enemyUnits, null);
        }
    }
}