using System.Linq;
using Shaders;
using UnityEngine;

namespace Misc
{
    public static class Raycasting
    {
        public static (RaycastHit hit, bool success) GetScreenRaycastHit()
        {
            try
            {
                Ray ray = ServiceLocator.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = new RaycastHit[5];
                _ = Physics.RaycastNonAlloc(ray, hits);
                RaycastHit hit = hits.First(hit => hit.transform is not null
                                                   && hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"));
                return new(hit, true);
            }
            catch
            {
                return new(new RaycastHit(), false);
            }
        }

        public static GameObject GetSelectedGameObject()
        {
            Ray ray = ServiceLocator.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform.gameObject;
            }
            return null;
        }

        public static (CameraObstruction cameraObstruction, bool success) CheckObstacleBetweenCameraAndPlayer()
        {
            Vector3 cameraPosition = ServiceLocator.Camera.transform.position;
            Ray ray = new Ray(cameraPosition,
                ServiceLocator.PlayerInstance.transform.position - cameraPosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            Physics.Raycast(ray, out RaycastHit hit);
            CameraObstruction cameraObstruction = hit.transform.gameObject.GetComponent<CameraObstruction>();
            if (cameraObstruction is null) return new(null, false);
            return new(cameraObstruction, true);
        }

        public static bool CheckObstacleBetween(Vector3 parasitePosition, GameObject prey)
        {
            Ray ray = new Ray(parasitePosition, prey.transform.position - parasitePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.transform.gameObject != prey) return false;
            return true;
        }
    }
}