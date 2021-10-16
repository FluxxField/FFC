using UnityEngine;
using UnboundLib.Cards;
using FFC.Utilities;

namespace FFC.Cards {
    class SniperRifleExtendedMag : CustomCard {
        private const float ReloadSpeedMultiplier = 1.10f;
        private const float MovementSpeedMultiplier = 0.95f;
        
        protected override string GetTitle() {
            return "Sniper Rifle Extended Mag";
        }

        protected override string GetDescription() {
            return "Get 1 more shots for your sniper!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
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
            gunAmmo.maxAmmo += 1;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            characterStats.movementSpeed *= MovementSpeedMultiplier;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", true, null, "+1"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Cooldown", false, MovementSpeedMultiplier)
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