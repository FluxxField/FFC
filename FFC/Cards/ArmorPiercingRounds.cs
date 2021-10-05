using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class ArmorPiercingRounds : CustomCard {
        protected override string GetTitle() {
            return "Armor-Piercing Rounds";
        }

        protected override string GetDescription() {
            return "Tired of your friends blocking your shots? This might help";
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
                FFC.SniperClassUpgradesCategory
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
            gun.unblockable = true;
            gun.reloadTime += 0.5f;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat {
                    positive = true,
                    amount = "Unblockable",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
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