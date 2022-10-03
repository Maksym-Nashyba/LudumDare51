using Misc;
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
            Vector3 cameraPosition = ServiceLocator.Camera.transform.position;
            if ((target - cameraPosition).sqrMagnitude < (transform.position - cameraPosition).sqrMagnitude)
            {
                GFXAnimator.Play("GuardInteractDown");
                return;
            }
            GFXAnimator.Play("GuardInteractUp");
        }
    }
}