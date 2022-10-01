using EpicRPG.Items;
using EpicRPG.Services;
using EpicRPG.Services.GameFactory;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class WeaponPositioner : GameEntity
    {
        [SerializeField] private Transform meleeWeapon;
        private IGameFactory gameFactory;

        private void Start()
        {
            gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public WeaponModel GetWeaponTransform(MeleeWeapon weapon)
        {
            GameObject item = gameFactory.CreateWeapon(weapon);
            item.transform.parent = meleeWeapon.transform;
            item.transform.localPosition = Vector3.zero;
            return item.GetComponent<WeaponModel>();
        }

    }
}