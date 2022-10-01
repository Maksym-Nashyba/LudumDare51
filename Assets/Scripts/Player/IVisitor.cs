using Interactables;

namespace Player
{
    public interface IVisitor
    {
        public void Interact(IInteractable interactable);
    }
}