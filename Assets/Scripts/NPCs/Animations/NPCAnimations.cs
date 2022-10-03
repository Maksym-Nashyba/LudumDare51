using System;
using UnityEngine;

namespace NPCs
{
    public abstract class NPCAnimations : MonoBehaviour
    {
        [SerializeField] protected Animator GFXAnimator;
        [SerializeField] protected GameObject GFXObject;
        protected Transform Transform;
        private Vector3 _previousPosition;

        private void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        private void Start()
        {
            _previousPosition = Transform.position;
        }

        public abstract void PlayStartInfection();

        public abstract void PlayDeath(LivingNPC.DeathCauses cause);

        public abstract void PlayInteractAnimation(Vector3 target);

        private void FixedUpdate()
        {
            GFXAnimator.SetFloat("Speed", (_previousPosition-Transform.position).magnitude);
            _previousPosition = Transform.position;
        }
    }
}