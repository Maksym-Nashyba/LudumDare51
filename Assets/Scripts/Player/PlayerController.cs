using Misc;
using NPCs;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public abstract class PlayerController : MonoBehaviour
    {
        protected PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = new ParasiteMovement(ServiceLocator.Camera, GetComponent<NavMeshAgent>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerMovement.MovePlayerToPoint();
            }
        }

        public virtual void ChangeHost(LivingNPC livingNpc)
        {
            playerMovement = new HostMovement(ServiceLocator.Camera, livingNpc);
        }
    }
}
