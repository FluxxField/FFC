namespace FFC.Utilities {
    public static class ManageCardInfoStats {
        public static CardInfoStat BuildCardInfoStat(
            string statName,
            bool positive,
            float? value = null,
            string explicitValue = ""
        ) {
            if (value != null) {
                bool isValuePositive = value > 1;
                string valueSign = isValuePositive ? "+" : "-";
                float? percentage = (isValuePositive ? value - 1 : 1 - value) * 100;

                return new CardInfoStat {
                    positive = positive,
                    stat = statName,
                    amount = $"{valueSign}{percentage}%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                };
            }

            return new CardInfoStat() {
                positive = positive,
                stat = statName,
                amount = explicitValue,
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            };
        }
    }
}