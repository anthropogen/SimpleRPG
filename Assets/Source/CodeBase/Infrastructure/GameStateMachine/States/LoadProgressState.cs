using SimpleRPG.Services.PersistentData;
using SimpleRPG.Services.SaveLoad;
using System;
using UnityEngine;

namespace SimpleRPG.Infrastructure.GameStateMachine
{
    public class LoadProgressState : IGameEnterState
    {
        private readonly GameStateMachine stateMachine;
        private readonly IPersistentProgressService progressService;
        private readonly ISaveLoadService saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            this.stateMachine = stateMachine;
            this.progressService = progressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadOrCreateNewProgress();
            stateMachine.Enter<LoadLevelState, string>(progressService.Progress.WorldData.PositionOnLevel.Level);
        }
        public void Exit()
        {
        }

        private void LoadOrCreateNewProgress()
            => progressService.Progress = saveLoadService.Load() ?? CreateNewProgress();

        private PersistentProgress CreateNewProgress()
        {
            var progress = new PersistentProgress("Village");
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();
            return progress;
        }
    }
}