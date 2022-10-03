using Misc;
using UnityEngine;

namespace OperatorWork
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        private Transform _currentTarget;
        private Transform _cameraTransform;
        private Transform _playerFollower;

        private void Awake()
        {
            _cameraTransform = ServiceLocator.Camera.transform;
            _playerFollower = new GameObject().transform;
        }

        private void Update()
        {
            _playerFollower.position = ServiceLocator.PlayerInstance.transform.position + _offset;
            if(ServiceLocator.GameLoop.IsPlayerControlled)
            {
                _cameraTransform.position =
                    Vector3.Lerp(_cameraTransform.position, _playerFollower.position, 2f * Time.deltaTime);
            }
            else if (_currentTarget is not null)
            {
                _cameraTransform.position =
                    Vector3.Lerp(_cameraTransform.position, _currentTarget.position, 2f * Time.deltaTime);   
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