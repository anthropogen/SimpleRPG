using EpicRPG.Infrastructure;
using EpicRPG.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField, Range(0, 100)] private float movementSpeed;
        private Vector3 movementVector;
        private IInputService inputService;
        private Camera cam;

        private void Start()
        {
            inputService = Game.InputService;
            cam = Camera.main;
        }
        private void Update()
        {
            movementVector = Vector3.zero;
            if (inputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = cam.transform.TransformDirection(new Vector3(inputService.Axis.x, 0, inputService.Axis.y));
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }
            movementVector += Physics.gravity;
            characterController.Move(movementVector * Time.deltaTime * movementSpeed);
        }
    }
}