using UnboundLib.Cards;
using FFC.MonoBehaviours;
using UnboundLib;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class Barret50Cal : CustomCard {
        private const float ReloadSpeedMultiplier = 0.50f;
        
        protected override string GetTitle() {
            return "Barret .50 Cal";
        }

        protected override string GetDescription() {
            return "Girl Friend: 'Now that's BIG ;)' *Ammo can only be added by Sniper Rifle Extended Mag*";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.ClassUpgradeCategories[FFC.MarksmanUpgrades]
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
            gunAmmo.maxAmmo = 1;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Insta Kill", true),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", false, null, "1"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}