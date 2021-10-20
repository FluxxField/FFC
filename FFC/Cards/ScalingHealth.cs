using FFC.Utilities;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class ScalingHealth : CustomCard {
        private const float MaxHealthMultiplier = 1.25f;
        private const float MaxMovementSpeedMultiplier = 1.15f;
        
        protected override string GetTitle() {
            return "Scaling Health";
        }

        protected override string GetDescription() {
            return "Your size and speed is dependent on current health instead of your max health";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            statModifiers.health = MaxHealthMultiplier;

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.JuggernautUpgrades]
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
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", true, MaxHealthMultiplier)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Uncommon;
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