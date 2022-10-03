using Misc;
using NPCs;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Movement
{
    public sealed class RatMovement: PlayerMovement
    {
        private readonly LivingNPC _host;
        private readonly NavMeshAgent _agent;

        public RatMovement(LivingNPC host, NavMeshAgent agent)
        {
            _host = host;
            _agent = agent;
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
        
        public override void WarpPlayerToPoint(Vector3 point)
        {
            _agent.Warp(point);
        }
    }
}