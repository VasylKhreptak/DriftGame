using Gameplay.TimeManagement;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelTimer>().AsSingle();
        }
    }
}