using UnityEngine;

namespace Steering
{
    public class Wander : Behavior
    {
        private float wanderAngle;

        private float wanderCircleDistance;
        private float wanderCircleRadius;
        private float wanderNoiseAngle;

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            wanderCircleDistance = _context.steeringSettings.wanderCircleDistance;
            wanderCircleRadius =_context.steeringSettings.wanderCircleRadius;
            wanderNoiseAngle = _context.steeringSettings.wanderNoiseAngle;


            wanderAngle += Random.Range(-0.5f * wanderNoiseAngle  * Mathf.Deg2Rad, 0.5f * wanderNoiseAngle * Mathf.Deg2Rad);

            Vector3 centerOfCircle = _context.position + _context.velocity.normalized * wanderCircleDistance;

            Vector3 offset = new Vector3(wanderCircleRadius * Mathf.Cos(wanderAngle), 0, wanderCircleRadius * Mathf.Sin(wanderAngle));

            positionTarget = centerOfCircle + offset;
            velocityDesired = (positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;

            return velocityDesired - _context.velocity;
        }

        public override void OnDrawGizmos(BehaviorContext _context)
        {
            base.OnDrawGizmos(_context);
            Vector3 centerOfCircle = _context.position + _context.velocity.normalized * wanderCircleDistance;
            Support.DrawWireDisc(centerOfCircle, wanderCircleRadius, Color.black);

            float a = wanderNoiseAngle * Mathf.Deg2Rad;

            Vector3 rangeMin = new Vector3(wanderCircleRadius * Mathf.Cos(wanderAngle - a), 0, wanderCircleRadius * Mathf.Sin(wanderAngle - a));

            Vector3 rangeMax = new Vector3(wanderCircleRadius * Mathf.Cos(wanderAngle + a), 0, wanderCircleRadius * Mathf.Sin(wanderAngle + a));

            Debug.DrawLine(centerOfCircle, centerOfCircle + rangeMin, Color.black);
            Debug.DrawLine(centerOfCircle, centerOfCircle + rangeMax, Color.black);
        }
    }
}
