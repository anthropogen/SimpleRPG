using SimpleRPG.Levels;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(UniqueID))]
public class UniqueIDEditor : Editor
{
    private readonly BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

    private void OnEnable()
    {
        var uniqueID = (UniqueID)target;
        if (uniqueID.gameObject.scene.rootCount == 0) return;

        if (string.IsNullOrEmpty(uniqueID.SaveID) && !Application.isPlaying)
            Generate(uniqueID);
        else
        {
            var uniqueIDs = FindObjectsOfType<UniqueID>();
            while (uniqueIDs.Any(i => i.SaveID == uniqueID.SaveID && i != uniqueID))
            {
                Generate(uniqueID);
            }
        }
    }

    private void Generate(UniqueID uniqueID)
    {
        var id = $"{uniqueID.gameObject.scene.name} {Guid.NewGuid().ToString()}";
        uniqueID.GetType()
            .GetProperty($"{nameof(uniqueID.SaveID)}", flags)
            .SetValue(uniqueID, id);

        EditorUtility.SetDirty(uniqueID);
        EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene);
    }
}

