using MelonLoader;


using Assets.Scripts.Unity.UI_New.InGame;

using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using Assets.Scripts.Utils;
using System;
using System.Text.RegularExpressions;
using System.IO;
using Assets.Main.Scenes;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Bloons.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Simulation.Track;
using static Assets.Scripts.Models.Towers.TargetType;
using Assets.Scripts.Simulation;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using UnhollowerBaseLib;
using Assets.Scripts.Models.Towers.Upgrades;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using NinjaKiwi.Common;
using Assets.Scripts.Models.Towers.Filters;
using BTD_Mod_Helper.Api.Display;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Unity.Towers.Projectiles.Behaviors;

namespace LinkingDruidParagon.Paragon
{
    public class TheParagon
    {
        public class Druidparagon : ModVanillaParagon
        {
            public override string BaseTower => "Druid-005";
        }
        public class SuperSpiritOfWrathTower : ModParagonUpgrade<Druidparagon>
        {
            public override string Description => "A True Force Of Nature.";
            public override string Icon => "SuperSpiritOfTheForest_Icon";
            public override string Portrait => "SuperSpiritOfWrath_Portrait";
            public override string DisplayName => "Super Spirit Of Wrath";
            public override int Cost => 4000000;
            

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var boomerangParagon = Game.instance.model.GetTowerFromId("BoomerangMonkey-Paragon").Duplicate();
                towerModel.AddBehavior(boomerangParagon.GetBehavior<CreateSoundOnAttachedModel>());
                var attackModel = towerModel.GetAttackModel();
                attackModel.weapons[0].projectile.pierce = 125.0f;
                attackModel.weapons[0].Rate *= 0.09f;
                
                
                var projectileModel = attackModel.weapons[0].projectile;
                
                attackModel.weapons[0].projectile.GetDamageModel().damage = 1200.0f;
                projectileModel.GetDamageModel().immuneBloonProperties = BloonProperties.None;

                //make tower pop camo
                towerModel.AddBehavior(new OverrideCamoDetectionModel("ParagonPopsCamo", true));
                towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model2 => model2.isActive = false);
                
            }
        }
        
        public class SuperSpiritOfWrathDisplay : ModTowerDisplay<Druidparagon>
        {
            public override string BaseDisplay => GetDisplay(TowerType.Druid, 0, 0, 5);

            public override bool UseForTower(int[] tiers)
            {
                return IsParagon(tiers);
            }


            public override int ParagonDisplayIndex => 0;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                // node.SaveMeshTexture();
                foreach (var renderer in node.genericRenderers)
                {
                    renderer.material.mainTexture = GetTexture("SuperSpiritOfWrathDisplay");
                }
            }
        }
    }
}
