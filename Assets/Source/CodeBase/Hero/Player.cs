using SimpleRPG.Characters;
using SimpleRPG.Infrastructure;
using SimpleRPG.Services.PersistentData;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRPG.Hero
{
    public class Player : GameEntity, ISavable
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerStateMachine stateMachine;
        protected override void Enable()
        {
            stateMachine.Construct(transform);
        }

        public void LoadProgress(PersistentProgress progress)
        {
            if (progress.WorldData.PositionOnLevel != null && progress.WorldData.PositionOnLevel.Level == PersistentProgressService.GetActiveSceneName())
                if (progress.WorldData.PositionOnLevel.PlayerPosition != null)
                    Warp(progress.WorldData.PositionOnLevel.PlayerPosition);
        }

        public void SaveProgress(PersistentProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(
                PersistentProgressService.GetActiveSceneName(), transform.position.ToVector3Data());
        }

        public void Warp(Vector3Data position)
        {
            characterController.enabled = false;
            transform.position = position.ToVector3();
            characterController.enabled = true;
        }
    }
}