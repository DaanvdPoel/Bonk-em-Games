using UnityEngine;

namespace Steering
{
    public class KeyBoard : Behavior
    {
        override public Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            Vector3 requested_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") * 3);

            if(requested_direction != Vector3.zero)
            {
                positionTarget = _context.position + requested_direction.normalized * _context.steeringSettings.maxDesiredVelocity;
            }
            else
            {
                positionTarget = _context.position;
            }

            velocityDesired = (positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;
            return requested_direction - _context.velocity;
        }
    }
}