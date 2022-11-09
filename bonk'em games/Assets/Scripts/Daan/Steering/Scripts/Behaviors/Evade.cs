using UnityEngine;

namespace Steering
{
    public class Evade : Behavior
    {
        private GameObject target;
        private Vector3 prevTargetPosition;
        public Evade(GameObject _target)
        {
            target = _target;
            prevTargetPosition = _target.transform.position;
        }
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {

            Vector3 targetSpeed = (target.transform.position - prevTargetPosition) / dt;

            positionTarget = target.transform.position + targetSpeed * _context.steeringSettings.lookAheadTime;
            velocityDesired = -(positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;

            prevTargetPosition = target.transform.position;
            return velocityDesired - _context.velocity;
        }

    }
}