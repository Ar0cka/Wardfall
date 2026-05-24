using System;
using System.Collections.Generic;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units.UnitBehavior;
using UnityEngine;
using UnityEngine.UI;

namespace Game.PatternCombat.Player
{
    public class PlayerTurnController : MonoBehaviour
    {
        [SerializeField] private List<Button> inactiveInTurn;
        
        [SerializeField] private Button chooseTurn;
        [SerializeField] private Button endTurnButton;
        
        public Action<PlayerTurnType, IBehaviorController> StartPlayerTurn;
        public Action<PlayerTurnType> EndPlayerTurn;
        
        private PlayerTurnType _currentTurnType;

        public void Initialize()
        {
            StartPlayerTurn += StartTurn;
            endTurnButton.onClick.AddListener(EndTurn);
        }

        private void StartTurn(PlayerTurnType turnType, IBehaviorController behaviorController)
        {
            _currentTurnType = turnType;
            StartTurnActiveItems(true);
            
            //TODO Send behavior controller to pattern choose system
        }
        
        private void EndTurn()
        {
            AllItemsActive(false);
            EndPlayerTurn?.Invoke(_currentTurnType);
        }
        
        private void AllItemsActive(bool isActive)
        {
            endTurnButton.interactable = isActive;
            chooseTurn.interactable = isActive;

            foreach (var button in inactiveInTurn)
            {
                button.interactable = isActive;
            }
        }

        private void StartTurnActiveItems(bool isActive)
        {
            endTurnButton.interactable = isActive;
            
            if (_currentTurnType is PlayerTurnType.StartTurn) 
                chooseTurn.interactable = isActive;

            foreach (var button in inactiveInTurn)
            {
                button.interactable = isActive;
            }
        }
    }
}