using Misc;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Movement
{
    public sealed class ParasiteMovement : PlayerMovement
    {
        private readonly NavMeshAgent _agent;

        public ParasiteMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }
        
        public override void MovePlayerToPoint()
        {
            RaycastHit hit = ScreenRaycasting.GetScreenRaycastHit();
            _agent.SetDestination(hit.point);
        }
    }
}