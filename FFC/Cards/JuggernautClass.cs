using FFC.Utilities;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class JuggernautClass : CustomCard {
        private const float MaxHealthMultiplier = 3.00f;
        private const float BlockCooldownMultiplier = 0.75f;
        private const float DamageMultiplier = 1.20f;
        private const float AttackSpeedMultiplier = 0.75f;
        private const float MovementSpeedMultiplier = 0.50f;
        
        protected override string GetTitle() {
            return "CLASS: Juggernaut";
        }

        protected override string GetDescription() {
            return "Years of steroids has turned you into a slow moving, but deadly and unstoppable force";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassCategory
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
            gun.damage *= DamageMultiplier;
            data.maxHealth *= MaxHealthMultiplier;
            block.cooldown *= BlockCooldownMultiplier;
            characterStats.movementSpeed *= MovementSpeedMultiplier;

            // Removes the defaultCategory and this classes upgrade category
            // from the players blacklisted categories
            ClassesManager.ClassesManager.Instance.RemoveDefaultCardCategoryFromPlayer(characterStats);
            characterStats.GetAdditionalData().blacklistedCategories
                .Remove(ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.JuggernautUpgrades]);
        }
        
        public override void OnRemoveCard() {
        }
        
        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Health", true, MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Block Cooldown", true, BlockCooldownMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeedMultiplier)
            };
        }
        
        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}