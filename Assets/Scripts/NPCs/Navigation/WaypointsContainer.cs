using System;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using UnityEngine;

namespace NPCs.Navigation
{
    public class WaypointsContainer : MonoBehaviour
    {
        public IReadOnlyList<Waypoint> Waypoints => _waypoints;
        private List<Waypoint> _waypoints;
        private List<AlarmBox> _alarmBoxes;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _waypoints = new List<Waypoint>(_transform.GetComponentsInChildren<Waypoint>());
            _alarmBoxes = new List<AlarmBox>(_transform.GetComponentsInChildren<AlarmBox>());
        }

        public Waypoint GetClosestWaypoint(Vector3 transformPosition)
        {
            float closest = float.MaxValue;
            Waypoint best = _waypoints[0];
            foreach (Waypoint waypoint in _waypoints)
            {
                float current = (transformPosition - waypoint.Transform.position).sqrMagnitude;
                if (current  < closest)
                {
                    closest = current;
                    best = waypoint;
                }
            }
            return best;
        }

        public Waypoint GetBestWaypointForEscape(Vector3 start, Vector3 threat)
        {
            throw new NotImplementedException();
        }

        public Waypoint GetWaypointsByAlignment()
        {
            throw new NotImplementedException();
        }

        public Waypoint[] GetWaypointsByDistance()
        {
            throw new NotImplementedException();
        }

        public Waypoint[] GetWaypointsByClosestEdge()
        {
            throw new NotImplementedException();
        }
        
        public AlarmBox GetClosestInactiveAlarmBox(Vector3 transformPosition)
        {
            float closest = float.MaxValue;
            AlarmBox best = _alarmBoxes[0];
            foreach (AlarmBox box in _alarmBoxes)
            {
                float current = (transformPosition - box.Position).sqrMagnitude;
                if (current  < closest)
                {
                    closest = current;
                    best = box;
                }
            }
            return best;
        }
    }
}