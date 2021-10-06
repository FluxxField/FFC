namespace FFC {
    public class Utilities {
        public static CardInfoStat GetCardInfoStat(string statName, float value, bool positive) {
            bool isValuePositive = value > 1;
            string valueSign = isValuePositive ? "+" : "-";
            float percentage = (isValuePositive ? value - 1 : 1 - value) * 100;

            return new CardInfoStat {
                positive = positive,
                stat = statName,
                amount = $"{valueSign}{percentage}%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            };
        }
    }
}