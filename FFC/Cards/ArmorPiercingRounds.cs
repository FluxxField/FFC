using FFC.Utilities;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class ArmorPiercingRounds : CustomCard {
        private const float ReloadSpeedMultiplier = 0.50f;
        
        protected override string GetTitle() {
            return "Armor-Piercing Rounds";
        }

        protected override string GetDescription() {
            return "Tired of your friends blocking your shots? This might help";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.MarksmanUpgrades]
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
            gun.unblockable = true;
            gun.reloadTime *= ReloadSpeedMultiplier;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Unblockable", true),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}