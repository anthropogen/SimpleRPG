using System.Collections;
using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public interface ICoroutineStarter
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}