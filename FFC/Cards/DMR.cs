using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class DMR : CustomCard {
        private const float DamageMultiplier = 1.50f;
        private const float ProjectileSpeedMultiplier = 1.50f;
        private const float AttackSpeedMultiplier = 2.00f;
        private const float ReloadSpeedMultiplier = 1.20f;
        
        protected override string GetTitle() {
            return "DMR";
        }

        protected override string GetDescription() {
            return "More Damage and ammo, but less attack speed";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            gun.dontAllowAutoFire = true;
            gun.damage = DamageMultiplier;
            gun.projectileSpeed = ProjectileSpeedMultiplier;
            gun.attackSpeed = AttackSpeedMultiplier;
            gun.reloadTime = ReloadSpeedMultiplier;

            cardInfo.allowMultiple = false;
            
            // DMR is apart of the LightGunnerClass and DMR Categories
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LightGunnerUpgrades],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.DMR]
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
            // If the player picks DMR, blacklist all cards in the AssaultRifle and LMG categories
            characterStats.GetAdditionalData().blacklistedCategories.AddRange(new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.AssaultRifle],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LMG]
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", false, AttackSpeedMultiplier, "", "-"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier)
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