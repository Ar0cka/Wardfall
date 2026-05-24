using Game.Core.Registers;
using Game.Core.Registers.PatternsRegister;
using Zenject;

namespace ZenjectProviders
{
    public class CombatRegistersProvider : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UnitBehaviorRegister>().AsSingle();
            Container.Bind<GeneralPatternRegister>().AsSingle();
            Container.Bind<SamplePatternRegister>().AsSingle();
        }
    }
}