using Interactables;
using NPCs;

namespace Player
{
    public interface IVisitor
    {
        public void Interact(Door door);

        public void Interact(RatDoor ratDoor);

        public void Interact(LivingNPC livingNpc);
    }
}