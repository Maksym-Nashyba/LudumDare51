using UnityEngine;

namespace Interactables
{
    public class Gate : MonoBehaviour
    {
        public bool IsOpened => _opened;
        private bool _opened;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void Open()
        {
            if(IsOpened) return;
            _opened = true;
            
            _animator.Play("GateOpen");
        }
    }
}