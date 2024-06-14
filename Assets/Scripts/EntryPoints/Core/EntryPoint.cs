using UnityEngine;

namespace EntryPoints.Core
{
    public abstract class EntryPoint : MonoBehaviour
    {
        #region MonoBehaviour

        private void Start() => Enter();

        #endregion

        protected abstract void Enter();
    }
}