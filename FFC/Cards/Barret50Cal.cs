using UnboundLib.Cards;
using FFC.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace FFC.Cards {
    public class Barret50Cal : CustomCard {
        private const float ReloadSpeedMultiplier = 0.50f;
        
        protected override string GetTitle() {
            return "Barret .50 Cal";
        }

        protected override string GetDescription() {
            return "Girl Friend: 'Now that's BIG ;)' *Ammo can only be added by Sniper Rifle Extended Mag*";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                FFC.MarksmanClassUpgradesCategory
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
            gunAmmo.maxAmmo = 1;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat {
                    positive = true,
                    amount = "Insta Kill",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                Utilities.GetCardInfoStat("Reload Speed", ReloadSpeedMultiplier, false)
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