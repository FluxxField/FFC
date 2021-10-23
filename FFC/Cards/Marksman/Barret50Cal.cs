using System.Collections.Generic;
using FFC.HitEffects;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.Marksman {
    public class Barret50Cal : CustomCard {
        private const float ReloadSpeed = 1.40f;
        private const float MovementSpeed = 0.90f;
        private const int MaxAmmo = 1;

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
            gun.reloadTime = ReloadSpeed;
            statModifiers.movementSpeed = MovementSpeed;

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
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();
            player.gameObject.GetOrAddComponent<Barret50CalMono>();

            ClassesManager.ClassesManager.Instance.RemoveUpgradeCategoriesFromPlayer(characterStats, new List<string> {
                FFC.Barret50Cal
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Insta Kill", true),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", false, null, $"{MaxAmmo}"),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeed),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeed)
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