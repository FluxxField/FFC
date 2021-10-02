using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils;
using UnityEngine;

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
            CustomCard.BuildCard<ExtendedMag>();
            CustomCard.BuildCard<InvisibleBullets>();
            CustomCard.BuildCard<Sniper>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");

            GameModeManager.AddHook(GameModeHooks.HookBattleStart, gm => UpdateSniperAmmo());
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, gm => UpdateBulletColor());
        }

        private IEnumerator UpdateSniperAmmo() {
            foreach (Player currentPlayer in PlayerManager.instance.players.ToArray()) {
                foreach (CardInfo currentPlayersCard in currentPlayer.data.currentCards) {
                    if (currentPlayersCard.cardName.ToUpper() == "SNIPER!") {
                        GunAmmo gunAmmo = currentPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>()
                            .GetComponentInChildren<GunAmmo>();
                        gunAmmo.maxAmmo = 1;
                    }
                }
            }

            yield break;
        }

        private IEnumerator UpdateBulletColor() {
            foreach (Player currentPlayer in PlayerManager.instance.players.ToArray()) {
                foreach (CardInfo currentPlayersCard in currentPlayer.data.currentCards) {
                    Gun gun = currentPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>();
                    if (currentPlayersCard.cardName.ToUpper() == "INVISIBLE BULLETS") {
                        gun.projectileColor = Color.clear;
                    }
                    break;
                }
            }

            yield break;
        }
    }
}