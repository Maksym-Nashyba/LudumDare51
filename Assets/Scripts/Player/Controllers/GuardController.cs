using Interactables;
using NPCs;
using NPCs.AI;
using Player.Movement;

namespace Player.Controllers
{
    public sealed class GuardController: PlayerController
    {
        private LivingNPC _host;
        
        protected override void Awake()
        {
            base.Awake();
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
            StartCoroutine(nameof(WalkToDoor), door);
        }

        public override void Interact(LivingNPC livingNpc)
        {
            ((GuardNPC)_host).ShootAt(livingNpc.transform);
            if(livingNpc == _host)LeaveHost();
        }

        public override void Interact(GateOpeningLever gateOpeningLever)
        {
            if(gateOpeningLever.AlreadyPulled)return;
            gateOpeningLever.Pull();
            _host.Animations.PlayInteractAnimation(gateOpeningLever.transform.position);
        }

        protected override void LeaveHost()
        {
            _host.Die(LivingNPC.DeathCauses.ParasiteLeaving);
            InstantiateParasite(_host.transform).AddComponent<Player>();
        }
    }
}