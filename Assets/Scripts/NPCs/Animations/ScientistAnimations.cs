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
    }
}