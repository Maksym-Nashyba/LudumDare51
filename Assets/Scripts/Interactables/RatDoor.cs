using Player;
using UnityEngine;

namespace Interactables
{
    public class RatDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _frontSide;
        [SerializeField] private Transform _backSide;

        public void AcceptVisitor(IVisitor visitor)
        {
            visitor.Interact(this);
        }

        public Vector3 FindExitPosition(Vector3 entryPosition)
        {
            return transform.position + (transform.position - entryPosition);
        }
    }
}