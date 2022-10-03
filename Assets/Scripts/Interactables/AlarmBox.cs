using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class AlarmBox : MonoBehaviour
    {
        public UnityEvent AlarmActivated;
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
            AlarmActivated?.Invoke();
        }
    }
}