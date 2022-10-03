using Interactables;
using Misc;
using NPCs;
using NPCs.AI;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class GuardController: PlayerController
    {
        private LivingNPC _host;
        
        private void Awake()
        {
            _host = GetComponent<LivingNPC>();
        }
        
        public override void ApplyPlayerMovement() 
        {
            PlayerMovement = new HumanoidMovement(_host);
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
            ((GuardNPC)_host).ShootAt(livingNpc.transform);
        }
        
        protected override void LeaveHost()
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
    }
}