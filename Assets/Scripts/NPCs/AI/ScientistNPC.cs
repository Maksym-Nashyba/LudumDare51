using System;
using System.Collections;
using Interactables;

namespace NPCs.AI
{
    public sealed class ScientistNPC : LivingNPC
    {
        protected override void Awake()
        {
            base.Awake();
            Detector.Detected += OnDetected;
        }

        private void OnDetected(SuspiciousObject.Types types)
        {
            switch (types)
            {
                case SuspiciousObject.Types.Parasite:
                    
                    break;
                case SuspiciousObject.Types.Corpse:
                    break;
                case SuspiciousObject.Types.Blood:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        private void ActivateAlarmBox(AlarmBox alarmBox)
        {
            alarmBox.Activate();
        }
        
        private class RunForAlarmState : State
        {
            private const float TargetDistanceToBox = 0.5f;
            private Context _context;
            private AlarmBox _alarmBox;
            private ScientistNPC _scientist;
            
            public override void OnStart(Context context)
            {
                _context = context;
                _scientist = (ScientistNPC)context.NPC;
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
            }

            public override void OnEnd()
            {
                throw new NotImplementedException();
            }
        }
    }
}