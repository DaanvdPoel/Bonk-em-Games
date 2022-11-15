using UnityEngine;

namespace Steering
{
    public class Arrive : Behavior
    {
        private GameObject target;
        public Arrive(GameObject _target)
        {
            target = _target;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            Vector3 positionTarget = target.transform.position;
            positionTarget.y = _context.position.y;

            Vector3 stopVector = (_context.position - positionTarget).normalized * _context.steeringSettings.arriveDistance;
            Vector3 stopPosition = positionTarget + stopVector;


            Vector3 targetOffset = stopPosition - _context.position;
            float distance = Vector3.Distance(_context.position, positionTarget);
            float rampedSpeed = _context.steeringSettings.maxDesiredVelocity * (distance / _context.steeringSettings.slowingDistance);
            float clippedSpeed = Mathf.Min(rampedSpeed, _context.steeringSettings.maxDesiredVelocity);

            if(distance == 0)
            {
                velocityDesired = Vector3.zero;
                return velocityDesired;
            }
            else
            {
                velocityDesired = (clippedSpeed / distance) * targetOffset;
                return velocityDesired - _context.velocity;
            }
        }
        
        public override void OnDrawGizmos(BehaviorContext _context)
        {
            Support.DrawWireDisc(target.transform.position, _context.steeringSettings.slowingDistance, Color.cyan);
            Support.DrawWireDisc(target.transform.position, _context.steeringSettings.arriveDistance, Color.cyan);
        }
    }
}