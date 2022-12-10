using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public abstract class BaseWindow : GameEntity
    {
        [SerializeField] private Button closeButton;
        [field: SerializeField] public WindowsID ID { get; private set; }

        private void Awake()
        {
            closeButton.onClick.AddListener(() => Close());
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void Close()
            => gameObject.SetActive(false);

        public void Open()
            => gameObject.SetActive(true);
    }
}