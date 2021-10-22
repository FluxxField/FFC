using System.Collections.Generic;
using UnboundLib.Cards;
using FFC.MonoBehaviours;
using UnboundLib;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class Barret50Cal : CustomCard {
        private const float ReloadSpeedMultiplier = 1.40f;
        private const float MovementSpeedMultiplier = 0.90f;

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
            gun.reloadTime = ReloadSpeedMultiplier;
            statModifiers.movementSpeed = MovementSpeedMultiplier;

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Marksman]
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
            gunAmmo.maxAmmo = 1;
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();

            ClassesManager.ClassesManager.Instance.RemoveUpgradeCategoriesFromPlayer(characterStats, new List<string> {
                FFC.Barret50Cal
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Insta Kill", true),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", false, null, "1"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeedMultiplier)
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