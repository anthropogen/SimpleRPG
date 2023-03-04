using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.UI;
using UnityEngine;

public class InventoryDropper : GameEntity, IDragDestination<InventoryItem>
{
    [SerializeField] private Vector3 dropOffset;
    private IGameFactory gameFactory;

    public void Construct(IGameFactory gameFactory)
         => this.gameFactory = gameFactory;

    public async void AddItem(InventoryItem item, int count)
    {
        var loot = await gameFactory.CreateLoot(item, count);
        loot.transform.position = gameFactory.Player.transform.position + dropOffset;
    }

    public int MaxAcceptable(InventoryItem item)
    {
        return int.MaxValue;
    }
}

