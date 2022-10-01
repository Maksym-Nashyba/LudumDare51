using Player;
using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        public void AcceptVisitor(IVisitor visitor)
        {
            visitor.Interact(this);
        }
    }
}