using Gameplay.InputService.Core;
using UnityEngine;

namespace Gameplay.InputService
{
    public class KeyboardInputService : IInputService
    {
        public float Horizontal => Enabled ? Input.GetAxisRaw("Horizontal") : 0;

        public float Vertical => Enabled ? Input.GetAxisRaw("Vertical") : 0;

        public bool HandBrake => Enabled && Input.GetKey(KeyCode.Space);

        public bool Enabled { get; set; }
    }
}