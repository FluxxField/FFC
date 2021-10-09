using System.Collections.Generic;
using System.Linq;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.Utils;

namespace FFC.Utilities {
    public class ManageCardCategories {
        public static CardCategory DefaultCategory;
        public static CardCategory MainClassesCategory;
        public static CardCategory MarksmanClassUpgradesCategory;
        public static CardCategory LightGunnerClassUpgradesCategory;
        public static CardCategory JuggernautClassUpgradesCategory;
        public static CardCategory AssaultRifleUpgradeCategory;
        public static CardCategory DMRUpgradeCategory;
        public static CardCategory LMGUpgradeCategory;
        
        private ManageCardCategories() {
            // Gotta give CustomCardCategories a sec to setup
            if (CustomCardCategories.instance != null) {
                DefaultCategory = CustomCardCategories.instance.CardCategory("Default");
                MainClassesCategory = CustomCardCategories.instance.CardCategory("MainClasses");
                MarksmanClassUpgradesCategory = CustomCardCategories.instance.CardCategory("MarksmanUpgrades");
                LightGunnerClassUpgradesCategory = CustomCardCategories.instance.CardCategory("LightGunnerUpgrades");
                AssaultRifleUpgradeCategory = CustomCardCategories.instance.CardCategory("AssaultRifle");
                DMRUpgradeCategory = CustomCardCategories.instance.CardCategory("DMR");
                LMGUpgradeCategory = CustomCardCategories.instance.CardCategory("LMG");
            }
        }
        
        public static void HandleBuildDefaultCategory() {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Building Default categories");
            foreach (Card card in CardManager.cards.Values.ToList()) {
                List<CardCategory> categories = card.cardInfo.categories.ToList();

                if (categories.Count == 0 || card.category != "FFC") {
                    categories.Add(DefaultCategory);
                    card.cardInfo.categories = categories.ToArray();
                }
            }
        }
    }
}