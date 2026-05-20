using DefaultNamespace.Pathfiender;
using Game.Core.BaseTurnController;
using Game.PatternCombat;
using Game.PatternCombat.BattleUnitSystem;
using Game.PatternCombat.Grid.Services;
using Game.PatternCombat.TrunControllers;
using Grid;
using UnityEngine;
using Zenject;

namespace ZenjectProviders
{
    public class CombatProvider : MonoInstaller
    {
        [SerializeField] private GridSystem gridSystem;
        [SerializeField] private Bfs bfs;
        
        public override void InstallBindings()
        {
            var unitRegister = new UnitsRegister();
            Container.Bind<UnitsRegister>().FromInstance(unitRegister).AsSingle();
            Container.Bind<IUnitRegister>().FromInstance(unitRegister).AsSingle();
            Container.Bind<IRegisterUpdate>().FromInstance(unitRegister).AsSingle();

            var pathService = new PathService(bfs, gridSystem);
            Container.Bind<IPathService>().FromInstance(pathService);
            
            Container.Bind<TurnFactory>().AsSingle();
            Container.Bind<GridQuery>().AsSingle();
        }
    }
}