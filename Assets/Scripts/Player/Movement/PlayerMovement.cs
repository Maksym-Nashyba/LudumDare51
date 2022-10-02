using UnityEngine;

namespace Player.Movement
{
    public abstract class PlayerMovement
    {
        public abstract void SetPlayerDestination();

        public virtual void WarpPlayerToPoint(Vector3 point)
        {
            
        }
    }
}