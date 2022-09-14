using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public abstract class EntityState : MonoBehaviour, IEntityState
    {
        [field: SerializeField] public List<Transition<EntityState>> Transitions { get; private set; } = new List<Transition<EntityState>>();

        public abstract void Init(Transform transform);
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Run();
    }
}