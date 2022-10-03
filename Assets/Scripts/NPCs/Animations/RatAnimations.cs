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
        }
    }
}