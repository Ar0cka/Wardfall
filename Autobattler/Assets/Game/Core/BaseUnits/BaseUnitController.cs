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
        
        protected UnitCombatInfo UnitInfo;
        protected UnitParent Parent;

        protected int ActionPoints = 0;
        protected GUID UniqueID;

        protected const float StopDistance = 0.2f;

        public virtual void InitializeUnit(UnitCombatInfo info, UnitParent parent, GridData gridData, GridQuery gridQuery)
        {
            UnitInfo = info;
            Parent = parent;
            GridQuery = gridQuery;
            
            IsValidComponents();
            GenerateUnitID();
            
            UnitInfo.SetPosition(gridData);
        }

        public abstract BaseUnitController ChooseTarget(List<BaseUnitController> enemyUnits);
        public abstract UniTask Action(IPathService pathService, BaseUnitController targetUnit);

        protected virtual async UniTask UnitMove(GridData targetData)
        {
            await move.MoveAsync(targetData, GetUnitInfo().UnitInfo.unitConfig);

            ActionPoints--;

            if (Vector2.Distance(transform.position, targetData.worldPosition) > StopDistance)
                throw new ArgumentException("Current position != targetData");
            
            UnitInfo.SetPosition(targetData);
        }

        protected virtual BaseUnitController CheckUnitRadius()
        {
            var unitConfig = UnitInfo.UnitInfo.unitConfig;
            
            var aroundList =
                Physics2D.OverlapCircleAll(rb2D.position, unitConfig.UnitChecker.aroundUnit, unitConfig.UnitChecker.targetLayer).ToList();

            if (aroundList.Count <= 0)
                return null;

            List<BaseUnitController> unitsPosition = new();

            foreach (var unitCollider in aroundList)
            {
                var unitController = unitCollider.GetComponent<BaseUnitController>();
                
                if (unitController.GetUniqueID() == UniqueID)
                    continue;
                
                Sort(unitController, ref unitsPosition);
            }

            var unit = ChoosePriorityType(unitsPosition);

            return unit;
        }
        protected virtual void Sort(BaseUnitController unitController, ref List<BaseUnitController> unitsPosition)
        {
            if (unitsPosition.Count <= 0)
            {
                unitsPosition.Add(unitController);
                return;
            }

            int i = 0;
            
            var unitDistance = Vector2.Distance(unitController.transform.position, rb2D.position);
            
            while (i < unitsPosition.Count)
            {
                var currentDistance = Vector2.Distance(rb2D.position, unitsPosition[i].transform.position);
                
                if (currentDistance > unitDistance)
                {
                    unitsPosition.Add(null);

                    var saved = unitsPosition[i];
                    unitsPosition[i] = unitController;
                    
                    for (int j = i; j < unitsPosition.Count; j++)
                    {
                        (unitsPosition[j + 1], saved) = (saved, unitsPosition[j + 1]);
                    }

                    return;
                }

                i++;
            }
            
            unitsPosition.Add(unitController);
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

        public UnitCombatInfo GetUnitInfo() => UnitInfo;
        public GUID GetUniqueID() => UniqueID;
        public IBehaviorController GetUnitBehavior() => behaviorControllerController;

        public UnitParent GetEnemyType()
        {
            return Parent == UnitParent.Player ? UnitParent.Enemy : UnitParent.Player;
        }

        public UnitParent GetParentType() => Parent;
    }
}