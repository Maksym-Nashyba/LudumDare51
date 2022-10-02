using System.Collections;
using NPCs.Navigation;
using UnityEngine;

namespace NPCs.AI
{
    public sealed class GuardNPC : HumanoidNPC
    {
        [SerializeField] private Waypoint[] _patrolPoints;
        [SerializeField] private Transform _gunPoint;

        protected override void Start()
        {
            base.Start();
            ChangeState(new PatrollingState(_patrolPoints));
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
            
            
            public override void Start(Context context)
            {
                throw new System.NotImplementedException();
            }

            public override IEnumerator Act()
            {
                throw new System.NotImplementedException();
            }

            public override bool IsRelaxed()
            {
                return false;
            }
            
            
        }
    }
}