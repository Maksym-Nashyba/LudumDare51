using System;

namespace NPCs.AI
{
    public class RatNPC : LivingNPC
    {
        protected override void Start()
        {
            base.Start();
            Detector.Detected += OnDetected;
        }

        private void OnDetected(SuspiciousObject obj)
        {
            switch (obj.Type)
            {
                case SuspiciousObject.Types.Parasite:
                    ChangeState(new RunForLifeState(obj.transform));
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
    }
}