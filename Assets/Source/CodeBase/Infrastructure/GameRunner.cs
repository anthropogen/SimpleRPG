using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper bootstrapTemplate;
        private void Awake()
        {
            var bootstrapper = GameObject.FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
                Instantiate(bootstrapTemplate);
        }
    }
}