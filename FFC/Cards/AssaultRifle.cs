using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class AssaultRifle : CustomCard {
        private const float DamageMultiplier = 0.80f;
        private const float AttackSpeedMultiplier = 1.40f;
        private const float ReloadSpeedMultiplier = 1.15f;
        private const float ProjectileSpeedMultiplier = 0.90f;
        
        protected override string GetTitle() {
            return "Assault Rifle";
        }

        protected override string GetDescription() {
            return "Less damage but a higher rate of fire with a larger mag";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            
            // AssaultRifle is apart of the LightGunnerClass and AssaultRifle Categories
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LightGunnerUpgrades],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.AssaultRifle]
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
            gun.dontAllowAutoFire = false;
            gun.damage *= DamageMultiplier;
            gun.projectileSpeed *= ProjectileSpeedMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            gunAmmo.maxAmmo += 3;
            
            // If the player picks AssaultRifle, blacklist all cards in the DMR and LMG categories
            characterStats.GetAdditionalData().blacklistedCategories.AddRange(new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.DMR],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LMG]
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true, null, "+3"),
                ManageCardInfoStats.BuildCardInfoStat("Damage", false, DamageMultiplier),
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