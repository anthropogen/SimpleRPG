using EpicRPG.Infrastructure;
using EpicRPG.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerMover : GameEntity
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField, Range(0, 100)] private float movementSpeed;
        private Vector3 movementVector;
        private IInputService inputService;
        private Camera cam;

        private void Start()
        {
            inputService = ServiceLocator.Container.Single<IInputService>();
            cam = Camera.main;
        }
        protected override void Loop()
        {
            movementVector = Vector3.zero;
            if (inputService.Axis.sqrMagnitude > 0.001f)
            {
                var inputVector = new Vector3(inputService.Axis.x, 0, inputService.Axis.y);
                movementVector = cam.transform.parent.TransformDirection(inputVector);
                movementVector.y = 0;
                movementVector = Vector3.ClampMagnitude(movementVector, 1);
                transform.forward = movementVector;
                animator.Move(movementVector.sqrMagnitude);
            }
            else
                animator.StopMove();

            movementVector += Physics.gravity;
            characterController.Move(movementVector * Time.deltaTime * movementSpeed);
        }
    }
}