using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class GateOpeningLever : MonoBehaviour, IInteractable
    {
        public UnityEvent Pulled;
        public bool AlreadyPulled => _pulled;
        private bool _pulled;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void AcceptVisitor(IVisitor visitor)
        {
            visitor.Interact(this);
        }

        public void Pull()
        {
            if(_pulled)return;
            _pulled = true;
            Pulled?.Invoke();
            PlayPullAnimation();
        }

        private void PlayPullAnimation()
        {
            _animator.Play("GateLeverPull");
        }
    }
}