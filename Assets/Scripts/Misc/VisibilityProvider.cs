using Shaders;
using UnityEngine;

namespace Misc
{
    public class VisibilityProvider : MonoBehaviour
    {
        private (CameraObstruction cameraObstruction, bool success) _raycastInfo;
        
        private void FixedUpdate()
        {
            _raycastInfo = Raycasting.CheckObstacleBetweenCameraAndPlayer();
            if(!_raycastInfo.success) return;
            _raycastInfo.cameraObstruction.MakeTransparentForSomeTime();
        }
    }
}