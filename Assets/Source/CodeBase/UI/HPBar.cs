using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class HPBar : GameEntity
    {
        [SerializeField] private Image foreground;
        public void SetValue(float value)
            => foreground.fillAmount = value;
    }
}