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
        
        public override void Interact(Door door)
        {
            Debug.Log("NO");
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