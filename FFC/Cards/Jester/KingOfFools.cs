using FFC.HitEffects;
using FFC.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.Jester {
    public class KingOfFools : CustomCard {
        protected override string GetTitle() {
            return "King Of Fools";
        }

        protected override string GetDescription() {
            return "You have become the King of Fools! Chance for bullet bounces to spawn more bullets!";
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
            player.gameObject.GetOrAddComponent<KingOfFoolsHitSurfaceEffect>();
        }
        
        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return null;
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