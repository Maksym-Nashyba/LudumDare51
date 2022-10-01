using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs.Navigation
{
    public class WaypointsContainer : MonoBehaviour
    {
        public IReadOnlyList<Waypoint> Waypoints => _waypoints;
        private List<Waypoint> _waypoints;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _waypoints = GetWaypointsFromChildren();
        }

        private List<Waypoint> GetWaypointsFromChildren()
        {
            return new List<Waypoint>(_transform.GetComponentsInChildren<Waypoint>());
        }


        public Waypoint GetClosestWaypoint(Vector3 transformPosition)
        {
            throw new NotImplementedException();
        }
    }
}