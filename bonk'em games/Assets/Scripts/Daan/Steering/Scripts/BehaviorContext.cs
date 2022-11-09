using UnityEngine;

namespace Steering
{
    public class BehaviorContext
    {
        public Vector3 position;
        public Vector3 velocity;
        public SteeringSettings steeringSettings;

        public BehaviorContext(Vector3 _position, Vector3 _velocity, SteeringSettings _steeringSettings)
        {
            position = _position;
            velocity = _velocity;
            steeringSettings = _steeringSettings;
        }
    }
}
