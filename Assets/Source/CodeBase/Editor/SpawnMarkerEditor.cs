using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnMarker), true)]
public class SpawnMarkerEditor : Editor
{
    private const float Radius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
    public static void RendererCustomEditor(SpawnMarker spawner, GizmoType gizmo)
    {
        Gizmos.color = spawner.DrawColor;
        Gizmos.DrawSphere(spawner.transform.position, Radius);
        Gizmos.color = Color.white;
    }
}
