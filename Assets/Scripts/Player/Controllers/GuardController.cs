using Interactables;
using NPCs;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class GuardController: PlayerController
    {
        private LivingNPC _host;
        
        private void OnEnable()
        {
            _host = GetComponent<LivingNPC>();
        }
        
        public override void ApplyPlayerMovement() 
        {
            playerMovement = new HumanoidMovement(_host);
        }
        
        public override void Interact(Door door)
        {
            door.OpenDoor();
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }
        
        protected override void LeaveHost()
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
    }
}