using UnityEngine;

namespace SimpleRPG
{
    public static class MyUnityExtensions
    {
        public static void ResetTransform(this Transform item)
        {
            item.localPosition = Vector3.zero;
            item.localScale = Vector3.one;
            item.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}