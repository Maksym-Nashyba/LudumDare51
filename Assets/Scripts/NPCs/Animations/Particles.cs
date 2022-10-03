using System.Collections;
using UnityEngine;

namespace NPCs
{
    public class Particles : MonoBehaviour
    {
        [SerializeField] private GameObject _slimeParticlesPrefab;
        [SerializeField] private GameObject _ketchupParticlesPrefab;
        [SerializeField] private Transform _floor;
        
        public void Spawn(Type type, Vector3 position)
        {
            GameObject prefab = type == Type.Slime ? _slimeParticlesPrefab : _ketchupParticlesPrefab;
            ParticleSystem particleSystem = Instantiate(prefab, position, Quaternion.identity).GetComponent<ParticleSystem>();
            particleSystem.collision.AddPlane(_floor);
            StartCoroutine(DestroyAfter(particleSystem.main.startLifetimeMultiplier, particleSystem.gameObject));
        }

        private IEnumerator DestroyAfter(float seconds, GameObject toDestroy)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(toDestroy);
        }
        
        public enum Type
        {
            Slime,
            Ketchup
        }
    }
}