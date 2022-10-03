using System;
using System.Collections;
using Misc;
using NPCs.Navigation;
using Player.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPCs.AI
{
    public sealed class GuardNPC : HumanoidNPC
    {
        [SerializeField] private Waypoint[] _patrolPoints;
        [SerializeField] private GunPoint _gunPoint;

        protected override void Start()
        {
            base.Start();
            ChangeState(new PatrollingState(_patrolPoints));
            Detector.Detected += OnDetected;
        }

        private void OnDetected(SuspiciousObject obj)
        {
            switch (obj.Type)
            {
                case SuspiciousObject.Types.Parasite:
                    ChangeState(new HuntingState());
                    break;
                case SuspiciousObject.Types.Corpse:
                    break;
                case SuspiciousObject.Types.Blood:
                    break;
                case SuspiciousObject.Types.HiddenParasite:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void ShootAt(Transform transform)
        {
            if (transform.TryGetComponent(out PlayerController controller) && controller.enabled)
            {
                _gunPoint.DrawShotsTo(transform);
                controller.GetShot();
                Destroy(transform.gameObject);
                return;
            }
            if (!transform.TryGetComponent(out LivingNPC living)) return;
            _gunPoint.DrawShotsTo(living.ParasiteTargetPoint);
            living.Die(DeathCauses.Shot);
        }

        private void OnDisable()
        {
            Detector.Detected -= OnDetected;
        }

        private sealed class PatrollingState : State
        {
            private readonly Waypoint[] _patrolPoints;
            private int _currentPoint;
            private GuardNPC _npc;
            
            public PatrollingState(Waypoint[] patrolPoints)
            {
                _patrolPoints = patrolPoints;
            }

            public override void Start(Context context)
            {
                _npc = (GuardNPC)context.NPC;
            }

            public override IEnumerator Act()
            {
                while (true)
                {
                    _npc.WalkToPosition(_patrolPoints[_currentPoint].Position);
                    bool reachedPoint = false;
                    float minDistanceToTarget = _patrolPoints[_currentPoint].Radius / 2f;
                    minDistanceToTarget *= minDistanceToTarget;
                    while (!reachedPoint)
                    {
                        reachedPoint = (_npc.Transform.position - _patrolPoints[_currentPoint].Position).sqrMagnitude < minDistanceToTarget;
                        yield return null;
                    }

                    yield return new WaitForSeconds(Random.Range(7f, 15f));
                    _currentPoint = PickNextPoint(_currentPoint);
                }
            }

            private int PickNextPoint(int current)
            {
                if (current < _patrolPoints.Length - 1) return current+1;
                return 0;
            }

            public override bool IsRelaxed()
            {
                return true;
            }
        }

        private sealed class HuntingState : State
        {
            private GuardNPC _npc;
            
            private Transform Target => ServiceLocator.PlayerInstance.transform;
            
            public override void Start(Context context)
            {
                _npc = (GuardNPC)context.NPC;
                _npc.StopMoving();
                _npc.ShootAt(Target);
            }

            public override IEnumerator Act()
            {
                yield return new WaitForSeconds(2f);
                _npc.ChangeState(new PatrollingState(_npc._patrolPoints));
            }

            public override bool IsRelaxed()
            {
                return false;
            }
            
            
        }
    }
}