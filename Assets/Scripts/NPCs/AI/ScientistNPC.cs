using System;
using System.Collections;
using Interactables;
using Misc;
using UnityEngine;

namespace NPCs.AI
{
    public sealed class ScientistNPC : HumanoidNPC
    {
        protected override void Awake()
        {
            base.Awake();
            Detector.Detected += OnDetected;
        }

        private void OnDetected(SuspiciousObject suspiciousObject)
        {
            switch (suspiciousObject.Type)
            {
                case SuspiciousObject.Types.Parasite:
                    AlarmBox alarmBox = ServiceLocator.WaypointsContainer.GetClosestInactiveAlarmBox(Transform.position);
                    if(alarmBox.Activated) ChangeState(new RunForLifeState(suspiciousObject.transform));
                    else ChangeState(new RunForAlarmState(alarmBox));
                    break;
                
                case SuspiciousObject.Types.Corpse:
                    break;
                
                case SuspiciousObject.Types.Blood:
                    break;
                
                default:
                    throw new NotImplementedException();
            }
        }

        private void ActivateAlarmBox(AlarmBox alarmBox)
        {
            alarmBox.Activate();
            Animations.PlayInteractAnimation(alarmBox.Position);
        }
        
        private class RunForAlarmState : State
        {
            private const float TargetDistanceToBox = 0.75f;
            private Context _context;
            private AlarmBox _alarmBox;
            private ScientistNPC _scientist;

            public RunForAlarmState(AlarmBox alarmBox)
            {
                _alarmBox = alarmBox;
            }

            public override void Start(Context context)
            {
                _context = context;
                _scientist = (ScientistNPC)context.NPC;
                _scientist.RunToPosition(_alarmBox.Position);
            }

            public override IEnumerator Act()
            {
                bool tooFar = true;
                while (tooFar)
                {
                    float distanceSqr = (_alarmBox.Position - _context.Transform.position).sqrMagnitude;
                    tooFar = distanceSqr > TargetDistanceToBox * TargetDistanceToBox;
                    yield return null;
                }
                _scientist.ActivateAlarmBox(_alarmBox);
                yield return new WaitForSeconds(1f);
                _scientist.ChangeState(new IdleState());
            }

            public override bool IsRelaxed()
            {
                return false;
            }
        }
    }
}