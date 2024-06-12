using Gameplay.InputService;
using Gameplay.InputService.Core;
using Gameplay.TimeManagement;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private ScreenInputService _screenInputService;

        public override void InstallBindings()
        {
            Container.Bind<LevelTimer>().AsSingle();
            BindInputService();
        }

        private void BindInputService()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Container.Bind<IInputService>().FromInstance(_screenInputService).AsSingle();
            else
                Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
        }
    }
}