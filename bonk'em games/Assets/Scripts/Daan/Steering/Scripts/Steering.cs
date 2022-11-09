using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    using BehaviorList = List<IBehavior>;
    public class Steering : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField] string label;
        public SteeringSettings settings;

        [Header("Steering Runtime")]
        [SerializeField] private Vector3 position = Vector3.zero;
        [SerializeField] private Vector3 velocity = Vector3.zero;
        [SerializeField] private Vector3 steering = Vector3.zero;

        [SerializeField] private BehaviorList behaviors = new BehaviorList();

        private void Start()
        {
            position = transform.position;
        }

        private void FixedUpdate()
        {
            steering = Vector3.zero;
            foreach (IBehavior behavior in behaviors)
            {
                steering += behavior.CalculateSteeringForce(Time.fixedDeltaTime, new BehaviorContext(position, velocity, settings));
            }

            steering.y = 0;

            steering = Vector3.ClampMagnitude(steering, settings.maxSteeringForce);
            steering /= settings.mass;

           //Debug.Log(steering);

            velocity = Vector3.ClampMagnitude(velocity + steering, settings.maxSpeed);
            position += velocity * Time.fixedDeltaTime;

            transform.position = position;
            transform.LookAt(position + Time.fixedDeltaTime * velocity);
        }

        private void OnDrawGizmos()
        {
            Support.DrawRay(transform.position, velocity, Color.blue);
            Support.DrawLabel(transform.position, name, Color.white);

            foreach(IBehavior behavior in behaviors)
            {
                behavior.OnDrawGizmos(new BehaviorContext(position, velocity, settings));
            }
        }

        public void SetBehaviors(BehaviorList _behaviors, string _label = "")
        {
            label = _label; 
            behaviors = _behaviors;

            foreach (IBehavior behavior in behaviors)
            {
                behavior.Start(new BehaviorContext(position, velocity, settings)); 
            }
        }

    }
}

