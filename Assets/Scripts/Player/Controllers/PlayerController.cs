using Interactables;
using Misc;
using NPCs;
using Player.Movement;
using UnityEngine;

namespace Player.Controllers
{
    public abstract class PlayerController : MonoBehaviour, IVisitor
    {
        protected PlayerMovement playerMovement;

        public abstract void ApplyPlayerMovement();
        
        protected abstract void LeaveHost();
        
        public void MovePlayer()
        {
            playerMovement.MovePlayerToPoint();
        }

        public virtual void Interact(Door door)
        {
            door.gameObject.SetActive(false);
        }

        public virtual void Interact(LivingNPC livingNpc)
        {
            if (livingNpc.gameObject == gameObject)
            {
                LeaveHost();
            }
        }
        
        protected GameObject InstantiateParasite(Transform hostTransform)
        {
            return Instantiate(ServiceLocator.ParasitePrefab, hostTransform.position, hostTransform.rotation);
        }
        
        protected void ChangeComponentsOn(LivingNPC livingNPC)
        {
            playerMovement = new HumanoidMovement(livingNPC);
            livingNPC.GetInfected();
            livingNPC.gameObject.GetComponent<NPC>().enabled = false;
            livingNPC.gameObject.AddComponent<Player>();
        }

        protected void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}
