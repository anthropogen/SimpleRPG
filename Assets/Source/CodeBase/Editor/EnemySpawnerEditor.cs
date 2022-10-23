using EpicRPG.Levels;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawnMarker))]
public class EnemySpawnerMarkerEditor : Editor
{
    private const float Radius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
    public static void RendererCustomEditor(EnemySpawnMarker spawner, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.transform.position, Radius);
    }
}

