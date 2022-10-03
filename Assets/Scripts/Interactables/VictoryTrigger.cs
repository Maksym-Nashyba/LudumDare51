using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class VictoryTrigger : MonoBehaviour
    {
        public UnityEvent Activated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                GetComponent<BoxCollider>().enabled = false;
                Activated?.Invoke();
            }
        }
    }
}