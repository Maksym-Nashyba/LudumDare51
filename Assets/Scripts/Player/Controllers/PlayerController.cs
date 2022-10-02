using System.Collections;
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
            playerMovement.SetPlayerDestination();
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
            }
        }

        protected float CheckDistanceTo(Transform pointTransform)
        {
            return Mathf.Abs((transform.position - pointTransform.position).magnitude);
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
            HostLifeTimer timer = livingNPC.gameObject.GetComponentInChildren<HostLifeTimer>(true);
            timer.gameObject.SetActive(true);
            timer.BeginCountdown(livingNPC.GetComponent<PlayerController>().LeaveHost);
        }

        protected abstract IEnumerator PlayAnimation(LivingNPC livingNpc);

        protected void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}
