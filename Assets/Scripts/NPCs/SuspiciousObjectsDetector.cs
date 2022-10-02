using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace NPCs
{
    public class SuspiciousObjectsDetector : MonoBehaviour
    {
        public event Action<SuspiciousObject> Detected;
        public event Action Disabled;
        public float Radius => _radius;
        [SerializeField] private float _radius;
        [SerializeField] private CapsuleCollider _trigger;
        private Transform _transform;
        private List<SuspiciousObject> _previouslyDetected;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _previouslyDetected = new List<SuspiciousObject>();
            _trigger.radius = _radius;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out SuspiciousObject suspiciousObject)) return;
            if (_previouslyDetected.Contains(suspiciousObject)) return;

            if (CanSee(suspiciousObject.transform))
            {
                Detect(suspiciousObject);
            }
        }

        private void Detect(SuspiciousObject suspiciousObject)
        {
            Detected?.Invoke(suspiciousObject);
            _previouslyDetected.Add(suspiciousObject);
        }
        
        public bool CanSee(Transform target)
        {
            Vector3 detectorPosition = _transform.position;
            Vector3 direction = target.position - detectorPosition;
            Ray ray = new Ray(detectorPosition, new Vector3(direction.x, 0, direction.z));
            Physics.Raycast(ray, out RaycastHit hit);
            Debug.DrawRay(detectorPosition, new Vector3(direction.x, 0, direction.z), Color.green);
            return hit.transform == target;
        }

        public RaycastHit ShootRayAtAngle(float angle)
        {
            Vector2 direction = Vector2.up.RotatedBy(angle);
            Ray ray = new Ray(_transform.position, new Vector3(direction.x, 0, direction.y));
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }

        public void Disable()
        {
            Disabled?.Invoke();
        }
    }
}