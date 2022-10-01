using UnityEngine;

namespace NPCs.Navigation
{
    public class Waypoint : MonoBehaviour
    {
        public Transform Transform => _transform;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
    }
}