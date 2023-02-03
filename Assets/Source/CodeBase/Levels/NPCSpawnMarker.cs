using UnityEngine;

public class NPCSpawnMarker : SpawnMarker
{
    [field: SerializeField] public string ID;

    public override Color DrawColor => Color.blue;
}
