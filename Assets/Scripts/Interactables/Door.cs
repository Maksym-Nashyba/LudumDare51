using Player;
using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] public Transform FrontPosition;         
        [SerializeField] public Transform BackPosition;
        [SerializeField] private Animator _animator;
        
        public void AcceptVisitor(IVisitor visitor)
        {
            visitor.Interact(this);
        }

        public void OpenDoor()
        {
            _animator.Play("OpenDoor");
        }

        public void CloseDoor()
        {
            _animator.Play("CloseDoor");
        }
    }
}