using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.Grid;
using Game.PatternCombat.Grid.Services;
using Game.PatternCombat.TrunControllers;
using Game.PatternCombat.Units;
using Game.PatternCombat.Units.UnitBehavior;
using Game.PatternCombat.Units.UnitHealthSystem;
using Game.PatternCombat.Units.UnitInfoManager;
using Grid;
using UnityEngine;
using Zenject;

namespace Game.Core.BaseUnits
{
    public abstract class BaseUnitController : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb2D;

        [Header("Unit components")] 
        [SerializeField] protected BaseUnitMovement move;
        [SerializeField] protected BehaviorControllerController behaviorControllerController;
        
        protected GridQuery GridQuery;
        
        protected UnitCombatInfo UnitCombatConfig;
        protected UnitInfoProvider UnitInfoProvider;
        protected IUnitHealth Health;
        protected UnitParent Parent;

        protected int ActionPoints = 0;
        protected GUID UniqueID;

        protected const float StopDistance = 0.2f;

        public virtual void InitializeUnit(UnitCombatInfo info, UnitParent parent, GridData gridData, GridQuery gridQuery)
        {
            UnitCombatConfig = info;
            Parent = parent;
            GridQuery = gridQuery;
            
            IsValidComponents();
            GenerateUnitID();
            
            UnitCombatConfig.SetPosition(gridData);
            UnitInfoProvider.Initialize(new UnitInfoProviderInitData(UnitCombatConfig.UnitWorldConfig.unitConfig, gameObject, Health));
        }
        
        public abstract UniTask Action(IPathService pathService, List<BaseUnitController> enemyUnits);

        protected virtual async UniTask UnitMove(GridData targetData)
        {
            await move.MoveAsync(targetData, GetUnitInfo().UnitWorldConfig.unitConfig);

            ActionPoints--;

            if (Vector2.Distance(transform.position, targetData.worldPosition) > StopDistance)
                throw new ArgumentException("Current position != targetData");
            
            UnitCombatConfig.SetPosition(targetData);
        }
        
        protected void IsValidComponents()
        {
            if (rb2D is null)
                throw new NullReferenceException(nameof(rb2D));
        }
        
        protected void GenerateUnitID()
        {
            UniqueID = GUID.Generate();
        }

        public UnitCombatInfo GetUnitInfo() => UnitCombatConfig;
        public GUID GetUniqueID() => UniqueID;
        public IBehaviorController GetUnitBehavior() => behaviorControllerController;
        public IUnitInfoProvider GetUnitInfoProvider() => UnitInfoProvider;
        public UnitParent GetEnemyType()
        {
            return Parent == UnitParent.Player ? UnitParent.Enemy : UnitParent.Player;
        }
        public UnitParent GetParentType() => Parent;
    }
}