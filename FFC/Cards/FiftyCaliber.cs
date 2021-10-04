using UnboundLib.Cards;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using FFC.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace FFC.Cards {
    public class FiftyCaliber : CustomCard {
        protected override string GetTitle() {
            return ".50 Cal";
        }

        protected override string GetDescription() {
            return "Girl Friend: 'Now that's BIG ;)'";
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
                CustomCardCategories.instance.CardCategory(FFC.SniperClassUpgradesCategory)
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
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat() {
                    positive = true,
                    amount = "Insta Kill",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat() {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
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