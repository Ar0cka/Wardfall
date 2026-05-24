using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.BaseUnits;
using Game.PatternCombat.TrunControllers;
using UnityEngine;

namespace Game.PatternCombat.Units
{
    public class UnitController : BaseUnitController
    {
        public override async UniTask Action(IPathService pathService, List<BaseUnitController> enemyUnits)
        {
            var enemyUnit = behaviorControllerController.ChooseTarget(enemyUnits, UnitInfoProvider);
            
            var enemyPosition = enemyUnit.GetUnitInfo().UnitPosition;
            var currentPosition = GetUnitInfo().UnitPosition;
            
            var pathToUnit =
                pathService.FindPath(currentPosition.x, currentPosition.y, enemyPosition.x, enemyPosition.y);

            ActionPoints = GetUnitInfo().UnitWorldConfig.unitConfig.Stats.actionPoints;
            
            foreach (var path in pathToUnit)
            {
                Debug.Log($"Current path == {GetUnitInfo().UnitPosition.worldPosition} and target path = {path.worldPosition}");
                
                if (GetUnitInfo().UnitPosition.worldPosition == path.worldPosition)
                    continue;
                
                if (ActionPoints <= 0)
                    break;
                
                if (GridQuery.IsAdjacent8(enemyPosition, GetUnitInfo().UnitPosition))
                {
                    ActionPoints = 0;
                    break;
                }
                
                await UnitMove(path);
                //TODO Unit decreasing action point
                //TODO next iteration
                //TODO Check around units and leave if having other units with low path (Только 1 раз)
            }
        }
    }
}