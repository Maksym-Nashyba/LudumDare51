using Misc;
using NPCs;
using UnityEngine;

namespace Player.Movement
{
    public sealed class HumanoidMovement: PlayerMovement
    {
        private readonly LivingNPC _host;

        public HumanoidMovement(LivingNPC host)
        {
            _host = host;
        }

        public override void MovePlayerToPoint()
        {
            RaycastHit hit = ScreenRaycasting.GetScreenRaycastHit();
            _host.WalkToPosition(hit.point);
        }
    }
}