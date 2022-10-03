using System.Collections;
using Misc;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public class GunPoint : MonoBehaviour
    {
        public UnityEvent Shot;
        [SerializeField] private Transform _barrelEnd;
        [SerializeField] private GameObject _tracerPrefab;
        public void DrawShotsTo(Transform position)
        {
            StartCoroutine(ShootWithInterval(position, 5, 0.2f));
        }

        private IEnumerator ShootWithInterval(Transform target, int shots,float medianInterval)
        {
            int shot = 0;
            while (shot < shots)
            {
                Shot?.Invoke();
                Vector3 shotsTarget = target.position + (Vector3)Vector2.up.RotatedBy(Random.Range(0f, 300f)) * 0.15f;
                ShotTracer tracer = Instantiate(_tracerPrefab).GetComponent<ShotTracer>();
                tracer.DrawAt(_barrelEnd.position, shotsTarget, 0.2f);
                yield return new WaitForSeconds(Random.Range(medianInterval/2f, medianInterval + medianInterval/2f));
                shot++;
            }
        }
    }
}