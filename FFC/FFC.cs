using System.Collections.Generic;
using BepInEx;
using FFC.Cards.BloodMage;
using FFC.Cards.Jester;
using FFC.Cards.Juggernaut;
using FFC.Cards.LightGunner;
using FFC.Cards.Marksman;
using FFC.Extensions;
using HarmonyLib;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;

namespace FFC {
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    [BepInDependency("fluxxfield.rounds.plugins.classesmanager")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FFC : BaseUnityPlugin {
        public const string AbbrModName = "FFC";

        private const string ModId = "fluxxfield.rounds.plugins.fluxxfieldscards";
        private const string ModName = "FluxxField's Cards (FFC)";
        private const string Version = "4.4.2";

        public const string Marksman = "Marksman";
        public const string LightGunner = "Light Gunner";
        public const string Juggernaut = "Juggernaut";
        public const string Jester = "Jester";
        public const string BloodMage = "Blood Mage";
        public const string AssaultRifle = "Assault Rifle";
        public const string Dmr = "DMR";
        public const string Lmg = "LMG";
        public const string Barret50Cal = "Barret .50 Cal";
        public const string KingOfFoolsCategory = "King of Fools";

        private void Awake() {
            new Harmony(ModId).PatchAll();
        }


        private void Start() {
            ClassesManager.ClassesManager.Instance.AddClassProgressionCategories(new List<string> {
                Marksman,
                LightGunner,
                Juggernaut,
                Jester,
                BloodMage,
                AssaultRifle,
                Dmr,
                Lmg,
                Barret50Cal,
                KingOfFoolsCategory
            });

            // Marksman Class
            CustomCard.BuildCard<MarksmanClass>();
            CustomCard.BuildCard<SniperRifleExtendedMag>();
            CustomCard.BuildCard<Barret50Cal>();
            CustomCard.BuildCard<ArmorPiercingRounds>();
            // Light Gunner Class
            CustomCard.BuildCard<LightGunnerClass>();
            CustomCard.BuildCard<AssaultRifle>();
            CustomCard.BuildCard<Dmr>();
            CustomCard.BuildCard<Lmg>();
            CustomCard.BuildCard<FastMags>();
            CustomCard.BuildCard<BattleExperience>();
            // Juggernaut Class
            CustomCard.BuildCard<JuggernautClass>();
            CustomCard.BuildCard<SizeMatters>();
            CustomCard.BuildCard<Conditioning>();
            // Jester Class
            CustomCard.BuildCard<JesterClass>();
            CustomCard.BuildCard<WayOfTheJester>();
            CustomCard.BuildCard<KingOfFools>();
            CustomCard.BuildCard<JokesOnYou>();
            //Blood Mage Class
            // CustomCard.BuildCard<BloodMage>();

            Unbound.RegisterCredits(ModName,
                new[] {"FluxxField"},
                new[] {"github"},
                new[] {"https://github.com/FluxxField/FFC"});

            GameModeManager.AddHook(GameModeHooks.HookPointStart, SizeMatters.SetPrePointStats);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, CharacterStatModifiersExtension.Reset);
        }
    }
}