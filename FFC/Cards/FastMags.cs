using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class FastMags : CustomCard {
        protected override string GetTitle() {
            return "Fast Mags";
        }

        protected override string GetDescription() {
            return "After many missed shots and constantly having to reloading.. You bought fast mags. But, it didn't fix your aim";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");
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
            gunAmmo.reloadTimeMultiplier -= 0.15f;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat() {
                    positive = true,
                    stat = "Reload Speed",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}