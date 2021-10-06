using UnboundLib.Cards;
using UnityEngine;

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
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");
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
                Utilities.GetCardInfoStat("Health", HealthMultiplier, true),
                Utilities.GetCardInfoStat("Movement Speed", MovementSpeedMultiplier, true),
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