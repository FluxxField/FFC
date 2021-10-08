using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

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
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                FFC.LightGunnerClassUpgradesCategory,
                FFC.AssaultRifleUpgradeCategory
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
            gunAmmo.maxAmmo += 2;
            
            characterStats.GetAdditionalData().blacklistedCategories.AddRange(new[] {
                FFC.DMRUpgradeCategory,
                FFC.LMGUpgradeCategory
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                Utilities.GetCardInfoStat("Attack Speed", AttackSpeedMultiplier, true),
                Utilities.GetCardInfoStat("Bullet Speed", ProjectileSpeedMultiplier, true),
                new CardInfoStat {
                    positive = true,
                    stat = "Max Ammo",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                Utilities.GetCardInfoStat("Damage", DamageMultiplier, false),
                Utilities.GetCardInfoStat("Reload Speed", ReloadSpeedMultiplier, false)
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