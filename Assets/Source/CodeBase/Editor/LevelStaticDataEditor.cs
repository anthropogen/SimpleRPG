using SimpleRPG.Hero;
using SimpleRPG.Levels;
using SimpleRPG.StaticData;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Collect"))
        {
            var staticData = target as LevelStaticData;
            staticData.Name = SceneManager.GetActiveScene().name;
           
            staticData.LevelTransfers = FindObjectsOfType<LevelTransferMarker>().
                Select(t =>
                new LevelTransferData(t.transform.position,
                t.TransferTo)).ToList();

            staticData.EnemySpawners =
                FindObjectsOfType<EnemySpawnMarker>().
                Select(m =>
                new EnemySpawnerData(m.EnemyTypeID,
                m.GetComponent<UniqueID>().SaveID,
                m.transform.position
                )).ToList();
            staticData.InitialPlayerPoint = FindObjectOfType<PlayerInitPoint>().Point;
            EditorUtility.SetDirty(target);
        }
    }
}

