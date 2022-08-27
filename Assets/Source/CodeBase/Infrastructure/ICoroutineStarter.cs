using System.Collections;
using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public interface ICoroutineStarter
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}