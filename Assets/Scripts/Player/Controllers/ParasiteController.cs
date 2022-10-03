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
        private float _minDistanceToPrey = 2f;
        
        public override void ApplyPlayerMovement()
        {
            PlayerMovement = new ParasiteMovement(GetComponent<NavMeshAgent>());
        }

        public override void GetShot()
        {
            ServiceLocator.Particles.Spawn(Particles.Type.Slime, Transform.position + Vector3.up*0.3f);
            gameObject.SetActive(false);
        }

        public override void Interact(RatDoor ratDoor)
        {
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) return;
            StartCoroutine(nameof(AnimateChangingHost), livingNpc);
        }

        protected IEnumerator AnimateChangingHost(LivingNPC livingNpc)
        {
            Vector3 preyPosition = livingNpc.transform.position;
            PlayerMovement.SetDestinationTo(preyPosition);
            yield return new WaitUntil(() => (preyPosition - Transform.position).sqrMagnitude < _minDistanceToPrey * _minDistanceToPrey);
            
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) yield break;
            PlayerMovement.TurnOffNavMesh();
            float distance = CheckDistanceTo(livingNpc.transform);
            while(distance > 1f)
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
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }

        protected override void LeaveHost()
        {
            
        }
    }
}