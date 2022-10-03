using UnityEngine;

namespace NPCs
{
    public class GuardAnimations : NPCAnimations
    {
        public override void PlayStartInfection()
        {
            GFXAnimator.Play("GuardStartInfection");
        }

        public override void PlayDeath(LivingNPC.DeathCauses cause)
        {
        }

        public override void PlayInteractAnimation(Vector3 target)
        {
            throw new System.NotImplementedException();
        }
    }
}