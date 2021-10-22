using FFC.MonoBehaviours;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;
using UnboundLib;

namespace FFC.Cards {
    public class AssaultRifle : CustomCard {
        private const float DamageMultiplier = 1.10f;
        private const float AttackSpeedMultiplier = 0.60f;
        private const float ReloadSpeedMultiplier = 1.10f;
        private const float ProjectileSpeedMultiplier = 1.10f;
        private const float Spread = 0.025f;
        private const int MaxAmmo = 3;
        
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
            gun.ammo = MaxAmmo;
            gun.spread = Spread;
            
            cardInfo.allowMultiple = false;

            var upgradeCategories = ClassesManager.ClassesManager.Instance.ClassUpgradeCategories;
            // AssaultRifle is apart of the LightGunnerClass and AssaultRifle Categories
            cardInfo.categories = new[] {
                upgradeCategories[FFC.LightGunner],
                upgradeCategories[FFC.AssaultRifle]
            };
            
            gameObject.GetOrAddComponent<ClassNameMono>();
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
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Dmr],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Lmg]
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true, null, $"+{MaxAmmo}"),
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