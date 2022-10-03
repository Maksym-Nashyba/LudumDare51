using System.Collections;
using Interactables;
using Misc;
using NPCs;
using Player.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Controllers
{
    public sealed class RatController: PlayerController
    {
        private float _minDistanceToRatDoor = 0.25f;
        private LivingNPC _host;
        private NavMeshAgent _navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            _host = GetComponent<LivingNPC>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override void ApplyPlayerMovement()
        {
            PlayerMovement = new RatMovement(_host, _navMeshAgent);
        }

        public override void MovePlayer()
        {
            StopApproachingDoor();
            base.MovePlayer();
        }

        public override void GetShot()
        {
            enabled = false;
            _host.Die(LivingNPC.DeathCauses.Shot);
        }

        private void StopApproachingDoor()
        {
            StopCoroutine(nameof(ApproachAndEnterRatDoor));
        }
        
        public override void Interact(RatDoor ratDoor)
        {
            StartCoroutine(nameof(ApproachAndEnterRatDoor), ratDoor);
        }

        private IEnumerator ApproachAndEnterRatDoor(RatDoor door)
        {
            Vector3 doorPosition = door.transform.position;
            _navMeshAgent.SetDestination(doorPosition);
            yield return new WaitUntil(() => (doorPosition - Transform.position).sqrMagnitude < _minDistanceToRatDoor * _minDistanceToRatDoor);
            Vector3 position = door.FindExitPosition(transform.position);
            PlayerMovement.WarpPlayerToPoint(position);
        }
        
        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!IsAlive) return;
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) return;
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }

        protected override void LeaveHost() 
        { 
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving); 
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
    }
}