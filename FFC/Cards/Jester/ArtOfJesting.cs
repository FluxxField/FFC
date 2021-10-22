using FFC.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.Jester {
    public class ArtOfJesting : CustomCard {
        protected override string GetTitle() {
            return "The Art Of Jesting";
        }

        protected override string GetDescription() {
            return "PASSIVE: Your stats increase as your pick cards that give you more bounces. Stats are added per bounce";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Jester]
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
            player.gameObject.GetOrAddComponent<ArtOfJestingMono>();
        }
        
        public override void OnRemoveCard() {
        }
        
        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat() {
                    positive = true,
                    stat = "Damage",
                    amount = "+3%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat() {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+2%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat() {
                    positive = true,
                    stat = "Projectile Speed",
                    amount = "-3%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
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