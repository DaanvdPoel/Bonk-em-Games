using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [RequireComponent(typeof(Steering))]
    public class HunterBrain : MonoBehaviour
    {
        public enum HunterState { Wander, Approach, Pursue}

        [Header("Target and Sate")]
        [SerializeField] private GameObject target;
        [SerializeField] private HunterState hunterState;
        [SerializeField] private float pursueRadius = 7;
        [SerializeField] private float approachRadius = 10;

        [Header("Steering Settings")]
        [SerializeField] private SteeringSettings wanderSettings;
        [SerializeField] private SteeringSettings approachSettings;
        [SerializeField] private SteeringSettings pursueSettings;

        [Header("Private")]
        private Steering steering;

        private void Start()
        {
            steering = GetComponent<Steering>();
        }

        private void FixedUpdate()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            switch (hunterState)
            {
                case HunterState.Wander:
                    if (distance < approachRadius)
                        ToApproach();
                        break;
                case HunterState.Approach:
                    if (distance > approachRadius)
                        ToWander();
                    else if (distance < pursueRadius)
                        ToPursue();
                        break;
                case HunterState.Pursue:
                    if (distance > pursueRadius)
                        ToApproach();
                    break;
            }
        }

        private void ToWander()
        {
            hunterState = HunterState.Wander;
            steering.settings = wanderSettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Wander());
            steering.SetBehaviors(behaviors, "Wander");
        }

        private void ToApproach()
        {
            hunterState = HunterState.Approach;
            steering.settings = approachSettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Seek(target));
            steering.SetBehaviors(behaviors, "Approach");
        }

        private void ToPursue()
        {
            hunterState = HunterState.Pursue;
            steering.settings = pursueSettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Pursue(target));
            steering.SetBehaviors(behaviors, "Pursue");
        }

        private void OnDrawGizmos()
        {
            Support.DrawWireDisc(transform.position, approachRadius, Color.cyan);
            Support.DrawWireDisc(transform.position, pursueRadius, Color.red);
        }
    }
}
