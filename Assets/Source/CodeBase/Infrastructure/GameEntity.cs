using EpicRPG.Infrastructure;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameUpdater.Instance != null)
        {
            GameUpdater.Instance.Loop += Loop;
            GameUpdater.Instance.FixedLoop += FixedLoop;
            GameUpdater.Instance.LateLoop += LateLoop;
        }
        else
            throw new System.NullReferenceException("Doesn't have game updater");

        Enable();
    }

    private void OnDisable()
    {
        if (GameUpdater.Instance != null)
        {
            GameUpdater.Instance.Loop -= Loop;
            GameUpdater.Instance.FixedLoop -= FixedLoop;
            GameUpdater.Instance.LateLoop -= LateLoop;
        }

        Disable();
    }

    protected virtual void Enable() { }
    protected virtual void Loop() { }
    protected virtual void FixedLoop() { }
    protected virtual void LateLoop() { }
    protected virtual void Disable() { }
}
