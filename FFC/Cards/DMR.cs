using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class DMR : CustomCard {
        private const float DamageMultiplier = 1.35f;
        private const float ProjectileSpeedMultiplier = 1.50f;
        private const float AttackSpeedMultiplier = 1.35f;
        private const float ReloadSpeedMultiplier = 1.50f;
        protected override string GetTitle() {
            return "DMR";
        }

        protected override string GetDescription() {
            return "More Damage, but less ammo and attack speed";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {FFC.LightGunnerClassUpgradesCategory};
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
            gun.dontAllowAutoFire = true;
            gun.damage *= DamageMultiplier;
            gun.projectileSpeed *= ProjectileSpeedMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            gunAmmo.maxAmmo = 3;
            
            characterStats.GetAdditionalData().blacklistedCategories.Add(FFC.AssaultRifleUpgradeCategory);
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                Utilities.GetCardInfoStat("Damage", DamageMultiplier, true),
                Utilities.GetCardInfoStat("Bullet Speed", ProjectileSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Attack Speed", AttackSpeedMultiplier, false),
                Utilities.GetCardInfoStat("Reload Speed", ReloadSpeedMultiplier, false),
                new CardInfoStat {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
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