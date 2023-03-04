using SimpleRPG.Dialogue;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class DialogueWindow : BaseWindow
    {
        [SerializeField] private TMP_Text aiText;
        [SerializeField] private TMP_Text conversantName;
        [SerializeField] private Button nextButton;
        [SerializeField] private Transform choisesRoot;
        [SerializeField] private Transform aiResponse;
        [SerializeField] private ChoiseButton choiseButtonTemplate;
        private List<ChoiseButton> choiseButtons = new List<ChoiseButton>();
        private PlayerConversant playerConversant;

        public void Construct(PlayerConversant playerConversant)
        {
            this.playerConversant = playerConversant;
        }

        protected override void Enable()
        {
            nextButton.onClick.AddListener(Next);
        }

        protected override void Disable()
        {
            playerConversant.Quit();
            nextButton.onClick.RemoveAllListeners();
        }

        public void UpdateConversantName(string name)
            => conversantName.text = name;

        public void UpdateText()
        {
            if (!playerConversant.HasDialogue)
                return;
            choisesRoot.gameObject.SetActive(playerConversant.IsChoosing);
            aiResponse.gameObject.SetActive(!playerConversant.IsChoosing);
            if (playerConversant.IsChoosing)
            {
                ClearChoises();
                SetChoises();
            }
            else
            {
                aiText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void Next()
        {
            playerConversant.Next();
            UpdateText();
        }

        private void ClearChoises()
        {
            foreach (var item in choiseButtons)
            {
                item.gameObject.SetActive(false);
                item.RemoveListeners();
            }
        }

        private void SetChoises()
        {
            int i = 0;
            foreach (var choise in playerConversant.GetChoises())
            {
                ChoiseButton button = null;
                if (i < choiseButtons.Count)
                {
                    choiseButtons[i].gameObject.SetActive(true);
                    button = choiseButtons[i];
                }
                else
                {
                    button = Instantiate(choiseButtonTemplate, choisesRoot);
                    choiseButtons.Add(button);
                }
                button.SetText(choise.Text);
                button.AddListener(() =>
                {
                    playerConversant.SelectChoose(choise);
                    UpdateText();
                });
                i++;
            }
        }
    }
}