using UnityEngine;

namespace SimpleRPG.Services
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string AttackButton = "Attack";
        protected const string ForceAttackButton = "ForceAttack";
        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp()
            => SimpleInput.GetButtonUp(AttackButton);

        public bool IsForceAttackButtonUp()
           => SimpleInput.GetButton(ForceAttackButton);

        protected Vector2 GetSimpleInputAxis()
           => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}