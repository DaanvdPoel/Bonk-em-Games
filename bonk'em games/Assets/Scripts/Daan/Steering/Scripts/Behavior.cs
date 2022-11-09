using UnityEngine;

namespace Steering
{
    public abstract class Behavior : IBehavior
    {
        [Header("Behavior Runtime")]
        public Vector3 positionTarget = Vector3.zero;
        public Vector3 velocityDesired = Vector3.zero;

        public virtual void Start(BehaviorContext _context)
        {
            positionTarget = _context.position;
        }

        public abstract Vector3 CalculateSteeringForce(float dt, BehaviorContext _context);

        public virtual void OnDrawGizmos(BehaviorContext _context)
        {
            Support.DrawRay(_context.position, velocityDesired, Color.red);
        }
    }
}
