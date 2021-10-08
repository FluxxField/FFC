using System.Collections.Generic;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class LightGunnerClass : CustomCard {
        private const float MaxHealthMultiplier = 0.80f;
        private const float DamageMultiplier = 0.60f;
        private const float AttackSpeedMultiplier = 0.66f;
        private const float MovementSpeedMultiplier = 1.10f;
        private const float BlockCooldownMultiplier = 1.25f;
        
        protected override string GetTitle() {
            return "CLASS: Light Gunner";
        }

        protected override string GetDescription() {
            return "As a Light Gunner your prioritize movement over Defence and Health";
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
            characterStats.movementSpeed *= MovementSpeedMultiplier;
            gun.damage *= DamageMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            block.cooldown *= BlockCooldownMultiplier;
            gunAmmo.maxAmmo += 3;
            gun.dontAllowAutoFire = true;

            List<CardCategory> blacklistedCategories = characterStats.GetAdditionalData().blacklistedCategories;
            blacklistedCategories.Remove(FFC.DefaultCategory);
            blacklistedCategories.Remove(FFC.LightGunnerClassUpgradesCategory);
            blacklistedCategories.AddRange(new [] {
                FFC.MainClassesCategory,
                FFC.MarksmanClassUpgradesCategory,
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                Utilities.GetCardInfoStat("Health", MaxHealthMultiplier, false),
                Utilities.GetCardInfoStat("Damage", DamageMultiplier, false),
                Utilities.GetCardInfoStat("Attack Speed", AttackSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Movement Speed", MovementSpeedMultiplier, true),
                new CardInfoStat {
                    positive = true,
                    stat = "Max Ammo",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                Utilities.GetCardInfoStat("Block Cooldown", BlockCooldownMultiplier, false),
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}