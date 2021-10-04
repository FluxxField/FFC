using System.Collections;
using System.Linq;
using System.Net.WebSockets;
using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;
using ModdingUtils.Extensions;
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
        public const string MainClassesCategory = "MainClasses";
        public const string SniperClassUpgradesCategory = "SniperUpgrades";

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
            CustomCard.BuildCard<Barret50Cal>();
            CustomCard.BuildCard<ArmorPiercingRounds>();
            CustomCard.BuildCard<FastMags>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");
            
            GameModeManager.AddHook(GameModeHooks.HookGameStart, gm => HandleCardCategoriesSetup());
        }
        
        private IEnumerator HandleCardCategoriesSetup() {
            UnityEngine.Debug.Log($"[{AbbrModName}] Setting up player categories");
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach (Player player in players) {
                CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(
                    CustomCardCategories.instance.CardCategory(SniperClassUpgradesCategory)
                );
            }
            UnityEngine.Debug.Log($"[{AbbrModName}] Dont setting up player categories");
            yield break;
        }
    }
}