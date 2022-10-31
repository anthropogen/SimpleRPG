using SimpleRPG.Hero;
using SimpleRPG.Infrastructure;
using SimpleRPG.Infrastructure.GameStateMachine;
using UnityEngine;

public class LevelTransfer : GameEntity
{
    [field: SerializeField] public string TransferTo { get; private set; }
    private IGameStateMachine gameStateMachine;
    private bool isTransfered;

    public void Construct(IGameStateMachine gameStateMachine, string nextLevel)
    {
        this.gameStateMachine = gameStateMachine;
        TransferTo = nextLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == false) return;
        if (isTransfered == true) return;
        gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
    }
}

