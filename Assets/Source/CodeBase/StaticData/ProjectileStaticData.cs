using EpicRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EpicRPG.StaticData
{
    [CreateAssetMenu(fileName = "newProjectileData", menuName = "Static Data/Projectiles")]
    public class ProjectileStaticData : ScriptableObject
    {
        [SerializeField] private Projectile[] templates;
        private Dictionary<ProjectileType, Projectile> projectiles;
        public Dictionary<ProjectileType, Projectile> Projectiles
        {
            get
            {
                if (projectiles == null)
                    projectiles = templates.ToDictionary(t => t.ProjectileType, t => t);
                return projectiles;
            }
        }
        public Projectile GetProjectileTempate(ProjectileType type)
        {
            if (Projectiles.ContainsKey(type))
                return projectiles[type];
            throw new ArgumentException("Doesn't have this type of projectile");
        }
    }
}