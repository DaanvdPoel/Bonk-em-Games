using UnityEngine;

namespace Steering
{
    public class Seek : Behavior
    {
        private GameObject target;
        public Seek(GameObject _target)
        {
            target = _target;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            if (target != null)
            {
                positionTarget = target.transform.position;
                velocityDesired = (positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;
            }
            else
            {
                velocityDesired = Vector3.zero;
            }
            return velocityDesired - _context.velocity;

        }

    }
}