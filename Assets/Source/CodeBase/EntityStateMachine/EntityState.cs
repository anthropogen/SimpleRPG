using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.EntityFSM
{
    public abstract class EntityState : MonoBehaviour, IEntityState
    {
        [field: SerializeField] public List<Transition> Transitions { get; private set; } = new List<Transition>();

        public abstract void Init(Transform transform);
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Run();
    }
}