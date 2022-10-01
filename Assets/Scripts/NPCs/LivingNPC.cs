using System.Collections;
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

        protected virtual void WalkToPosition(Vector3 target)
        {
            NavMeshAgent.SetDestination(target);
        }

        protected virtual void Die(DeathCauses deathCause)
        {
            
        }

        public enum DeathCauses
        {
            Default,
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
            
            public override void OnStart(Context context)
            {
                _context = context;
                _idlingWaypoint = context.WaypointsContainer.GetClosestWaypoint(context.Transform.position);
            }

            public override IEnumerator Act()
            {
                
                
                yield return null;
            }

            public override void OnEnd()
            {
            }
        }
    }
}