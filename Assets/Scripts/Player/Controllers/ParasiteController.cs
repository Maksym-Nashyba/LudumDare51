using System.Collections;
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
            StartCoroutine(PlayAnimation(livingNpc));
            ChangeComponentsOn(livingNpc);
        }

        protected override IEnumerator PlayAnimation(LivingNPC livingNpc)
        {
            playerMovement.TurnOffNavMesh();
            float distance = CheckDistanceTo(livingNpc.transform);
            while(distance > 1.5f)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, livingNpc.transform.position, 0.15f);
                distance = CheckDistanceTo(livingNpc.transform);
            }
            while(distance >= 0.15f)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, livingNpc.ParasiteTargetPoint.position, 0.12f);
                distance = CheckDistanceTo(livingNpc.ParasiteTargetPoint);
            }
            DestroyPlayer();
        }

        protected override void LeaveHost()
        {
            
        }
    }
}