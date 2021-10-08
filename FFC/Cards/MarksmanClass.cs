using System.Collections.Generic;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

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
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {FFC.MainClassesCategory};
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
            blacklistedCategories.Remove(FFC.DefaultCategory);
            blacklistedCategories.Remove(FFC.MarksmanClassUpgradesCategory);
            blacklistedCategories.AddRange(new[] {
                FFC.MainClassesCategory,
                FFC.LightGunnerClassUpgradesCategory
            });
            characterStats.GetAdditionalData().blacklistedCategories = blacklistedCategories;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                Utilities.GetCardInfoStat("Health", MaxHealthMultiplier, false),
                Utilities.GetCardInfoStat("Damage", DamageMultiplier, true),
                new CardInfoStat {
                    positive = true,
                    stat = "Bullet Gravity",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                Utilities.GetCardInfoStat("Projectile Speed", ProjectileSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Attack Speed", AttackSpeedMultiplier, false),
                new CardInfoStat {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "-1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}