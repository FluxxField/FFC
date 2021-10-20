using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

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
            gun.damage = DamageMultiplier;
            gun.attackSpeed = AttackSpeedMultiplier;
            gun.reloadTime = ReloadSpeedMultiplier;
            statModifiers.health = HealthMultiplier;
            
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
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", true, ReloadSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Health", false, HealthMultiplier),
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