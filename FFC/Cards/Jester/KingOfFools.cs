using FFC.Extensions;
using FFC.HitEffects;
using FFC.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CharacterStatModifiersExtension = ModdingUtils.Extensions.CharacterStatModifiersExtension;

namespace FFC.Cards.Jester {
    public class KingOfFools : CustomCard {
        protected override string GetTitle() {
            return "King Of Fools";
        }

        protected override string GetDescription() {
            return
                "You have become the King of Fools! You know have a 15% for bullet bounces to spawn an extra bullet!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.Jester],
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.KingOfFoolsCategory]
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
            var kingOfFools = characterStats.GetAdditionalData().kingOfFools += 1;

            switch (kingOfFools) {
                case 1: {
                    player.gameObject.GetOrAddComponent<KingOfFoolsHitSurfaceEffect>();
                    break;
                }
                case 2: {
                    CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.KingOfFoolsCategory]);
                    break;
                }
            }
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