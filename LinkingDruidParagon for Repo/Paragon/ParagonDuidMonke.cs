using MelonLoader;


using Assets.Scripts.Unity.UI_New.InGame;
using static Assets.Scripts.Simulation.Bloons.Behaviors.DamageOverTimeCustom;
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
using static Assets.Scripts.Simulation.Towers.Weapons.Weapon;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using NinjaKiwi.Common;
using Assets.Scripts.Models.Towers.Filters;
using BTD_Mod_Helper.Api.Display;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Unity.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.GenericBehaviors;

namespace LinkingDruidParagon.Paragon
{
    public class TheParagon
    {
        public class Druidparagon : ModVanillaParagon
        {
            public override string BaseTower => "Druid-005";
        }
        public class GodSpiritOfNature : ModParagonUpgrade<Druidparagon>
        {
            public override string Description => "So powerful, God had to be summoned to use the power.";
            public override string Icon => "GodSpiritOfTheForest_Icon";
            public override string Portrait => "GodSpiritOfWrath_Portrait";
            public override string DisplayName => "God, Spirit Of Nature";
            public override int Cost => 4000000;
            

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var boomerangParagon = Game.instance.model.GetTowerFromId("BoomerangMonkey-Paragon").Duplicate();
                towerModel.AddBehavior(boomerangParagon.GetBehavior<CreateSoundOnAttachedModel>());
                var attackModel = towerModel.GetAttackModel();
                var projectileModel = attackModel.weapons[0].projectile;

                // super storm stuff
                var SuperStormWeapon = Game.instance.model.GetTowerFromId("Druid-500").GetWeapon(3);
                SuperStormWeapon.projectile.pierce *= 10;
                SuperStormWeapon.Rate *= 0.5f;
                attackModel.AddWeapon(SuperStormWeapon);

                // vines
                var ForestVines = Game.instance.model.GetTowerFromId("Druid-050").GetBehavior<SpiritOfTheForestModel>().Duplicate();
                var ForestVineStats = ForestVines.GetDescendant<DamageOverTimeCustomModel>();
                ForestVineStats.intervalFrames = 1;
                ForestVineStats.immuneBloonProperties = BloonProperties.None;
                towerModel.AddBehavior(ForestVines);
                
                // normal stuff
                attackModel.weapons[0].Rate *= 0.09f;
                
                attackModel.weapons[0].projectile.GetDamageModel().damage = 1200.0f;
                projectileModel.GetDamageModel().immuneBloonProperties = BloonProperties.None;

                //make tower pop camo
                towerModel.AddBehavior(new OverrideCamoDetectionModel("ParagonPopsCamo", true));
                towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model2 => model2.isActive = false);
                
            }
        }
        
        public class SuperSpiritOfWrathDisplay : ModTowerDisplay<Druidparagon>
        {
            public override string BaseDisplay =>
            Game.instance.model.GetTower(TowerType.SuperMonkey, 5).GetAttackModel().GetBehavior<DisplayModel>().display;


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
                    renderer.material.mainTexture = GetTexture("GodSpiritOfWrathDisplay");
                }
            }
        }
        
    }
}
