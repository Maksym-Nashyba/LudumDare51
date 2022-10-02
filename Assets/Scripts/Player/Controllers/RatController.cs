using Interactables;
using Misc;
using NPCs;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class RatController: PlayerController
    {
        private LivingNPC _host;
        
        private void Awake()
        {
            _host = GetComponent<LivingNPC>();
        }
        
        public override void ApplyPlayerMovement()
        {
            playerMovement = new RatMovement(_host);
        }
        
        public override void GetShot()
        {
            enabled = false;
            _host.Die(LivingNPC.DeathCauses.Shot);
        }
        
        public override void Interact(RatDoor ratDoor)
        {
            if (!Raycasting.CheckObstacleBetween(transform.position, ratDoor.gameObject)) return;
            ratDoor.GoThrough(transform.position);
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!isAlive) return;
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) return;
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