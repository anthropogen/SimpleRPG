using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SimpleRPG.Dialogue
{
    [CreateAssetMenu(fileName = "newDialogue", menuName = "Dialogue/Create Dialogue", order = 51)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [field: SerializeField] public List<DialogueNode> Nodes { get; private set; } = new List<DialogueNode>();
        private Dictionary<string, DialogueNode> nodesLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodesLookup.Clear();
            nodesLookup = Nodes.ToDictionary(n => n.name);
        }

        public DialogueNode GetRootNode()
        {
            if (Nodes != null && Nodes.Count > 0)
                return Nodes[0];
            return null;
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (var id in parentNode.GetAllChildren)
            {
                if (nodesLookup.ContainsKey(id))
                    yield return nodesLookup[id];
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode parentNode)
            => GetAllChildren(parentNode).Where(n => n.IsPlayerSpeaking);

        public IEnumerable<DialogueNode> GetAiChildren(DialogueNode parentNode)
           => GetAllChildren(parentNode).Where(n => !n.IsPlayerSpeaking);

#if UNITY_EDITOR
        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "delete node");
            Nodes.Remove(nodeToDelete);
            OnValidate();
            foreach (var node in Nodes)
                node.RemoveChildren(nodeToDelete.name);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        public DialogueNode CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "created node");
            Undo.RecordObject(this, "add node");
            AddNode(newNode);
            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            Nodes.Add(newNode);
            OnValidate();
        }

        private static DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChildren(newNode.name);
                newNode.IsPlayerSpeaking = !parent.IsPlayerSpeaking;
                newNode.SetPosition(parent.Rect.position + new Vector2(parent.Rect.size.x, 0));
                EditorUtility.SetDirty(newNode);
            }
            return newNode;
        }
#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (Nodes.Count == 0)
            {
                AddNode(MakeNode(null));
            }
            if (AssetDatabase.GetAssetPath(this) == "")
                return;
            foreach (var node in Nodes)
            {
                if (AssetDatabase.GetAssetPath(node) == "")
                    AssetDatabase.AddObjectToAsset(node, this);
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}