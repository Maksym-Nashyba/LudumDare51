using System.Collections;
using Interactables;
using Misc;
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
            PlayerMovement = new ParasiteMovement(GetComponent<NavMeshAgent>());
        }

        public override void GetShot()
        {
            DestroyPlayer();
        }

        public override void Interact(RatDoor ratDoor)
        {
            if (!Raycasting.CheckObstacleBetween(transform.position, ratDoor.gameObject)) return;
            Vector3 position = ratDoor.GoThrough(transform.position);
            PlayerMovement.WarpPlayerToPoint(position);
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) return;
            StartCoroutine(AnimateChangingHost(livingNpc));
            ChangeComponentsOn(livingNpc);
        }

        protected IEnumerator AnimateChangingHost(LivingNPC livingNpc)
        {
            PlayerMovement.TurnOffNavMesh();
            float distance = CheckDistanceTo(livingNpc.transform);
            while(distance > 1.5f)
            {
                yield return new WaitForSeconds(Time.deltaTime * 5);
                transform.position = Vector3.Lerp(transform.position, livingNpc.transform.position, 0.1f);
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