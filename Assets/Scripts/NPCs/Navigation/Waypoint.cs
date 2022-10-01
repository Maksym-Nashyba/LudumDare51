using UnityEngine;

namespace NPCs.Navigation
{
    public class Waypoint : MonoBehaviour
    {
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public Transform Transform => _transform;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
    }
}