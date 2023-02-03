using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimpleRPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private string text;
        [SerializeField] private List<string> children = new List<string>();
        [SerializeField] private Rect rect = new Rect(0, 0, 200, 150);
        [field: SerializeField] public bool IsPlayerSpeaking { get; set; }
        public string Text => text;
        public Rect Rect => rect;
        public IEnumerable<string> GetAllChildren => children;
        public int ChildrenCount => children.Count;

        #region EDITOR
#if UNITY_EDITOR

        public void SetText(string text)
        {
            Undo.RecordObject(this, "update dialogue text");
            this.text = text;
        }

        public void SetPosition(Vector2 value)
        {
            Undo.RecordObject(this, "move node");
            rect.position = value;
            EditorUtility.SetDirty(this);
        }

        public void RemoveChildren(string name)
        {
            Undo.RecordObject(this, "remove children");
            children.Remove(name);
            EditorUtility.SetDirty(this);
        }

        public void AddChildren(string name)
        {
            Undo.RecordObject(this, "link node");
            children.Add(name);
        }
#endif
        #endregion
    }
}