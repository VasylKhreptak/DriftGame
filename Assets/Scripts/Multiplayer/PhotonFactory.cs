using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Multiplayer
{
    public class PhotonFactory : IFactory<string, Vector3, Quaternion, byte, object[], GameObject>
    {
        private readonly DiContainer _container;

        public PhotonFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(string prefabName, Vector3 position, Quaternion rotation, byte group = 0, object[] data = null)
        {
            GameObject gameObject = PhotonNetwork.Instantiate(prefabName, position, rotation, group, data);

            GameObjectContext context = gameObject.GetComponent<GameObjectContext>();

            if (context == null)
                gameObject.AddComponent<GameObjectContext>();

            _container.InjectGameObject(gameObject);

            return gameObject;
        }
    }
}