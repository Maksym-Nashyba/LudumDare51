using UnityEngine;

namespace Misc
{
    public static class ScreenRaycasting
    {
        public static RaycastHit GetScreenRaycastHit()
        {
            Ray ray = ServiceLocator.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            return hit;
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
    }
}