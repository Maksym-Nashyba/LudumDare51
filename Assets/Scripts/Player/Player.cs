﻿using Interactables;
using Misc;
using Player.Controllers;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerController _playerController;

        private void OnEnable()
        {
            CheckPlayerController();
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                GameObject selectedGameObject = ScreenRaycasting.GetSelectedGameObject();
                if (!selectedGameObject.TryGetComponent(out IInteractable interactable))
                {
                    return;
                }
                interactable.AcceptVisitor(_playerController);
            }
            else if(Input.GetMouseButtonUp(1))
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