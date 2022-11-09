using UnityEngine;

namespace Steering
{
    public class SeekClickPoint : Behavior
    {
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                positionTarget = hit.point;
                positionTarget.y = _context.position.y;
            }

            velocityDesired = (positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;
            return velocityDesired - _context.velocity;
        }

    }
}