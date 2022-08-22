using EpicRPG.Services;
using System;
using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public Game()
        {
            InputService = CreateInputService();
        }

        private IInputService CreateInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}