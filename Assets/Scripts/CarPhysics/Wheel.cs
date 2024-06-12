using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarPhysics
{
    [Serializable]
    public class Wheel
    {
        public WheelCollider Collider;
        public Transform Transform;
        public bool CanSteer;
        public bool CanDrive;
        public bool CanBrake;
        [ShowIf(nameof(CanBrake))] public bool CanHandBrake;
    }
}