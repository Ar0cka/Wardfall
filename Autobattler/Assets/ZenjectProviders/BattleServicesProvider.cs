using Game.Services;
using Zenject;

namespace ZenjectProviders
{
    public class BattleServicesProvider : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WeightCalculator>().AsSingle();
        }
    }
}