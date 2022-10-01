using NPCs;
using UnityEngine;

namespace Player
{
    public class HostMovement: PlayerMovement
    {
        private readonly Camera _camera;
        private readonly LivingNPC _host;

        public HostMovement(Camera camera, LivingNPC host)
        {
            _camera = camera;
            _host = host;
        }

        public override void MovePlayerToPoint()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _host.WalkToPosition(hit.point);
            }
        }
    }
}