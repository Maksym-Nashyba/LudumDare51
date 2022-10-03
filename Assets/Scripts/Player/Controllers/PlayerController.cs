using Interactables;
using Misc;
using NPCs;
using Player.Movement;
using UnityEngine;

namespace Player.Controllers
{
    public abstract class PlayerController : MonoBehaviour, IVisitor
    {
        protected Transform Transform;
        protected PlayerMovement PlayerMovement;
        protected bool IsAlive = true;

        protected virtual void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        public abstract void ApplyPlayerMovement();
        
        protected abstract void LeaveHost();

        public virtual void GetShot()
        {
        }
        
        public virtual void MovePlayer()
        {
            PlayerMovement.SetPlayerDestination();
        }

        public virtual void Interact(Door door)
        {
            
        }
        
        public virtual void Interact(RatDoor ratDoor)
        {
            
        }

        public virtual void Interact(LivingNPC livingNpc)
        {
            if (livingNpc.gameObject == gameObject)
            {
                LeaveHost();
                IsAlive = false;
            }
        }

        public virtual void Interact(GateOpeningLever gateOpeningLever)
        {
            
        }

        protected float CheckDistanceTo(Transform pointTransform)
        {
            return Mathf.Abs((transform.position - pointTransform.position).magnitude);
        }
        
        protected GameObject InstantiateParasite(Transform hostTransform)
        {
            Player player = FindObjectOfType<Player>();
            if (player is not null)
            {
                Destroy(player.gameObject);
            }
            return Instantiate(ServiceLocator.ParasitePrefab, hostTransform.position, hostTransform.rotation);
        }
        
        protected void ChangeComponentsOn(LivingNPC livingNPC)
        {
            PlayerMovement = new HumanoidMovement(livingNPC);
            livingNPC.GetInfected();
            livingNPC.gameObject.GetComponent<NPC>().enabled = false;
            livingNPC.gameObject.AddComponent<Player>();
            HostLifeTimer timer = livingNPC.gameObject.GetComponentInChildren<HostLifeTimer>(true);
            timer.gameObject.SetActive(true);
            timer.BeginCountdown(() => livingNPC.GetComponent<PlayerController>().LeaveHost());
        }

        protected void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}
