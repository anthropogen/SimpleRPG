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

        private void Awake()
        {
            gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public WeaponModel SetWeapon(WeaponItem weaponItem)
        {
            if (weaponItem is MeleeWeapon)
                return SetWeapon(weaponItem as MeleeWeapon);

            else throw new System.InvalidOperationException("I don't know what kind of weapon this is");
        }

        public WeaponModel SetWeapon(MeleeWeapon weapon)
        {
            GameObject item = gameFactory.CreateWeapon(weapon);
            item.transform.parent = meleeWeapon.transform;
            item.transform.localPosition = Vector3.zero;
            return item.GetComponent<WeaponModel>();
        }

    }
}