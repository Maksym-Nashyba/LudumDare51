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

        public Vector3 GoThrough(Vector3 playerPosition)
        {
            Vector3 localPlayerPosition = transform.InverseTransformPoint(playerPosition);
            if (localPlayerPosition.z < transform.localPosition.z)
            {
                return _frontSide.position;
            }
            return _backSide.position;
        }
    }
}