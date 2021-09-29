using System;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards
{
    class ExtendedMag : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo += (int)Math.Floor(gunAmmo.maxAmmo * (1f / 3f));

            gun.reloadTime = 0.85f;
            characterStats.movementSpeed = 0.9f;
        }

        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Extended Mag";
        }

        protected override string GetDescription()
        {
            return "Swap out your mag for an extended version!";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
{
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+33%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        public override string GetModName()
        {
            return FFC.ModNameAbr;
        }
    }
}
