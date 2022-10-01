using Interactables;
using Misc;
using Player.Controllers;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Start()
        {
            CheckPlayerController();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject selectedGameObject = ScreenRaycasting.GetSelectedGameObject();
                if (!selectedGameObject.TryGetComponent(out IInteractable interactable))
                {
                    return;
                }
                interactable.AcceptVisitor(_playerController);
            }
            if(Input.GetMouseButtonDown(1))
            {
                _playerController.MovePlayer();
            }
        }

        private void CheckPlayerController()
        {
            if (!TryGetComponent(out PlayerController controller))
            {
                _playerController = gameObject.AddComponent<ParasiteController>();
                _playerController.ApplyPlayerMovement();
                return;
            }
            _playerController = controller;
            _playerController.enabled = true;
            _playerController.ApplyPlayerMovement();
        }
    }
}