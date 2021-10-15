using System.Collections.Generic;
using System.Linq;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    class MarksmanClass : CustomCard {
        private const float MaxHealthMultiplier = 0.50f;
        private const float DamageMultiplier = 1.60f;
        private const float ProjectileSpeedMultiplier = 2.00f;
        private const float AttackSpeedMultiplier = 3.00f;

        protected override string GetTitle() {
            return "CLASS: Marksman";
        }

        protected override string GetDescription() {
            return "All or nothing. Precision is key";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.DefaultCardCategory
            };
        }

        public override void OnAddCard(
            Player player,
            Gun gun,
            GunAmmo gunAmmo,
            CharacterData data,
            HealthHandler health,
            Gravity gravity,
            Block block,
            CharacterStatModifiers characterStats
        ) {
            data.maxHealth *= MaxHealthMultiplier;
            gun.damage *= DamageMultiplier;
            gun.projectileSpeed *= ProjectileSpeedMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            gun.gravity = 0f;
            gunAmmo.maxAmmo -= 1;

            List<CardCategory> blacklistedCategories = characterStats.GetAdditionalData().blacklistedCategories;
            
            // Removes the defaultCategory and this classes upgrade category
            // from the players blacklisted categories
            blacklistedCategories.RemoveAll(category => new[] {
                ClassesManager.ClassesManager.DefaultCardCategory,
                ClassesManager.ClassesManager.ClassUpgradeCategories[FFC.MarksmanUpgrades]
            }.Contains(category));
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", false,MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Gravity", true, null, "No"),
                ManageCardInfoStats.BuildCardInfoStat("Projectile Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", false, AttackSpeedMultiplier, "", "-"),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", false, null, "-1")
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}