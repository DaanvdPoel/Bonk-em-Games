using UnityEngine;

namespace Steering
{
    public interface IBehavior
    {
        void Start(BehaviorContext context);

        Vector3 CalculateSteeringForce(float dt, BehaviorContext _context);

        void OnDrawGizmos(BehaviorContext context);
    }
}
