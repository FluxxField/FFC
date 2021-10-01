using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using FFC.Cards;

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

            CustomCardCategories.instance.CardCategory(SniperClassCategory);
        }
    }
}