using System.Collections.Generic;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class LightGunner : CustomCard {
        protected override string GetTitle() {
            return "Light Gunner";
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
            data.maxHealth = 80f;
            characterStats.movementSpeed *= 1.20f;
            gun.damage = 0.4f; // 22 damage
            gun.attackSpeed = 0.33f;
            block.cooldown = 5f; // 5s cooldown
            gunAmmo.maxAmmo = 6;
            gun.dontAllowAutoFire = true;

            List<CardCategory> blacklistedCategories = characterStats.GetAdditionalData().blacklistedCategories;
            blacklistedCategories.Remove(FFC.LightGunnerClassUpgradesCategory);
            blacklistedCategories.Add(FFC.MainClassesCategory);
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat {
                    positive = true,
                    stat = "80 Health",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = true,
                    stat = "22 Damage",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = true,
                    stat = "0.33s Attack Speed",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "5s Block Cooldown",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
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