using FFC.Extensions;
using FFC.MonoBehaviours;
using UnityEngine;
using UnboundLib.Cards;
using FFC.Utilities;
using UnboundLib;

namespace FFC.Cards {
    class SniperRifleExtendedMag : CustomCard {
        private const float ReloadSpeed = 1.10f;
        private const float MovementSpeed = 0.95f;
        private const int MaxAmmo = 1;
        
        protected override string GetTitle() {
            return "Sniper Rifle Extended Mag";
        }

        protected override string GetDescription() {
            return "Then only way to add ammo if you have Barret .50 Cal!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            gun.reloadTime = ReloadSpeed;
            statModifiers.movementSpeed = MovementSpeed;

            var classUpgradeCategories = ClassesManager.ClassesManager.Instance.ClassUpgradeCategories;
            
            cardInfo.categories = new[] {
                classUpgradeCategories[FFC.Marksman],
                classUpgradeCategories[FFC.Barret50Cal]
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
            characterStats.GetAdditionalData().extendedMags += MaxAmmo;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", true, null, $"+{MaxAmmo}"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeed),
                ManageCardInfoStats.BuildCardInfoStat("Movement Cooldown", false, MovementSpeed)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}