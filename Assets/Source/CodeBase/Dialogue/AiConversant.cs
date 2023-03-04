using SimpleRPG.Characters;
using SimpleRPG.Dialogue;
using SimpleRPG.Hero;
using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class AiConversant : GameEntity
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private PlayerDetector playerDetector;
    [SerializeField] private Button dialogueButton;
    [SerializeField] private new string name;

    private void OnEnable()
    {
        playerDetector.InteractPlayer += OnInteractPlayer;
    }

    private void OnDisable()
    {
        playerDetector.InteractPlayer -= OnInteractPlayer;
    }

    private void OnInteractPlayer(Player player, bool isInteract)
    {
        dialogueButton.gameObject.SetActive(isInteract);
        if (isInteract)
            dialogueButton.onClick.AddListener(() => StartDialogueWith(player));
        else
            dialogueButton.onClick.RemoveAllListeners();
    }

    private void StartDialogueWith(Player player)
    {
        dialogueButton.gameObject.SetActive(false);
        player.GetComponentInChildren<PlayerConversant>().StartDialogue(this, dialogue, name);
    }
}

