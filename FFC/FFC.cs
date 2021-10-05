using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using ModdingUtils.Extensions;
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

        public static CardCategory DefaultCategory;
        public static CardCategory MainClassesCategory;
        public static CardCategory SniperClassUpgradesCategory;
        public static CardCategory LightGunnerClassUpgradesCategory;

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

            // Gotta give CustomCardCategories a sec to setup
            if (CustomCardCategories.instance != null) {
                DefaultCategory = CustomCardCategories.instance.CardCategory("Default");
                MainClassesCategory = CustomCardCategories.instance.CardCategory("MainClasses");
                SniperClassUpgradesCategory = CustomCardCategories.instance.CardCategory("SniperUpgrades");
                LightGunnerClassUpgradesCategory = CustomCardCategories.instance.CardCategory("LightGunnerUpgrades");
            }

            UnityEngine.Debug.Log($"[{AbbrModName}] Building cards");
            CustomCard.BuildCard<Sniper>();
            CustomCard.BuildCard<SniperRifleExtendedMag>();
            CustomCard.BuildCard<Barret50Cal>();
            CustomCard.BuildCard<ArmorPiercingRounds>();
            CustomCard.BuildCard<LightGunner>();
            CustomCard.BuildCard<FastMags>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");

            this.ExecuteAfterSeconds(0.4f, HandleBuildDefaultCategory);

            GameModeManager.AddHook(GameModeHooks.HookGameStart, gm => HandlePlayersBlacklistedCategories());
        }

        private void HandleBuildDefaultCategory() {
            foreach (Card card in CardManager.cards.Values.ToList()) {
                List<CardCategory> categories = card.cardInfo.categories.ToList();

                if (categories.Count == 0 || card.category != "FFC") {
                    categories.Add(DefaultCategory);
                    card.cardInfo.categories = categories.ToArray();
                }
            }
        }

        private IEnumerator HandlePlayersBlacklistedCategories() {
            UnityEngine.Debug.Log($"[{AbbrModName}] Setting up player categories");
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach (Player player in players) {
                CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.AddRange(
                    new[] {
                        DefaultCategory,
                        SniperClassUpgradesCategory,
                        LightGunnerClassUpgradesCategory
                    }
                );
            }

            UnityEngine.Debug.Log($"[{AbbrModName}] Dont setting up player categories");
            yield break;
        }
    }

    [HarmonyPatch]
    class Patches {
        [HarmonyPatch(typeof(CharacterStatModifiers), "DealtDamage")]
        [HarmonyPostfix]
        static void DealtDamage_Postfix(
            Vector2 damage,
            bool selfDamage,
            Player damagedPlayer
        ) {
            UnityEngine.Debug.Log(
                $"[{FFC.AbbrModName}] Player {damagedPlayer.playerID + 1} took {damage.magnitude} damage"
            );
        }
    }
}