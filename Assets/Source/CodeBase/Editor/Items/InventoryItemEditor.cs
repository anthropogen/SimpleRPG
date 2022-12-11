using SimpleRPG.Items;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryItem))]
public class InventoryItemEditor : Editor
{
    private InventoryItem item;

    private void OnEnable()
    {
        item = target as InventoryItem;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (item.Icon == null)
            return;

        var texture = AssetPreview.GetAssetPreview(item.Icon);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
