using UnityEngine;

namespace Player.Movement
{
    public abstract class PlayerMovement
    {
        public abstract void SetPlayerDestination();
        public abstract void TurnOffNavMesh();

        public virtual void WarpPlayerToPoint(Vector3 point)
        {
            
        }
    }
}