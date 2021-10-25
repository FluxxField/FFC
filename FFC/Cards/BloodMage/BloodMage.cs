using System.Collections.Generic;
using FFC.Extensions;
using FFC.MonoBehaviours;
using FFC.Utilities;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards.BloodMage {
    public class BloodMage : CustomCard { 
        private const float HealthRegen = 2.5f;
        private const int HealthPerShot = 5;
            
        protected override string GetTitle() {
            return "Blood Mage";
        }

        protected override string GetDescription() {
            return "You have unlocked incredible powers! But, at the cost of blood... Your attacks now cost health";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            statModifiers.regen = HealthRegen;
            gun.projectileColor = Color.red;

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
            var additionalData = statModifiers.GetAdditionalData();
            additionalData.isBloodMage = true;
            additionalData.healthCost += HealthPerShot;
            
            // Removes the defaultCategory and this classes upgrade category from the players blacklisted categories.
            // While also adding the classCategory to the players blacklist
            ClassesManager.ClassesManager.Instance.OnClassCardSelect(characterStats, new List<string> {
                FFC.BloodMage
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health Regen", true, null, $"+{HealthRegen} HP/s"),
                ManageCardInfoStats.BuildCardInfoStat("Health per shot", false, null, $"-{HealthPerShot} HP")
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
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