using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(Rigidbody))]
    public class FountainDrop : MonoBehaviour
    {
        [SerializeField] private Material _lightMaterial;
        [SerializeField] private Material _darkMaterial;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private int _mask;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
            _mask = LayerMask.NameToLayer("Floor");
        }

        private void FixedUpdate()
        {
            Ray ray = new Ray(_transform.position, Vector3.down);
            Debug.DrawRay(_transform.position, Vector3.down, Color.red, 0.25f);
            if (Physics.Raycast(ray, out RaycastHit hit, 0.25f, _mask))
            {
                OnHitFloor(hit.point);
            }
        }

        public void Launch(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
            
        }
        
        private void OnHitFloor(Vector3 point)
        {
            Destroy(gameObject);
        }
    }
}