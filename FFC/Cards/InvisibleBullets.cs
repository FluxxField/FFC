using System;
using UnboundLib.Cards;
using UnityEngine;

namespace FFC.Cards
{
    class InvisibleBullets : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            throw new NotImplementedException();
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            throw new NotImplementedException();
        }

        public override void OnRemoveCard()
        {
            throw new NotImplementedException();
        }

        protected override string GetTitle()
        {
            throw new NotImplementedException();
        }

        protected override GameObject GetCardArt()
        {
            throw new NotImplementedException();
        }

        protected override string GetDescription()
        {
            throw new NotImplementedException();
        }

        protected override CardInfo.Rarity GetRarity()
        {
            throw new NotImplementedException();
        }

        protected override CardInfoStat[] GetStats()
        {
            throw new NotImplementedException();
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            throw new NotImplementedException();
        }
    }
}
