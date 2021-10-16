using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class Conditioning : CustomCard {
        private const float HealthMultiplier = 1.50f;
        private const float MovementSpeedMultiplier = 1.15f;

        protected override string GetTitle() {
            return "Conditioning";
        }

        protected override string GetDescription() {
            return "You have trained hard! And its paying off...";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.DefaultCardCategory
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
            data.maxHealth *= HealthMultiplier;
            characterStats.movementSpeed *= MovementSpeedMultiplier;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", true, HealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", true, MovementSpeedMultiplier),
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.NatureBrown;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}