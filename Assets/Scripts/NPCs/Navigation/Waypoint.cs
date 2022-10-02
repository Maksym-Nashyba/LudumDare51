using UnityEngine;

namespace NPCs.Navigation
{
    public class Waypoint : MonoBehaviour
    {
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public Vector3 Position => _transform.position;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
    }
}