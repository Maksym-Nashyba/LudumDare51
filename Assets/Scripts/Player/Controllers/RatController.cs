using System.Collections;
using Interactables;
using NPCs;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class RatController: PlayerController
    {
        private LivingNPC _host;
        
        private void OnEnable()
        {
            _host = GetComponent<LivingNPC>();
        }
        
        public override void ApplyPlayerMovement()
        {
            playerMovement = new RatMovement(_host);
        }
        
        public override void Interact(RatDoor ratDoor)
        {
            ratDoor.GoThrough(transform.position);
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }

        protected override IEnumerator PlayAnimation(LivingNPC livingNpc)
        {
            throw new System.NotImplementedException();
        }

        protected override void LeaveHost() 
        { 
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving); 
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
    }
}