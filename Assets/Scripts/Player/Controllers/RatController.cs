using System.Collections;
using Interactables;
using NPCs;
using Player.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Controllers
{
    public sealed class RatController: PlayerController
    {
        private readonly float _minDistanceToRatDoor = 0.25f;
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
            StopApproachingDoor();
            StartCoroutine(nameof(ApproachAndEnterRatDoor), ratDoor);
        }

        private IEnumerator ApproachAndEnterRatDoor(RatDoor ratDoor)
        {
            Vector3 doorPosition = ratDoor.transform.position + (transform.position - ratDoor.transform.position).normalized * 0.1f;
            _navMeshAgent.SetDestination(doorPosition);
            yield return new WaitUntil(() => (doorPosition - Transform.position).sqrMagnitude < _minDistanceToRatDoor * _minDistanceToRatDoor);
            Vector3 position = ratDoor.FindExitPosition(transform.position);
            PlayerMovement.WarpPlayerToPoint(position);
        }
        
        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!IsAlive) return;
            LeaveHost(livingNpc);
        }

        protected override void LeaveHost() 
        { 
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving); 
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
        
        protected void LeaveHost(LivingNPC livingNpc)
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            Player parasite = InstantiateParasite(_host.ParasiteTargetPoint).AddComponent<Player>();
            parasite.GetComponent<PlayerController>().Interact(livingNpc);
        }
    }
}