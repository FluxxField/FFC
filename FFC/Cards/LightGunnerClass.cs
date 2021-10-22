using System.Collections.Generic;
using FFC.Utilities;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards {
    public class LightGunnerClass : CustomCard {
        private const float MaxHealthMultiplier = 1.10f;
        private const float MovementSpeedMultiplier = 1.10f;
        
        protected override string GetTitle() {
            return "Light Gunner Class";
        }

        protected override string GetDescription() {
            return "As a Light Gunner your prioritize movement over Defence and Health";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            gun.ammo = 3;
            statModifiers.health = MaxHealthMultiplier;
            statModifiers.movementSpeed = MovementSpeedMultiplier;
            
            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ClassesManager.ClassesManager.Instance.ClassCategory
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
            // Removes the defaultCategory and this classes upgrade category from the players blacklisted categories.
            // While also adding the classCategory to the players blacklist
            ClassesManager.ClassesManager.Instance.OnClassCardSelect(characterStats, new List<string> {
                FFC.LightGunnerUpgrades,
                FFC.AssaultRifle,
                FFC.DMR,
                FFC.LMG
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Health", true, MaxHealthMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", true, MovementSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true,null, "+3")
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}