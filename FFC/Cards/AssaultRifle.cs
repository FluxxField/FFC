using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class AssaultRifle : CustomCard {
        private const float DamageMultiplier = 1.10f;
        private const float AttackSpeedMultiplier = 0.60f;
        private const float ReloadSpeedMultiplier = 1.10f;
        private const float ProjectileSpeedMultiplier = 1.10f;
        private const float Spread = 0.8f;
        
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
            gun.dontAllowAutoFire = false;
            gun.damage = DamageMultiplier;
            gun.projectileSpeed = ProjectileSpeedMultiplier;
            gun.attackSpeed = AttackSpeedMultiplier;
            gun.reloadTime = ReloadSpeedMultiplier;
            gun.ammo = 3;
            gun.spread = Spread;
            
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