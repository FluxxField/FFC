using System.Collections.Generic;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.Jester {
    public class JesterClass : CustomCard {
        private const float MaxHealth = 0.85f;
        private const float MovementSpeed = 1.15f;
        private const float Size = 0.90f;
        private const int Bounces = 3;
        
        protected override string GetTitle() {
            return "Jester";
        }

        protected override string GetDescription() {
            return "As a Jester, you enjoy bullet bounces";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            statModifiers.health = MaxHealth;
            statModifiers.movementSpeed = MovementSpeed;
            statModifiers.sizeMultiplier = Size;
            
            gun.reflects = Bounces;
            
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
                FFC.Jester
            });
        }
        
        public override void OnRemoveCard() {
        }
        
        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", true, MovementSpeed),
                ManageCardInfoStats.BuildCardInfoStat("Bounces", true, null, $"+{Bounces}"),
                ManageCardInfoStats.BuildCardInfoStat("Health", false, MaxHealth),
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