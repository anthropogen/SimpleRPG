using UnityEngine;
using UnityEngine.UI;

namespace EpicRPG.UI
{
    public class HPBar : GameEntity
    {
        [SerializeField] private Image foreground;
        public void SetValue(float value)
            => foreground.fillAmount = value;
    }
}