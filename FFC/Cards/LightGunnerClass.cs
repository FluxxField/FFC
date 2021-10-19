using FFC.Utilities;
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
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassCategory
            };
            
            gun.damage = DamageMultiplier;
            gun.attackSpeed = AttackSpeedMultiplier;
            gun.dontAllowAutoFire = true;
            gun.ammo = 3;
            statModifiers.health = MaxHealthMultiplier;
            statModifiers.movementSpeed = MovementSpeedMultiplier;
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
            block.cooldown *= BlockCooldownMultiplier;

            // Removes the defaultCategory and this classes upgrade category
            // from the players blacklisted categories
            ClassesManager.ClassesManager.Instance.RemoveDefaultCardCategoryFromPlayer(characterStats);
            characterStats.GetAdditionalData().blacklistedCategories
                .Remove(ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LightGunnerUpgrades]);
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", false, MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Damage", false, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", true, MovementSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true,null, "+3"),
                ManageCardInfoStats.BuildCardInfoStat("Block Cooldown", false, BlockCooldownMultiplier),
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