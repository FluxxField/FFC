using BepInEx; // requires BepInEx.dll and BepInEx.Harmony.dll
using UnboundLib; // requires UnboundLib.dll
using UnboundLib.Cards; // " "
using UnityEngine; // requires UnityEngine.dll, UnityEngine.CoreModule.dll, and UnityEngine.AssetBundleModule.dll
using HarmonyLib; // requires 0Harmony.dll
using System.Collections;
using Photon.Pun;
using Jotunn.Utils;
using UnboundLib.GameModes;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnboundLib.Networking;
using UnboundLib.Utils;

// requires Assembly-CSharp.dll
// requires MMHOOK-Assembly-CSharp.dll

namespace FFC
{
    public class FFC : BaseUnityPlugin
    {
        private const string ModId = "fluxxfield.rounds.plugins.fluxxfieldscards";
        private const string ModName = "FluxxField's Cards (FFC)";

        private void Awake()
        {
            new Harmony(ModId).PatchAll();
        }

        private void Start()
        {
            Unbound.RegisterCredits(ModName, new string[] { "FluxxField" }, new string[] { "github" }, new string[] { "https://github.com/FluxxField/FFC" });
        }
    }
}
