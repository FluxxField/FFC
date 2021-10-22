using System.Collections.Generic;
using FFC.MonoBehaviours;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;
using UnboundLib;

namespace FFC.Cards {
    class MarksmanClass : CustomCard {
        private const float MaxHealthMultiplier = 0.50f;
        private const float DamageMultiplier = 1.80f;
        private const float ProjectileSpeedMultiplier = 2.00f;
        private const float AttackSpeedMultiplier = 3.00f;
        private const float ReloadSpeedMultiplier = 1.20f;

        protected override string GetTitle() {
            return "Marksman";
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
            gun.damage = DamageMultiplier;
            gun.projectileSpeed = ProjectileSpeedMultiplier;
            gun.attackSpeed = AttackSpeedMultiplier;
            gun.reloadTime = ReloadSpeedMultiplier;
            gun.gravity = 0f;
            gun.ammo = -1;
            statModifiers.health = MaxHealthMultiplier;
            
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassCategory
            };

            gameObject.GetOrAddComponent<ClassNameMono>();
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
            // Removes the defaultCategory and this classes upgrade category from the players blacklisted categories.
            // While also adding the classCategory to the players blacklist
            ClassesManager.ClassesManager.Instance.OnClassCardSelect(characterStats, new List<string> {
                FFC.Marksman
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Gravity", true, null, "No"),
                ManageCardInfoStats.BuildCardInfoStat("Projectile Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Health", false,MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", false, AttackSpeedMultiplier, "", "-"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier),
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