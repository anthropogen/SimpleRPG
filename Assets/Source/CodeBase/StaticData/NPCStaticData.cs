using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "newNPCData", menuName = "Static Data/NPC")]
public class NPCStaticData : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
}
