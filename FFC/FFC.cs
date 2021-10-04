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

        public const string SniperClassCategory = "Sniper";
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
            CustomCard.BuildCard<FiftyCaliber>();
            CustomCard.BuildCard<ArmorPiercingRounds>();
            UnityEngine.Debug.Log($"[{AbbrModName}] Done building cards");
        }
    }
}