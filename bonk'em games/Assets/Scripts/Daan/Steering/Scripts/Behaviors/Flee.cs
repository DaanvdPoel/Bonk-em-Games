using UnityEngine;

namespace Steering
{
    public class Flee : Behavior
    {
        GameObject target;
        public Flee(GameObject _target)
        {
            target = _target;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext context)
        {
            if (target != null)
            {
                positionTarget = target.transform.position;
                velocityDesired = -(positionTarget - context.position).normalized * context.steeringSettings.maxDesiredVelocity;
            }
            else
            {
                velocityDesired = Vector3.zero;
            }

            return velocityDesired - context.velocity;
        }
    }
}