using System;

namespace Game.PatternCombat.Units.UnitHealthSystem
{
    public interface IHealthSubscribes
    {
        void SubscribeOnHealthChanged(Action onHealthChanged);
        void UnsubscribeOnHealthChanged(Action onHealthChanged);
        void SubscribeOnUnitDead(Action onUnitDead);
        void UnsubscribeOnUnitDead(Action onUnitDead);
    }
}