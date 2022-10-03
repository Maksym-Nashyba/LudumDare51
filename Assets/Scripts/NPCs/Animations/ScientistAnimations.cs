using Misc;
using UnityEngine;

namespace NPCs
{
    public class ScientistAnimations : NPCAnimations
    {
        public override void PlayStartInfection()
        {
            GFXAnimator.Play("ScientistStartInfection");
        }

        public override void PlayDeath(LivingNPC.DeathCauses cause)
        {
            //TODO make them lol
        }

        public override void PlayInteractAnimation(Vector3 target)
        {
            Vector3 cameraPosition = ServiceLocator.Camera.transform.position;
            if ((target - cameraPosition).sqrMagnitude < (transform.position - cameraPosition).sqrMagnitude)
            {
                GFXAnimator.Play("ScientistInteractDown");
                return;
            }
            GFXAnimator.Play("ScientistInteractUp");
        }
    }
}