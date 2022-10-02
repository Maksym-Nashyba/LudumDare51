using Interactables;
using NPCs;
using Player.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Controllers
{
    public class ParasiteController : PlayerController
    {
        public override void ApplyPlayerMovement()
        {
            playerMovement = new ParasiteMovement(GetComponent<NavMeshAgent>());
        }
        
        public override void Interact(RatDoor ratDoor)
        {
            Vector3 position = ratDoor.GoThrough(transform.position);
            playerMovement.WarpPlayerToPoint(position);
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }
        
        protected override void LeaveHost()
        {
            
        }
    }
}