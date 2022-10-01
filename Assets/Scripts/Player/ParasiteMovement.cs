using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class ParasiteMovement : PlayerMovement
    {
        private readonly Camera _camera;
        private readonly NavMeshAgent _agent;

        public ParasiteMovement(Camera camera, NavMeshAgent agent)
        {
            _camera = camera;
            _agent = agent;
        }
        
        public override void MovePlayerToPoint()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}