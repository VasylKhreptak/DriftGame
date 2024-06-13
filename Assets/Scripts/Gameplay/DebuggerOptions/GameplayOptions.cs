using DebuggerOptions.Core;
using Gameplay.TimeManagement;

namespace Gameplay.DebuggerOptions
{
    public class GameplayOptions : ContextOptions
    {
        private readonly LevelTimer _levelTimer;

        public GameplayOptions(LevelTimer levelTimer)
        {
            _levelTimer = levelTimer;
        }

        public void CompleteTimer() => _levelTimer.Complete();
    }
}