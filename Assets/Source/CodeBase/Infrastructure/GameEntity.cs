using EpicRPG.Infrastructure;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    private void OnEnable()
    {
        GameUpdater.Instance.Loop += Loop;
        GameUpdater.Instance.FixedLoop += FixedLoop;
        GameUpdater.Instance.LateLoop += LateLoop;
        Enable();
    }
    private void OnDisable()
    {
        GameUpdater.Instance.Loop -= Loop;
        GameUpdater.Instance.FixedLoop -= FixedLoop;
        GameUpdater.Instance.LateLoop -= LateLoop;
        Disable();
    }

    protected virtual void Enable() { }
    protected virtual void Loop() { }
    protected virtual void FixedLoop() { }
    protected virtual void LateLoop() { }
    protected virtual void Disable() { }


}
