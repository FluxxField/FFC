using System.Collections;
using System.Collections.Generic;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using UnboundLib.GameModes;

namespace FFC {
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FFC : BaseUnityPlugin {
        public const string AbbrModName = "FFC";
        
        private const string ModId = "fluxxfield.rounds.plugins.fluxxfieldscards";
        private const string ModName = "FluxxField's Cards (FFC)";
        private const string Version = "1.1.2";

        public const string MarksmanUpgrades = "marksmanUpgrades";
        public const string LightGunnerUpgrades = "lightGunnerUpgrades";
        public const string JuggernautUpgrades = "juggernautUpgrades";
        public const string AssaultRifle = "assaultRifle";
        public const string DMR = "DMR";
        public const string LMG = "LMG";
        public const string Barret50Cal = "barret50Cal";

        private void Awake() {
            new Harmony(ModId).PatchAll();
        }


        private void Start() {
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(MarksmanUpgrades);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(LightGunnerUpgrades);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(JuggernautUpgrades);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(AssaultRifle);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(DMR);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(LMG);
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategory(Barret50Cal);
            
            // Marksman Class
            CustomCard.BuildCard<MarksmanClass>();
            CustomCard.BuildCard<SniperRifleExtendedMag>();
            CustomCard.BuildCard<Barret50Cal>();
            CustomCard.BuildCard<ArmorPiercingRounds>();
            // Light Gunner Class
            CustomCard.BuildCard<LightGunnerClass>();
            CustomCard.BuildCard<AssaultRifle>();
            CustomCard.BuildCard<DMR>();
            CustomCard.BuildCard<LMG>();
            // Juggernaut Class
            CustomCard.BuildCard<JuggernautClass>();
            // Default
            CustomCard.BuildCard<FastMags>();
            CustomCard.BuildCard<Conditioning>();
            CustomCard.BuildCard<BattleExperience>();

            Unbound.RegisterCredits(ModName,
                new[] {"FluxxField"},
                new[] {"github"},
                new[] {"https://github.com/FluxxField/FFC"});

            GameModeManager.AddHook(GameModeHooks.HookRoundStart, gm => HandleBarret50CalAmmo());
        }

        private IEnumerator HandleBarret50CalAmmo() {
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach (Player player in players) {
                List<CardInfo> cards = player.data.currentCards;
                int ammoCount = 0;
                bool has50Cal = false;

                foreach (CardInfo card in cards) {
                    switch (card.cardName.ToUpper()) {
                        case "BARRET .50 CAL": {
                            has50Cal = true;
                            goto case "SNIPER RIFLE EXTENDED MAG";
                        }
                        case "SNIPER RIFLE EXTENDED MAG": {
                            ammoCount += 1;
                            break;
                        }
                    }
                }

                if (has50Cal) {
                    player
                        .GetComponent<Holding>()
                        .holdable.GetComponent<Gun>()
                        .GetComponentInChildren<GunAmmo>()
                        .maxAmmo = ammoCount;
                }
            }

            yield break;
        }
    }
}