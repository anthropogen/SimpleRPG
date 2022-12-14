using SimpleRPG.Infrastructure;
using TMPro;
using UnityEngine;
public class ItemTootlip : GameEntity
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text body;

    public void UpdateTootlip(string title, string body)
    {
        this.title.text = title;
        this.body.text = body;
    }
}

