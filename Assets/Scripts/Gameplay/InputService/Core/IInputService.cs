namespace Gameplay.InputService.Core
{
    public interface IInputService
    {
        public float Horizontal { get; }
        public float Vertical { get; }

        public bool Enabled { get; }
    }
}