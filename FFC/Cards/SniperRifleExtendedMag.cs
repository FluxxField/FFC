using UnityEngine;
using UnboundLib.Cards;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace FFC.Cards {
    class SniperRifleExtendedMag : CustomCard {
        protected override string GetTitle() {
            return "Sniper Rifle Extended Mag";
        }

        protected override string GetDescription() {
            return "Get 1 more shots for your sniper!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");
            
            cardInfo.allowMultiple = true;
            cardInfo.categories = new []
            {
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
            gunAmmo.maxAmmo += 1;
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat() {
                    positive = true,
                    stat = "Max Ammo",
                    amount = "1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Uncommon;
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