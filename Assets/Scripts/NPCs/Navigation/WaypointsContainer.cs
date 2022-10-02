using System;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using UnityEngine;
using UnityEngine.AI;

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
                float current = (transformPosition - waypoint.Position).sqrMagnitude;
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
            Waypoint excluded = GetClosestWaypoint(start);
            Waypoint[] byAlignment = GetWaypointsByAlignment(start, threat - start);
            Waypoint[] byDistance = GetWaypointsByDistance(start, threat);
            Waypoint[] byClosestCorner = GetWaypointsByClosestCorner(start, threat);

            (Waypoint waypoint, int score)[] points = new (Waypoint waypoint, int score)[Waypoints.Count];
            for (int i = 0; i < Waypoints.Count; i++)
            {
                points[i] = new(Waypoints[i], 0);
            }
            for (int i = 0; i < points.Length; i++)
            {
                AddToWaypoint(byAlignment[i], points.Length - i);
                AddToWaypoint(byDistance[i], i);
                AddToWaypoint(byClosestCorner[i], points.Length - i);
            }

            Waypoint result = points[0].waypoint;
            int best = 0;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].score > best)
                {
                    best = points[i].score;
                    result = points[i].waypoint;
                }
            }

            return result;
            
            void AddToWaypoint(Waypoint waypoint, int score)
            {
                if (waypoint == excluded) return;
                for (int i = 0; i < points.Length; i++)
                {
                    if(points[i].waypoint != waypoint)continue;
                    points[i].score += score;
                    break;
                }
            }
        }

        public Waypoint[] GetWaypointsByAlignment(Vector3 center, Vector3 direction)
        {
            (Waypoint waypoint, float angle)[] pairs = new (Waypoint waypoint, float number)[Waypoints.Count];
            for (int i = 0; i < pairs.Length; i++)
            {
                float angle = Vector3.Angle(direction, Waypoints[i].Position - center);
                pairs[i] = new (Waypoints[i], angle);
            }
            int j = 0;
            while (j < pairs.Length-1)
            {
                if (pairs[j].angle > pairs[j + 1].angle)
                {
                    (Waypoint waypoint, float angle) temp = pairs[j];
                    pairs[j] = pairs[j + 1];
                    pairs[j + 1] = temp;
                    j = 0;
                    continue;
                }
                j++;
            }
            Waypoint[] result = new Waypoint[pairs.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = pairs[i].waypoint;
            }
            return result;
        }

        public Waypoint[] GetWaypointsByDistance(Vector3 center, Vector3 threat)
        {
            (Waypoint waypoint, float distanceSqr)[] pairs = new (Waypoint waypoint, float distanceSqr)[Waypoints.Count];
            for (int i = 0; i < pairs.Length; i++)
            {
                float distanceSqr = (Waypoints[i].Position - center).sqrMagnitude;
                pairs[i] = new (Waypoints[i], distanceSqr);
            }
            int j = 0;
            while (j < pairs.Length-1)
            {
                if (pairs[j].distanceSqr > pairs[j + 1].distanceSqr)
                {
                    (Waypoint waypoint, float distanceSqr) temp = pairs[j];
                    pairs[j] = pairs[j + 1];
                    pairs[j + 1] = temp;
                    j = 0;
                    continue;
                }
                j++;
            }
            Waypoint[] result = new Waypoint[pairs.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = pairs[i].waypoint;
            }
            return result;
        }

        public Waypoint[] GetWaypointsByClosestCorner(Vector3 center, Vector3 threat)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3[] corners = new Vector3[3];
            (Waypoint waypoint, float distanceSqr)[] pairs = new (Waypoint waypoint, float distanceSqr)[Waypoints.Count];
            for (int i = 0; i < pairs.Length; i++)
            {
                NavMesh.CalculatePath(center, Waypoints[i].Position, NavMesh.AllAreas, path);
                path.GetCornersNonAlloc(corners);
                float distanceSqr = (corners[1] - center).sqrMagnitude;
                pairs[i] = new (Waypoints[i], distanceSqr);
            }
            int j = 0;
            while (j < pairs.Length-1)
            {
                if (pairs[j].distanceSqr > pairs[j + 1].distanceSqr)
                {
                    (Waypoint waypoint, float distanceSqr) temp = pairs[j];
                    pairs[j] = pairs[j + 1];
                    pairs[j + 1] = temp;
                    j = 0;
                    continue;
                }
                j++;
            }
            Waypoint[] result = new Waypoint[pairs.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = pairs[i].waypoint;
            }
            return result;
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