using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    public class FollowPath : Behavior
    {
        private List<Transform> waypoints;
        private int currentWaypointNumber = 0;
        private Transform currentWaypoint;

        private SteeringSettings settings;
        public FollowPath(List<Transform> _waypoints, SteeringSettings _settings)
        {
            waypoints = _waypoints;
            settings = _settings;
        }

        public override void Start(BehaviorContext _context)
        {
            if (settings.followPathMode == SteeringSettings.FPM.Forwards)
            {
                currentWaypointNumber = 0;
            }
            else if(settings.followPathMode == SteeringSettings.FPM.Backwards)
            {
                currentWaypointNumber = waypoints.Count - 1;
            }

            currentWaypoint = waypoints[currentWaypointNumber];
        }

        private void WayPoints(Vector3 _position)
        {
            float distance = Vector3.Distance(_position, currentWaypoint.position);
            if(distance <= settings.followPathRadius && distance != 0)
            {
                if (settings.followPathMode == SteeringSettings.FPM.Forwards)
                {
                    currentWaypointNumber = currentWaypointNumber + 1;

                    if (currentWaypointNumber > waypoints.Count - 1 && settings.followPathLooping == true)
                        currentWaypointNumber = 0;
                    else if (currentWaypointNumber > waypoints.Count - 1)
                        return;

                }else if(settings.followPathMode == SteeringSettings.FPM.Backwards)
                {
                    currentWaypointNumber = currentWaypointNumber - 1;

                    if (currentWaypointNumber < 0 && settings.followPathLooping == true)
                        currentWaypointNumber = waypoints.Count - 1;
                    else if (currentWaypointNumber < 0)
                        return;

                }else if (settings.followPathMode == SteeringSettings.FPM.Random)
                {
                    currentWaypointNumber = Random.Range(0,waypoints.Count - 1);
                }

                    currentWaypoint = waypoints[currentWaypointNumber];
            }
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            WayPoints(_context.position);
            positionTarget = currentWaypoint.position;
            positionTarget.y = _context.position.y;
            velocityDesired = (positionTarget - _context.position).normalized * _context.steeringSettings.maxDesiredVelocity;

            return velocityDesired - _context.velocity;
        }

        public override void OnDrawGizmos(BehaviorContext _context)
        {
            foreach (Transform waypoint in waypoints)
            {
                Support.DrawWireDisc(waypoint.position, settings.followPathRadius, Color.magenta);
                Support.DrawLabel(waypoint.position, settings.followPathTag, Color.black);
                Support.ChangeObjectColor(waypoint.gameObject, Color.white);
            }

            Support.ChangeObjectColor(currentWaypoint.gameObject, Color.red);
        }
    }
}
