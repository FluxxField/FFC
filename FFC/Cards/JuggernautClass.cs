using System.Collections.Generic;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class JuggernautClass : CustomCard {
        private const float MaxHealthMultiplier = 3.50f;
        private const float MovementSpeedMultiplier = 0.65f;
        private const float GravityMultiplier = 0.75f;
        private const float SizeMultiplier = 1.30f;
        
        protected override string GetTitle() {
            return "Juggernaut";
        }

        protected override string GetDescription() {
            return "Years of steroids has turned you into a slow moving, but deadly and unstoppable force";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            statModifiers.health = MaxHealthMultiplier;
            statModifiers.movementSpeed = MovementSpeedMultiplier;
            statModifiers.jump = GravityMultiplier;
            statModifiers.sizeMultiplier = SizeMultiplier;
            
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassCategory
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
            // Removes the defaultCategory and this classes upgrade category from the players blacklisted categories.
            // While also adding the classCategory to the players blacklist
            ClassesManager.ClassesManager.Instance.OnClassCardSelect(characterStats, new List<string> {
                FFC.JuggernautUpgrades
            });
        }
        
        public override void OnRemoveCard() {
        }
        
        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", true, MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Gravity", false, GravityMultiplier)
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