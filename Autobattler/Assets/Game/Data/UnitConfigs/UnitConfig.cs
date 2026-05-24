using System;
using Game.Data.UnitConfigs.UnitInfoClasses;
using UnityEngine;

namespace Game.Data.UnitConfigs
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public string UnitID { get; private set; }
        [field: SerializeField] public UnitDefinition UnitDefinition { get; private set; }
        [field: SerializeField] public UnitMovement Movement { get; private set; }
        [field: SerializeField] public UnitStats Stats { get; private set; }
        [field: SerializeField] public UnitAnimation Animation { get; private set; }
        [field: SerializeField] public UnitVisualData VisualData { get; private set; }
        
        [field: SerializeField] public UnitChecker UnitChecker { get; private set; }
        
    }
    
    [Serializable]
    public class UnitDefinition
    {
        public string unitName;
        public string unitDescription;
        public UnitType unitType;
    }

    [Serializable]
    public class UnitVisualData
    {
        public Sprite unitSprite;
        public GameObject unitModel;
    }
}