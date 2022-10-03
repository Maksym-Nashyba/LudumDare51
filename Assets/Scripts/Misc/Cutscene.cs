using System;
using System.Collections;
using OperatorWork;
using UnityEngine;

namespace Misc
{
    public class Cutscene: MonoBehaviour
    {
        [SerializeField] private Transform[] _cameraTargets;
        [SerializeField] private float _duration = 1f;
        protected CameraMover _mover;

        protected virtual void Awake()
        {
            _mover = ServiceLocator.Camera.GetComponent<CameraMover>();
        }

        public virtual void Show(Action callback)
        {
            StartCoroutine(nameof(FlyBetweenTargets), callback);
        }

        public virtual void Abort()
        {
            StopCoroutine(nameof(FlyBetweenTargets));
            _mover.Stop();
        }
        
        private IEnumerator FlyBetweenTargets(Action callback)
        {
            GameObject temp = new GameObject();
            Transform tempTransform = temp.transform;
            tempTransform.position = ServiceLocator.Camera.transform.position;
            for (int i = 0; i < _cameraTargets.Length; i++)
            {
                _mover.LerpCameraTo(_cameraTargets[i]);
                yield return new WaitUntil(() => _mover.DistanceToCurrentTarget() < 0.5f);
                yield return new WaitForSeconds(_duration);
            }
            _mover.LerpCameraTo(tempTransform);
            yield return new WaitUntil(() => _mover.DistanceToCurrentTarget() < 0.5f);
            Destroy(temp);
            callback?.Invoke();
        }
    }
}