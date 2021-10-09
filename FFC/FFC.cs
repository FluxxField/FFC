using System.Collections;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using FFC.Utilities;
using ModdingUtils.Extensions;
using Photon.Pun;
using UnboundLib.GameModes;
using UnboundLib.Utils.UI;
using UnityEngine;
using TMPro;
using UnboundLib.Networking;

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
        private const string Version = "1.0.1";

        private static ConfigEntry<bool> _useClassFirstRoundConfig;
        private static bool _useClassesFirstRound;

        private void Awake() {
            _useClassFirstRoundConfig = Config.Bind("FFC", "Enabled", false, "Enable classes only first round");
            new Harmony(ModId).PatchAll();
        }


        private void Start() {
            _useClassesFirstRound = _useClassFirstRoundConfig.Value;

            UnityEngine.Debug.Log($"[{AbbrModName}] Building cards");
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
            // Default
            CustomCard.BuildCard<FastMags>();
            CustomCard.BuildCard<Conditioning>();
            CustomCard.BuildCard<BattleExperience>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");

            this.ExecuteAfterSeconds(0.4f, ManageCardCategories.HandleBuildDefaultCategory);
            
            Unbound.RegisterMenu(ModName, () => { }, NewGUI, null, false);
            
            Unbound.RegisterHandshake(ModId, OnHandShakeCompleted);
            
            Unbound.RegisterCredits(ModName,
                new[] {"FluxxField"},
                new[] {"github"},
                new[] {"https://github.com/FluxxField/FFC"});

            GameModeManager.AddHook(GameModeHooks.HookGameStart, gm => ForceClassesFirstRound());
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, gm => HandleBarret50CalAmmo());
        }

        private void NewGUI(GameObject menu) {
            MenuHandler.CreateText($"{ModName} Options", menu, out TextMeshProUGUI _);
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, 30);
            MenuHandler.CreateToggle(false, "Enable Force classes first round", menu, useClassesFirstRound => {
                _useClassesFirstRound = useClassesFirstRound;
                OnHandShakeCompleted();
            });
        }

        private IEnumerator ForceClassesFirstRound() {
            if (_useClassesFirstRound) {
                UnityEngine.Debug.Log($"[{AbbrModName}] Setting up players blacklisted categories");
                Player[] players = PlayerManager.instance.players.ToArray();
        
                foreach (Player player in players) {
                    // Blacklist Default Cards and all Upgrade Cards for all players
                    CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.AddRange(
                        new[] {
                            ManageCardCategories.DefaultCategory,
                            ManageCardCategories.MarksmanClassUpgradesCategory,
                            ManageCardCategories.LightGunnerClassUpgradesCategory,
                            ManageCardCategories.JuggernautClassUpgradesCategory,
                            ManageCardCategories.AssaultRifleUpgradeCategory,
                            ManageCardCategories.DMRUpgradeCategory,
                            ManageCardCategories.LMGUpgradeCategory,
                        }
                    );
                }
            }

            yield break;
        }

        private IEnumerator HandleBarret50CalAmmo() {
            UnityEngine.Debug.Log($"[{AbbrModName}] Setting up player categories");
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
        
        private void OnHandShakeCompleted() {
            if (PhotonNetwork.IsMasterClient) {
                NetworkingManager.RPC_Others(typeof(FFC), nameof(SyncSettings), new object[] { _useClassesFirstRound });
            }
        }

        [UnboundRPC]
        private static void SyncSettings(bool hostUseClassesStart) {
            _useClassesFirstRound = hostUseClassesStart;
        }
    }
}