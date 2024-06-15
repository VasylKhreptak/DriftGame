using Infrastructure.Services.Scene.Core;
using Infrastructure.Services.StaticData.Core;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Scene
{
    public class SceneService : ISceneService
    {
        private readonly IStaticDataService _staticDataService;

        public SceneService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public bool IsInGarage() => _staticDataService.Config.GarageScene.Name == SceneManager.GetActiveScene().name;
    }
}