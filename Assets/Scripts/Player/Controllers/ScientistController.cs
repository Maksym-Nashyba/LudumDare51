using Interactables;
using Misc;
using NPCs;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class ScientistController: PlayerController
    {
        private LivingNPC _host;

        private void Awake()
        {
            _host = GetComponent<LivingNPC>();
        }
        
        public override void ApplyPlayerMovement()
        {
            playerMovement = new HumanoidMovement(_host);
        }

        public override void GetShot()
        {
            enabled = false;
            _host.Die(LivingNPC.DeathCauses.Shot);
        }
        
        public override void Interact(Door door)
        {
            if (!Raycasting.CheckObstacleBetween(transform.position, door.gameObject)) return;
            door.OpenDoor();
        }

        public override void Interact(LivingNPC livingNpc)
        {
            base.Interact(livingNpc);
            if (!isAlive) return;
            if (!Raycasting.CheckObstacleBetween(transform.position, livingNpc.gameObject)) return;
            LeaveHost(livingNpc);
            ChangeComponentsOn(livingNpc);
            DestroyPlayer();
        }

        protected override void LeaveHost()
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            InstantiateParasite(_host.ParasiteTargetPoint).AddComponent<Player>();
        }
        
        protected void LeaveHost(LivingNPC livingNpc)
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            Player parasite = InstantiateParasite(_host.ParasiteTargetPoint).AddComponent<Player>();
            parasite.GetComponent<PlayerController>().Interact(livingNpc);
        }
    }
}