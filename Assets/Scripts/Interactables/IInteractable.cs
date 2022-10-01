using Player;

namespace Interactables
{
    public interface IInteractable
    {
        public void AcceptVisitor(IVisitor visitor);
    }
}