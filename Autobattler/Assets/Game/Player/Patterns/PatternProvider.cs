using System;
using System.Collections.Generic;
using Game.Data.Player;
using NUnit.Framework;
using UnityEngine;

namespace Game.Player.Patterns
{
    public class PatternProvider : MonoBehaviour
    {
        private GeneralPatternContainer _generalContainer;
        private SamplePatternContainer _sampleContainer;
        
        //[SerializeField] private PlayerStartPatterns startPlayerPatterns;

        // private void Awake()
        // {
        //     var generalPatternData = new PatternData<GeneralPatternInfoData>(
        //         new List<GeneralPatternInfoData>(startPlayerPatterns.StartPlayerPatterns.generalsPatterns));
        //     var samplePatternData = new PatternData<SampleBaseBehaviorData>(
        //         new List<SampleBaseBehaviorData>(startPlayerPatterns.StartPlayerPatterns.samplePatterns));
        // }

        public void InitializePatternProvider(ref Action<string> operationFallback, PatternLimitsInfo limits)
        {
            _generalContainer = new GeneralPatternContainer();
            _sampleContainer = new SamplePatternContainer();

            _generalContainer.Initialize(limits, ref operationFallback);
            _sampleContainer.Initialize(limits, ref operationFallback);
        }
    }
}