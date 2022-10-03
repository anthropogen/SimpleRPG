using EpicRPG.Items;
using EpicRPG.Services;
using EpicRPG.Services.GameFactory;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class WeaponPositioner : GameEntity
    {
        [SerializeField] private Transform meleeWeapon;
        [SerializeField] private Transform bow;
        private IGameFactory gameFactory;

        private void Awake()
        {
            gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public WeaponModel SetWeapon(WeaponItem weaponItem)
        {
            if (weaponItem is MeleeWeapon)
                return SetWeapon(weaponItem as MeleeWeapon);
            if (weaponItem is BowItem)
                return SetWeapon(weaponItem as BowItem);

            else throw new System.InvalidOperationException("I don't know what kind of weapon this is");
        }

        public WeaponModel SetWeapon(MeleeWeapon weapon)
        {
            GameObject item = gameFactory.CreateWeapon(weapon);
            item.transform.parent = meleeWeapon.transform;
            item.transform.ResetTransform();
            return item.GetComponent<WeaponModel>();
        }

        public WeaponModel SetWeapon(BowItem weapon)
        {
            GameObject item = gameFactory.CreateWeapon(weapon);
            item.transform.parent = bow.transform;
            item.transform.ResetTransform();
            return item.GetComponent<WeaponModel>();
        }
    }
}
