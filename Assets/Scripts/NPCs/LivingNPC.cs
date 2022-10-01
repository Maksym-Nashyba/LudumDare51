using System.Collections;
using Misc;
using NPCs.Navigation;
using UnityEngine;

namespace NPCs
{
    public class LivingNPC : NPC
    {
        private void Start()
        {
            ChangeState(new IdleState());
        }

        public virtual void WalkToPosition(Vector3 target)
        {
            NavMeshAgent.SetDestination(target);
        }

        public virtual void Die(DeathCauses deathCause)
        {
            
        }

        public enum DeathCauses
        {
            Default,
            Shot,
            ParasiteLeaving
        }
        
        protected override Context GetContext()
        {
            return new Context(WaypointsContainer, NavMeshAgent, Transform);
        }
        
        protected class IdleState : State
        {
            private Context _context;
            private Waypoint _idlingWaypoint;
            private Vector3 _idlingPosition;
            
            public override void OnStart(Context context)
            {
                _context = context;
                _idlingWaypoint = context.WaypointsContainer.GetClosestWaypoint(context.Transform.position);
            }

            public override IEnumerator Act()
            {
                while (true)
                {
                    _idlingPosition = Utils.GetPointInRadiusFlat(_idlingWaypoint.Transform.position, _idlingWaypoint.Radius);
                    _context.NavMeshAgent.SetDestination(_idlingPosition);
                    yield return new WaitForSeconds(Random.Range(2f, 8f));
                }
            }

            public override void OnEnd()
            {
            }
        }
        
        
    }
}