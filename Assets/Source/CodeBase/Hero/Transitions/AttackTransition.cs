using EpicRPG.Services;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class AttackTransition : PlayerTransition
    {
        private IInputService inputService;

        private void Start()
        {
            inputService = ServiceLocator.Container.Single<IInputService>();
        }

        public override bool NeedTransit()
        {
            return inputService.IsAttackButtonUp();
        }
    }
}