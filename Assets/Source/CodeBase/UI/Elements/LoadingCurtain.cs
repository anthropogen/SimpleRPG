using System.Collections;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField, Range(0, 2)] private float hideDuration = 1;
        private void Awake()
            => DontDestroyOnLoad(this);
        public void Show()
        {
            gameObject.SetActive(true);
            group.alpha = 1;
        }
        public void Hide()
            => StartCoroutine(HideRoutine());

        private IEnumerator HideRoutine()
        {
            while (group.alpha > 0)
            {
                group.alpha -= Time.deltaTime / hideDuration;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}