using System.Collections;
using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Debris : MonoBehaviour
    {
        [SerializeField] private float _activePhysicsTime = 3f;

        private void Start()
        {
            StartCoroutine(ActivateAfterDelay(_activePhysicsTime));
        }

        private IEnumerator ActivateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Collider>().enabled = false;
        }
    }
}