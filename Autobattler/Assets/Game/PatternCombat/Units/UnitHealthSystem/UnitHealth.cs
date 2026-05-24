using System;
using Game.Data.Behaviors;
using Game.Data.UnitConfigs;
using Game.Data.UnitConfigs.UnitInfoClasses;
using Game.Tools;
using UnityEngine;

namespace Game.PatternCombat.Units.UnitHealthSystem
{
    public class UnitHealth : IUnitHealth, IHealthSubscribes
    {
        private SerializableDictionary<float, UnitHitPointsState> _unitHpStateCollection;

        private Action _onHealthChanged;
        private Action _onUnitDead;
        
        public int CurrentHealth { get; private set; } = 100;
        public UnitHitPointsState HitPointsState { get; private set; }

        private UnitHitPointsInfo _unitHpInfo;

        public void Initialize(UnitHitPointsInfo unitHp)
        {
            _unitHpInfo = unitHp;
            CurrentHealth = _unitHpInfo.maxHealth;;
        }

        public void AddHitPoints(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, CurrentHealth, _unitHpInfo.maxHealth);
            _onHealthChanged?.Invoke();
            
            ChangeUnitHealthState();
        }
        public void TakeDamage(int amount) //TODO Add attack info and service for calculating finally damage
        {
            CurrentHealth -= amount;
            
            _onHealthChanged?.Invoke();
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                _onUnitDead?.Invoke();
            }
            
            ChangeUnitHealthState();
        }
        
        private void ChangeUnitHealthState()
        {
            var proc = CurrentHealth / (float)_unitHpInfo.maxHealth;;

            HitPointsState = proc < _unitHpInfo.lowProc ? UnitHitPointsState.Low : 
                proc < _unitHpInfo.mediumProc && proc > _unitHpInfo.lowProc ? UnitHitPointsState.Medium : UnitHitPointsState.Max;
        }

        #region SubscribersRegion

        public IHealthSubscribes GetSubscriber() => this;
        
        public void SubscribeOnHealthChanged(Action onHealthChanged)
        {
            _onHealthChanged += onHealthChanged;
        }
        public void UnsubscribeOnHealthChanged(Action onHealthChanged)
        {
            _onHealthChanged -= onHealthChanged;
        }

        public void SubscribeOnUnitDead(Action onUnitDead)
        {
            _onUnitDead += onUnitDead;
        }
        public void UnsubscribeOnUnitDead(Action onUnitDead)
        {
            _onUnitDead -= onUnitDead;
        }

        #endregion
       
    }
}