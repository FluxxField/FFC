using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using FFC.Extensions;
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
        private const string Version = "1.2.5";

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
            ClassesManager.ClassesManager.Instance.AddClassUpgradeCategories(new List<string> {
                MarksmanUpgrades,
                LightGunnerUpgrades,
                JuggernautUpgrades,
                AssaultRifle,
                DMR,
                LMG,
                Barret50Cal
            });

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
            CustomCard.BuildCard<SizeMatters>();
            // Default
            CustomCard.BuildCard<FastMags>();
            CustomCard.BuildCard<Conditioning>();
            CustomCard.BuildCard<BattleExperience>();

            Unbound.RegisterCredits(ModName,
                new[] {"FluxxField"},
                new[] {"github"},
                new[] {"https://github.com/FluxxField/FFC"});

            GameModeManager.AddHook(GameModeHooks.HookRoundStart, HandleBarret50CalAmmo);
            GameModeManager.AddHook(GameModeHooks.HookPointStart, SizeMatters.SetPrePointStats);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, CharacterStatModifiersExtension.Reset);
        }

        private static IEnumerator HandleBarret50CalAmmo(IGameModeHandler gm) {
            var players = PlayerManager.instance.players.ToArray();

            foreach (var player in players) {
                var cards = player.data.currentCards;
                var ammoCount = 0;
                var has50Cal = false;

                foreach (var card in cards) {
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