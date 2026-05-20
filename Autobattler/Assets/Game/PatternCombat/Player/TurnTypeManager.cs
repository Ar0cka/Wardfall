using System;
using Game.PatternCombat.TrunControllers;
using UnityEngine;

namespace Game.PatternCombat.Player
{
    public class TurnTypeManager : MonoBehaviour
    {
        public Action<TurnControllerType> OnChangeTurnType;
    }
}