using UnityEngine;

namespace Steering
{
    public class Idle : Behavior
    {
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            positionTarget = _context.position + dt * _context.velocity;
            velocityDesired = Vector3.zero;

            return velocityDesired - _context.velocity;
        }
    }
}
