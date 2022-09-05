using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public abstract class Transition : MonoBehaviour
    {
        [field: SerializeField] public EntityState NextState { get; private set; }
        public abstract bool NeedTransit();
    }
}