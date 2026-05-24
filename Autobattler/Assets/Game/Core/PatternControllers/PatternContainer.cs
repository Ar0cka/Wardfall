using System;
using System.Collections.Generic;
using System.Linq;
using Game.Data.PatternsSO;
using Game.Data.Player;
using Game.Patterns.BasePatternLogic;
using UnityEngine;

namespace Game.Core.PatternControllers
{
    public class PatternContainer<TConfig> where TConfig : DefaultPatternConfig<IPattern>
    {
        protected Dictionary<string, TConfig> PatternsContainer { get; private set; } = new();
        protected PatternLimitsInfo Limits;

        protected Action<string> FallbackContainerError;
        
        protected const string OverflawMessage = "You have reached the maximum number of patterns!";
        protected const string PatternIsAlreadyExisted = "Pattern already exists!";
        protected const string PatternNotFound = "Pattern not found!";
        protected const string FailedChangePatternName = "Failed to change pattern name!";
        
        public virtual void Initialize(PatternLimitsInfo patternLimits, ref Action<string> isOverflawAction)
        {
            FallbackContainerError = isOverflawAction;
        }
        
        public virtual void AddPattern(string patternId, TConfig config)
        {
            if (PatternsContainer.Count >= Limits.maxGeneralPatterns)
            {
                Debug.LogWarning(OverflawMessage);
                FallbackContainerError?.Invoke(OverflawMessage);
                return;
            }
            
            var isAdd = PatternsContainer.TryAdd(patternId, config);
            
            FallbackContainerError?.Invoke(isAdd ? "" : PatternIsAlreadyExisted);
        }

        public virtual void RemovePattern(string patternId)
        {
            var isRemove = PatternsContainer.Remove(patternId);
            FallbackContainerError?.Invoke(isRemove ? "" : PatternNotFound);
        }

        public virtual void ChangePattern(string oldPatternId, string newPatternId, TConfig newConfig)
        {
            var isEmpty = PatternsContainer.ContainsKey(oldPatternId);
            var isNotAlreadyExist = !PatternsContainer.ContainsKey(newPatternId);

            if (isEmpty)
            {
                FallbackContainerError?.Invoke($"{PatternNotFound} with id {oldPatternId}");
                return;
            }
            if (!isNotAlreadyExist)
            {
                FallbackContainerError?.Invoke($"{PatternIsAlreadyExisted} with id {newPatternId}");
                return;
            }
            
            var remove = PatternsContainer.Remove(oldPatternId);
            bool add = false;

            if (remove)
            {
                add = PatternsContainer.TryAdd(newPatternId, newConfig);
            }
              
            
            FallbackContainerError?.Invoke(remove && add ? "" : PatternNotFound);
        }
    }
}