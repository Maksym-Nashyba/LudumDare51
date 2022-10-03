using Misc;
using UnityEngine;

namespace NPCs
{
    public class RatAnimations : NPCAnimations
    {
        public override void PlayStartInfection()
        {
            GFXAnimator.Play("RatStartInfection");
        }

        public override void PlayDeath(LivingNPC.DeathCauses cause)
        {
            if (cause == LivingNPC.DeathCauses.Shot || cause == LivingNPC.DeathCauses.ParasiteLeaving)
            {
                ServiceLocator.Particles.Spawn(Particles.Type.Ketchup, transform.position + Vector3.one * 0.2f);
            }
        }

        public override void PlayInteractAnimation(Vector3 target)
        {
            throw new System.NotImplementedException();
        }
    }
}