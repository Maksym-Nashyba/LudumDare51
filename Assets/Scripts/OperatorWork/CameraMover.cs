using Misc;
using UnityEngine;

namespace OperatorWork
{
    public class CameraMover : MonoBehaviour
    {
        private Transform _currentTarget;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = ServiceLocator.Camera.transform;
        }

        private void Update()
        {
            if (_currentTarget is not null)
            {
                _cameraTransform.position =
                    Vector3.Lerp(_cameraTransform.position, _currentTarget.position, 2f * Time.deltaTime);   
            }
            else
            {
                
            }
        }

        public void Stop()
        {
            _currentTarget = null;
        }
        
        public void LerpCameraTo(Transform position)
        {
            _currentTarget = position;
        }

        public float DistanceToCurrentTarget()
        {
            return Vector3.Distance(_cameraTransform.position, _currentTarget.position);
        }
    }
}