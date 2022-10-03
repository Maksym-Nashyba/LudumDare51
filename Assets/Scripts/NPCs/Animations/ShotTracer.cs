using System.Collections;
using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShotTracer : MonoBehaviour
    {
        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        public void DrawAt(Vector3 from, Vector3 to, float duration)
        {
            _line.SetPosition(0, from);
            _line.SetPosition(1, to);
            StartCoroutine(Fade(duration));
        }

        private IEnumerator Fade(float duration)
        {
            float startStartWidth = _line.startWidth;
            float passed = 0f;
            while (passed < duration)
            {
                _line.startWidth = startStartWidth-(startStartWidth * passed / duration);
                
                passed += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Destroy(gameObject);
        } 
    }
}