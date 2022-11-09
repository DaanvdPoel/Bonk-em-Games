using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(fileName = "Steering Settings", menuName = "Steering/ Steering Settings", order = 1)]
    public class SteeringSettings : ScriptableObject
    {
        public enum FPM {Forwards, Backwards, Random }

        [Header("Steering Settings")]
        public float mass = 70;
        public float maxDesiredVelocity = 3;
        public float maxSteeringForce = 4;
        public float maxSpeed = 3;

        [Header("Arrive")]
        public float arriveDistance = 1;
        public float slowingDistance = 2;

        [Header("Follow Path")]
        public FPM followPathMode = FPM.Forwards;
        public bool followPathLooping = false;
        public float followPathRadius = 2.5f;
        public string followPathTag = "";

        [Header("Pursuit and Evade")]
        public float lookAheadTime = 1;

        [Header("Wander")]
        public float wanderCircleDistance = 5;
        public float wanderCircleRadius = 5;
        public float wanderNoiseAngle = 10;
    }
}
