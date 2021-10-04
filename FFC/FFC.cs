using System.Collections;
using System.Collections.Generic;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils;

namespace FFC {
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FFC : BaseUnityPlugin {
        public const string AbbrModName = "FFC";

        public const string SniperClassCategory = "Class - Sniper";

        private const string ModId = "fluxxfield.rounds.plugins.fluxxfieldscards";
        private const string ModName = "FluxxField's Cards (FFC)";
        private const string Version = "1.0.1";

        private void Awake() {
            new Harmony(ModId).PatchAll();
        }


        private void Start() {
            Unbound.RegisterCredits(ModName,
                new[] {"FluxxField"},
                new[] {"github"},
                new[] {"https://github.com/FluxxField/FFC"});

            UnityEngine.Debug.Log($"[{AbbrModName}] Building cards");
            CustomCard.BuildCard<Sniper>();
            CustomCard.BuildCard<SniperRifleExtendedMag>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");

            GameModeManager.AddHook(GameModeHooks.HookBattleStart, gm => HandleSniperAmmo());
        }

        private IEnumerator HandleSniperAmmo() {
            UnityEngine.Debug.Log($"[{AbbrModName}] Updating Sniper Ammo");

            foreach (Player currentPlayer in PlayerManager.instance.players.ToArray()) {
                List<CardInfo> currentCards = currentPlayer.data.currentCards;
                GunAmmo gunAmmo = currentPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>()
                    .GetComponentInChildren<GunAmmo>();

                bool hasSniper = false;
                bool hasExtendedSniperMag = false;

                foreach (CardInfo currentCard in currentCards) {
                    string cardName = currentCard.cardName.ToUpper();

                    if (cardName == "SNIPER") {
                        hasSniper = true;
                    }

                    if (cardName == "SNIPER RIFLE EXTENDED MAG") {
                        hasExtendedSniperMag = true;
                    }
                }

                if (hasSniper) {
                    gunAmmo.maxAmmo = 1;
                }

                if (hasSniper && hasExtendedSniperMag) {
                    gunAmmo.maxAmmo = 3;
                }
            }

            yield break;
        }
    }
}