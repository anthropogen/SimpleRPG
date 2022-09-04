using UnityEngine;

namespace EpicRPG.Infrastructure
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