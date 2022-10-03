using System.Collections;
using Interactables;
using Misc;
using UnityEngine;

namespace NPCs.AI
{
    public class HumanoidNPC : LivingNPC
    {
        private RaycastHit[] _hits = new RaycastHit[4];
        [SerializeField] private GameObject _panicMark;

        protected override void Awake()
        {
            base.Awake();
            Detector.Detected += Panic;
        }

        protected void Update()
        {
            TryDetectDoors();
        }

        private void Panic(SuspiciousObject suspiciousObject)
        {
            StartCoroutine(nameof(PanicCoroutine));
        }

        private IEnumerator PanicCoroutine()
        {
            _panicMark.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _panicMark.SetActive(false);
        }

        private void OnCollidedWithDoor(Door door)
        {
            if (CurrentState is WalkThroughDoorState) return;
            
            ChangeState(new WalkThroughDoorState(CurrentState, door));
        }

        private void InteractWithDoor(Door door, bool shouldOpen)
        {
            if(shouldOpen)door.OpenDoor();
            else door.CloseDoor();
            Animations.PlayInteractAnimation(door.transform.position);
        }

        private void TryDetectDoors()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 flatDirection = Vector2.up.RotatedBy(90f * i);
                Vector3 direction = new Vector3(flatDirection.x, 0f, flatDirection.y);
                Vector3 origin = Transform.position;
                Ray ray = new Ray(origin, direction);

                if (Physics.Raycast(ray, out _hits[i], 0.25f, LayerMask.GetMask("Door")))
                {
                    if (!_hits[i].collider.TryGetComponent(out Door door)) continue;
                    OnCollidedWithDoor(door);
                }
            }
        }
        
        private void OnDestroy()
        {
            Detector.Detected -= Panic;
        }
        
        protected class WalkThroughDoorState : State
        {
            private readonly State _continueState;
            private readonly Door _door;
            private HumanoidNPC _npc;
            private Vector3 _enterPosition;
            private Vector3 _exitPosition;

            public WalkThroughDoorState(State continueState, Door door)
            {
                _continueState = continueState;
                _door = door;
            }

            public override void Start(Context context)
            {
                _npc = (HumanoidNPC)context.NPC;
                CalculateDoorSides();
                if (IsRelaxed())
                {
                    _npc.WalkToPosition(_exitPosition);
                }
                else
                {
                    _npc.RunToPosition(_exitPosition);
                }
            }

            public override IEnumerator Act()
            {
                _npc.InteractWithDoor(_door, true);
                yield return new WaitForSeconds(1f);
                
                bool walkedThrough = false;
                while (!walkedThrough)
                {
                    walkedThrough = (_npc.Transform.position - _exitPosition).sqrMagnitude < 0.1f;
                    yield return null;
                }

                if (_continueState.IsRelaxed())
                {
                    _npc.InteractWithDoor(_door, false);
                    yield return new WaitForSeconds(1f);
                }
                
                _npc.ChangeState(_continueState);
            }

            public override bool IsRelaxed()
            {
                return _continueState.IsRelaxed();
            }

            private void CalculateDoorSides()
            {
                Vector3 npcPosition = _npc.Transform.position;
                if ((_door.FrontPosition.position - npcPosition).sqrMagnitude <
                    (_door.BackPosition.position - npcPosition).sqrMagnitude)
                {
                    _exitPosition = _door.BackPosition.position;
                    _enterPosition = _door.FrontPosition.position;
                }
                else
                {
                    _exitPosition = _door.FrontPosition.position;
                    _enterPosition = _door.BackPosition.position;
                }
            }
        }
    }
}