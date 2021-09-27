using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using BepInEx;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Cards;
using UnboundLib.Utils;
using FluxxFieldsCards.Cards;
using HarmonyLib;
using Photon.Pun;

namespace FluxxFieldsCards
{
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]

    public class FluxxFieldsCards : BaseUnityPlugin
    {
        private const string ModId = "com.fluxxfieldcards.rounds.card";
        private const string ModName = "FluxxFields Cards";
        public const string Version = "1.0.1"; // What version are we on (major.minor.patch)?

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            UnityEngine.Debug.Log("[FFC] Loading Cards");
            CustomCard.BuildCard<ExtendedMag>();
            UnityEngine.Debug.Log("[FFC] Cards Successfully Build");
        }
    }
}