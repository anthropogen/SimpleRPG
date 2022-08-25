using System;
using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public class GameUpdater : MonoBehaviour
    {
        public static GameUpdater Instance { get; private set; }

        public event Action Loop;
        public event Action LateLoop;
        public event Action FixedLoop;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
            => Loop?.Invoke();
        private void FixedUpdate()
            => FixedLoop?.Invoke();
        private void LateUpdate()
            => LateLoop?.Invoke();

        private void OnDestroy()
            => Instance = null;
    }
}