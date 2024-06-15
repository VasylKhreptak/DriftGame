using System;
using Zenject;

namespace DebuggerOptions.Core
{
    public class ContextOptions : IInitializable, IDisposable
    {
        private bool _initialized;

        public void Initialize()
        {
            if (_initialized)
                return;

            SRDebug.Instance.AddOptionContainer(this);

            _initialized = true;
        }

        public void Dispose() => SRDebug.Instance?.RemoveOptionContainer(this);
    }
}