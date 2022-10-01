using UnityEngine;

namespace Interactables
{
    public class AlarmBox : MonoBehaviour
    {
        public Vector3 Position => _transform.position;
        public bool Activated => _activated;
        private bool _activated = false;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Activate()
        {
            if (_activated) return;
            _activated = true;
        }
    }
}