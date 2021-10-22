using System.Collections;
using FFC.Extensions;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;
using CharacterStatModifiersExtension = FFC.Extensions.CharacterStatModifiersExtension;

namespace FFC.Cards {
    public class AdaptiveSizing : CustomCard {
        private const float MaxHealthMultiplier = 1.25f;
        private const float MaxAdaptiveMovementSpeedMultiplier = 0.35f;
        private const float MaxAdaptiveGravityMultiplier = 0.25f;
        
        protected override string GetTitle() {
            return "Adaptive Sizing";
        }

        protected override string GetDescription() {
            return "You get smaller and run faster as you take damage";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            statModifiers.health = MaxHealthMultiplier;
            
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassUpgradeCategories[FFC.JuggernautUpgrades]
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
            var additionalData = CharacterStatModifiersExtension.GetAdditionalData(characterStats);
            additionalData.hasAdaptiveSizing = true;
            additionalData.adaptiveMovementSpeed += MaxAdaptiveMovementSpeedMultiplier;
            additionalData.adaptiveGravity += MaxAdaptiveGravityMultiplier;
            player.gameObject.GetOrAddComponent<AdaptiveSizingMono>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", true, MaxHealthMultiplier)
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

        public static IEnumerator SetPrePointStats(IGameModeHandler gm) {
            foreach (var player in PlayerManager.instance.players) {
                var additionalData = player.data.stats.GetAdditionalData();

                if (additionalData.hasAdaptiveSizing) {
                    player.gameObject.GetComponent<AdaptiveSizingMono>().SetPrePointStats(player.data);
                }
            }
            
            yield break;
        }
    }
}