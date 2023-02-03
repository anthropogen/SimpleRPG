using SimpleRPG.Dialogue;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Linq;
public class DialogueEditor : EditorWindow
{
    private const int CanvasSize = 4000;
    private const float BackgroundSize = 50f;
    private static Dialogue selected;
    private static GUIStyle nodeStyle;
    private static GUIStyle playerNodeStyle;
    private static DialogueNode draggingNode;
    private static Vector2 draggingOffset;
    private static DialogueNode creatingNode;
    private static DialogueNode delitingNode;
    private static DialogueNode linkingNode;
    private static Vector2 scrollPosition;
    private static Vector2 draggingCanvasOffset;
    private static bool isDraggingCanvas;

    private void Awake()
    {
        Selection.selectionChanged += OnSelectionChanged;
        CreateNodeStyle();
    }

    private void OnDestroy()
    {
        Selection.selectionChanged -= OnSelectionChanged;
    }

    [MenuItem("Window/Dialogue Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }

    [OnOpenAsset(1)]
    public static bool OpenDialogue(int instanceID, int line)
    {
        var dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if (dialogue != null)
        {
            selected = dialogue;
            ShowEditorWindow();
            return true;
        }
        return false;
    }

    private void OnGUI()
    {
        if (selected == null)
        {
            EditorGUILayout.LabelField("Non selected dialogue");
            return;
        }

        EditorGUILayout.LabelField(selected.name);
        ProcessEvents();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        Rect canvas = GUILayoutUtility.GetRect(CanvasSize, CanvasSize);
        var backgroundTex = Resources.Load("grid_background") as Texture2D;
        Rect textCoords = new Rect(0, 0, CanvasSize / BackgroundSize, CanvasSize / BackgroundSize);
        GUI.DrawTextureWithTexCoords(canvas, backgroundTex, textCoords);
        EditorGUILayout.BeginHorizontal();

        foreach (var node in selected.Nodes)
        {
            DrawNode(node);
            DrawConnectionNode(node);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();

        if (creatingNode != null)
        {
            selected.CreateNode(creatingNode);
            creatingNode = null;
        }
        if (delitingNode != null)
        {
            selected.DeleteNode(delitingNode);
            delitingNode = null;
        }
    }

    private void ProcessEvents()
    {
        var eventType = Event.current.type;
        if (eventType == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
            if (draggingNode != null)
            {
                draggingOffset = draggingNode.Rect.position - Event.current.mousePosition;
                Selection.activeObject = draggingNode;
            }
            else
            {
                isDraggingCanvas = true;
                draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                Selection.activeObject = selected;
            }
        }
        else if (eventType == EventType.MouseDrag && draggingNode != null)
        {
            var rootNode = draggingNode;
            rootNode.SetPosition(Event.current.mousePosition + draggingOffset);
            GUI.changed = true;
        }
        else if (eventType == EventType.MouseDrag && isDraggingCanvas)
        {
            scrollPosition = draggingCanvasOffset - Event.current.mousePosition;
            GUI.changed = true;
        }
        else if (eventType == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }
        else if (eventType == EventType.MouseUp && isDraggingCanvas == true)
        {
            isDraggingCanvas = false;
        }
    }

    private DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode result = null;
        foreach (var node in selected.Nodes)
        {
            if (node.Rect.Contains(point))
                result = node;
        }
        return result;
    }

    private static void DrawNode(DialogueNode node)
    {
        var style = node.IsPlayerSpeaking ? playerNodeStyle : nodeStyle;
        GUILayout.BeginArea(node.Rect, style);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Node:", EditorStyles.whiteBoldLabel);
        var newText = EditorGUILayout.TextField(node.Text);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selected, "update text");
            node.SetText(newText);
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            creatingNode = node;
        }
        DrawLinkingNodeButton(node);
        if (GUILayout.Button("x"))
        {
            delitingNode = node;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private static void DrawLinkingNodeButton(DialogueNode node)
    {
        if (linkingNode == null)
        {
            if (GUILayout.Button("Link"))
            {
                linkingNode = node;
            }
        }
        else if (linkingNode == node)
        {
            if (GUILayout.Button("Cancel"))
            {
                linkingNode = null;
            }
        }
        else if (linkingNode.GetAllChildren.Contains(node.name))
        {
            if (GUILayout.Button("Unchild"))
            {
                Undo.RecordObject(selected, "unchild node");
                linkingNode.RemoveChildren(node.name);
                linkingNode = null;
            }
        }

        else
        {
            if (GUILayout.Button("child"))
            {
                linkingNode.AddChildren(node.name);
                linkingNode = null;
            }
        }
    }

    private void DrawConnectionNode(DialogueNode parent)
    {
        foreach (var child in selected.GetAllChildren(parent))
        {
            Vector3 startPos = new Vector3(parent.Rect.xMax, parent.Rect.center.y);
            Vector3 endPos = new Vector3(child.Rect.xMin, child.Rect.center.y);
            Vector3 controlPointOffset = endPos - startPos;
            controlPointOffset.y = 0;
            controlPointOffset.x *= 0.8f;
            Handles.DrawBezier(startPos, endPos, startPos + controlPointOffset, endPos - controlPointOffset, Color.gray, null, 4f);
        }
    }

    private void OnSelectionChanged()
    {
        var dialogue = Selection.activeObject as Dialogue;
        if (dialogue != null)
        {
            selected = dialogue;
            Repaint();
        }
    }
    private static void CreateNodeStyle()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        nodeStyle.normal.textColor = Color.white;
        nodeStyle.padding = new RectOffset(20, 20, 20, 20);
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        playerNodeStyle = new GUIStyle();
        playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        playerNodeStyle.normal.textColor = Color.white;
        playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
        playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
    }
}
