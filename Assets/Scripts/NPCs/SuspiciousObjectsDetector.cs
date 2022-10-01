using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace NPCs
{
    public class SuspiciousObjectsDetector : MonoBehaviour
    {
        public event Action<SuspiciousObject.Types> Detected;
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out SuspiciousObject suspiciousObject)) return;
            if (_previouslyDetected.Contains(suspiciousObject)) return;
            
            Detected?.Invoke(suspiciousObject.Type);
            _previouslyDetected.Add(suspiciousObject);
        }

        public RaycastHit ShootRayAtAngle(float angle)
        {
            Vector2 direction = Vector2.up.RotatedBy(angle);
            Ray ray = new Ray(_transform.position, new Vector3(direction.x, 0, direction.y));
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }
    }
}