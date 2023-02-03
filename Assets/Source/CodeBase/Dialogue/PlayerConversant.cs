using SimpleRPG.Infrastructure;
using SimpleRPG.UI;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRPG.Dialogue
{
    public class PlayerConversant : GameEntity
    {
        private Dialogue currentDialogue;
        private DialogueNode currentNode;
        private DialogueWindow dialogueWindow;
        public bool IsChoosing { get; private set; }
        public bool HasDialogue => currentDialogue != null;

        public void Construct(DialogueWindow dialogueWindow)
        {
            this.dialogueWindow = dialogueWindow;
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            dialogueWindow.Open();
            dialogueWindow.UpdateText();
        }

        public string GetText()
        {
            if (currentDialogue == null)
                return "";

            if (currentNode == null)
                return currentDialogue.GetRootNode().Text;

            return currentNode.Text;
        }

        public void Next()
        {
            if (currentDialogue.GetPlayerChildren(currentNode).Count() > 0)
            {
                IsChoosing = true;
                return;
            }
            currentNode = currentDialogue.GetAiChildren(currentNode).First();
        }

        public bool HasNext()
        {
            if (currentNode.ChildrenCount > 0)
                return true;
            return false;
        }

        public IEnumerable<DialogueNode> GetChoises()
            => currentDialogue.GetPlayerChildren(currentNode);

        public void SelectChoose(DialogueNode node)
        {
            currentNode = node;
            IsChoosing = false;
            Next();
        }
    }
}

