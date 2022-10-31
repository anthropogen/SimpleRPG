using SimpleRPG.Infrastructure;
using UnityEngine;

public class LevelTransferMarker : GameEntity
{
    [field: SerializeField] public string TransferTo { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}

