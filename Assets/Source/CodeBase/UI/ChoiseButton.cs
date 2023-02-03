using SimpleRPG.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace SimpleRPG.UI
{
    public class ChoiseButton : GameEntity
    {
        [SerializeField] public Button button;
        [SerializeField] private TMP_Text text;

        public void SetText(string text)
            => this.text.text = text;

        public void ClearText()
            => text.text = "";

        public void AddListener(UnityAction action)
            => button.onClick.AddListener(action);

        public void RemoveListeners()
            => button.onClick.RemoveAllListeners();
    }
}