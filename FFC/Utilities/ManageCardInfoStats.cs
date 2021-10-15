namespace FFC.Utilities {
    public static class ManageCardInfoStats {
        public static CardInfoStat BuildCardInfoStat(
            string statName,
            bool positive,
            float? value = null,
            string explicitValue = "",
            string signOverride = null
        ) {
            if (value == null) {
                return new CardInfoStat() {
                    positive = positive,
                    stat = statName,
                    amount = explicitValue,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                };
            }
            
            bool isValuePositive = value > 1;
            string valueSign = isValuePositive ? "+" : "-";
            float? percentage = (isValuePositive ? value - 1 : 1 - value) * 100;

            if (signOverride != null) {
                valueSign = signOverride;
            }

            return new CardInfoStat {
                positive = positive,
                stat = statName,
                amount = $"{valueSign}{percentage:F1}%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            };
        }
    }
}