using System;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards
{
    class InvisibleBullets : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.bulletDamageMultiplier = 0.5f;
            gun.projectileColor = Color.clear;

            if (gun.destroyBulletAfter == 0f)
            {
                gun.destroyBulletAfter = 5f;
            }
        }

        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Invisible Bullets";
        }
        
        protected override string GetDescription()
        {
            return "Am I actually shooting anything!? I can't tell...";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "Invisible",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
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
