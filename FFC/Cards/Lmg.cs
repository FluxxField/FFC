using FFC.MonoBehaviours;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;
using UnboundLib;

namespace FFC.Cards {
    public class Lmg : CustomCard {
        private const float DamageMultiplier = 1.35f;
        private const float ProjectileSpeedMultiplier = 1.30f;
        private const float AttackSpeedMultiplier = 1.50f;
        private const float ReloadSpeedMultiplier = 2.50f;
        private const float MovementSpeedMultiplier = 0.70f;
        private const float Spread = 0.5f;

        protected override string GetTitle() {
            return "LMG";
        }

        protected override string GetDescription() {
            return "'SAY HELLO TO MY LITTLE FRIEND' - Said someone, somewhere";
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
            gun.ammo = 12;
            gun.reloadTime = ReloadSpeedMultiplier;
            gun.spread = Spread;
            statModifiers.movementSpeed = MovementSpeedMultiplier;

            cardInfo.allowMultiple = false;
            
            // LMG is apart of the LightGunnerClass and DMR Categories
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.LightGunner],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Lmg]
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
            // If the player picks LMG, blacklist all cards in the AssaultRifle and DMR categories
            characterStats.GetAdditionalData().blacklistedCategories.AddRange(new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.AssaultRifle],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Dmr]
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true,null, "+12"),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", false, AttackSpeedMultiplier, "", "-"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeedMultiplier)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
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