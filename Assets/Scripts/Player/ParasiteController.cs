using Interactables;
using NPCs;
using UnityEngine;

namespace Player
{
    public class ParasiteController : PlayerController, IVisitor
    {
        public override void ChangeHost(LivingNPC livingNpc)
        {
            throw new System.NotImplementedException();
        }
        
        void IVisitor.Interact(IInteractable interactable)
        {
            Debug.Log("ABOBA");
        }
    }
}