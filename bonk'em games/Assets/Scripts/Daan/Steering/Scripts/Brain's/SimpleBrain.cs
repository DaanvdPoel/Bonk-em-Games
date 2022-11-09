using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [RequireComponent(typeof(Steering))]
    public class SimpleBrain : MonoBehaviour
    {
        public enum BehaviorEnum {Keyboard, SeekClickPoint, Seek, Flee, Pursue, Evade, Wander, FollowPath, Hide, Arrive, Idle, NotSet}

        [Header("Manual")]
        public BehaviorEnum behavior;
        public GameObject target;

        [Header("Private")]
        private Steering steering;
        List<IBehavior> behaviors = new List<IBehavior>();

        [Header("Follow Path")]
        public List<Transform> waypoints;

        [Header("Steering Settings")]
        [SerializeField] private SteeringSettings wanderSettings;
        [SerializeField] private SteeringSettings approachSettings;
        [SerializeField] private SteeringSettings pursueSettings;


        public SimpleBrain()
        {
            behavior = BehaviorEnum.NotSet;
            target = null;
        }

        public void Start()
        {
            if(behavior == BehaviorEnum.Keyboard || behavior == BehaviorEnum.SeekClickPoint)
            {
                target = null;
            }
            else
            {
                if(target == null)
                {
                    target = GameObject.Find("Player");
                }
                if(target == null)
                {
                    target = GameObject.Find("target");
                }
            }

            steering = GetComponent<Steering>();

            SetBehavior();
        }

        public void SetBehavior()
        {
            switch (behavior)
            {
                case BehaviorEnum.Keyboard:
                    behaviors.Add(new KeyBoard());
                    steering.SetBehaviors(behaviors, "Keyboard");
                    break;

                case BehaviorEnum.SeekClickPoint:
                    behaviors.Add(new SeekClickPoint());
                    steering.SetBehaviors(behaviors, "SeekClickPoint");
                    break;

                case BehaviorEnum.Seek:
                    behaviors.Add(new Seek(target));
                    steering.SetBehaviors(behaviors, "Seek");
                    steering.settings = approachSettings;
                    break;

                case BehaviorEnum.Flee:
                    behaviors.Add(new Flee(target));
                    steering.SetBehaviors(behaviors, "Flee");
                    break;

                case BehaviorEnum.FollowPath:
                    behaviors.Add(new FollowPath(waypoints, steering.settings));
                    steering.SetBehaviors(behaviors, "FollowPath");
                    break;

                case BehaviorEnum.Evade:
                    behaviors.Add(new Evade(target));
                    steering.SetBehaviors(behaviors, "Evade");
                    break;

                case BehaviorEnum.Pursue:
                    behaviors.Add(new Pursue(target));
                    steering.settings = pursueSettings;
                    steering.SetBehaviors(behaviors, "Pursue");
                    break;

                case BehaviorEnum.Arrive:
                    behaviors.Add(new Arrive(target));
                    steering.SetBehaviors(behaviors, "Arrive");
                    break;

                case BehaviorEnum.Wander:
                    behaviors.Add(new Wander());
                    steering.SetBehaviors(behaviors, "Wander");
                    steering.settings = wanderSettings;
                    break;

                case BehaviorEnum.Idle:
                    behaviors.Add(new Idle());
                    steering.SetBehaviors(behaviors, "Idle");
                    break;

                default:
                    Debug.LogError($"Behavior of type {behavior} not implemented yet!");
                    break;
            }
        }
    }
}
