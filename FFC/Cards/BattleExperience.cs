using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class BattleExperience : CustomCard {
        private const float DamageMultiplier = 1.15f;
        private const float ReloadSpeedMultiplier = 0.75f;
        private const float AttackSpeedMultiplier = 0.90f;
        private const float HealthMultiplier = 0.90f;

        protected override string GetTitle() {
            return "Battle Experience";
        }

        protected override string GetDescription() {
            return "A few tours later...";
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
            gun.damage *= DamageMultiplier;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            data.maxHealth *= HealthMultiplier;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                Utilities.GetCardInfoStat("Damage", DamageMultiplier, true),
                Utilities.GetCardInfoStat("Reload Speed", ReloadSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Attack Speed", AttackSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Health", HealthMultiplier, false),
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
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