using Misc;
using NPCs;
using UnityEngine;

namespace Player.Movement
{
    public sealed class RatMovement: PlayerMovement
    {
        private readonly LivingNPC _host;

        public RatMovement(LivingNPC host)
        {
            _host = host;
        }

        public override void SetPlayerDestination()
        {
            (RaycastHit hit, bool success) hitInfo = Raycasting.GetScreenRaycastHit();
            if(!hitInfo.success) return;
            _host.WalkToPosition(hitInfo.hit.point);
        }

        public override void TurnOffNavMesh()
        {
            _host.enabled = false;
        }
    }
}