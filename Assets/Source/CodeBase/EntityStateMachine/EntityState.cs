using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public abstract class EntityState : MonoBehaviour, IEntityState
    {
        [field: SerializeField] public Transition[] Transitions { get; private set; }

        public abstract void Init(Transform transform);
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Run();
    }
}