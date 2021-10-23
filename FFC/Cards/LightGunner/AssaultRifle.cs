using FFC.MonoBehaviours;
using FFC.Utilities;
using ModdingUtils.Extensions;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.LightGunner {
    public class AssaultRifle : CustomCard {
        private const float Damage = 1.10f;
        private const float AttackSpeed = 0.60f;
        private const float ReloadSpeed = 1.10f;
        private const float ProjectileSpeed = 1.10f;
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
            gun.damage = Damage;
            gun.projectileSpeed = ProjectileSpeed;
            gun.attackSpeed = AttackSpeed;
            gun.reloadTime = ReloadSpeed;
            gun.ammo = MaxAmmo;

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
                ManageCardInfoStats.BuildCardInfoStat("Damage", true, Damage),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeed),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", true, AttackSpeed),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true, null, $"+{MaxAmmo}"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeed)
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