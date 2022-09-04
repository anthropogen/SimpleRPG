using EpicRPG.Services;
using EpicRPG.Services.SaveLoad;
using UnityEngine;

public class SaveTrigger : GameEntity
{
    private ISaveLoadService saveService;

    private void Awake()
    {
        saveService = ServiceLocator.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        saveService.Save();
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}

