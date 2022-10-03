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
            throw new System.NotImplementedException();
        }
    }
}