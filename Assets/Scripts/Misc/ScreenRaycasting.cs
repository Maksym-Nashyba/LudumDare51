using System.Linq;
using UnityEngine;

namespace Misc
{
    public static class ScreenRaycasting
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
    }
}