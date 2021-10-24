using FFC.HitEffects;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.Jester {
    public class JokesOnYou : CustomCard {
        private const int Bounces = 3;

        protected override string GetTitle() {
            return "Jokes on you!";
        }

        protected override string GetDescription() {
            return "Your bouncing bullets no longer damage you!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            gun.reflects = Bounces;
            
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
            player.gameObject.GetOrAddComponent<JokesOnYouHitEffect>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new [] {
                ManageCardInfoStats.BuildCardInfoStat("Bounces", true, null, $"+{Bounces}"),
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