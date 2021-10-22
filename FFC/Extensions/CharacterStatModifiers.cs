using System;
using System.Collections;
using System.Runtime.CompilerServices;
using FFC.MonoBehaviours;
using HarmonyLib;
using UnboundLib.GameModes;

namespace FFC.Extensions {
    [Serializable]
    public class CharacterStatModifiersAdditionalData {
        public bool hasAdaptiveSizing;
        public float adaptiveMovementSpeed;
        public float adaptiveGravity;
        public int kingOfFoolsBullets;
        public int extendedMags;

        public CharacterStatModifiersAdditionalData() {
            hasAdaptiveSizing = false;
            adaptiveMovementSpeed = 0f;
            adaptiveGravity = 0f;
            kingOfFoolsBullets = 0;
            extendedMags = 1;
        }
    }


    public static class CharacterStatModifiersExtension {
        public static readonly
            ConditionalWeakTable<CharacterStatModifiers,
                CharacterStatModifiersAdditionalData> Data =
                new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(
            this CharacterStatModifiers statModifiers
        ) {
            return Data.GetOrCreateValue(statModifiers);
        }

        public static void AddData(
            this CharacterStatModifiers statModifiers,
            CharacterStatModifiersAdditionalData value
        ) {
            try {
                Data.Add(statModifiers, value);
            }
            catch (Exception) {
            }
        }

        [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
        class CharacterStatModifiersPatchResetStats {
            private static void Prefix(CharacterStatModifiers __instance) {
                var additionalData = __instance.GetAdditionalData();
                additionalData.adaptiveMovementSpeed = 0f;
                additionalData.adaptiveGravity = 0f;
                additionalData.hasAdaptiveSizing = false;
                additionalData.kingOfFoolsBullets = 0;
                additionalData.extendedMags = 1;
            }
        }

        internal static IEnumerator Reset(
            IGameModeHandler gm
        ) {
            foreach (var player in PlayerManager.instance.players) {
                var additionalData = player.data.stats.GetAdditionalData();

                if (additionalData.hasAdaptiveSizing) {
                    var adaptiveSizingMono = player.gameObject.GetComponent<AdaptiveSizingMono>();
                    var characterStatsModifiers = player.gameObject.GetComponent<CharacterStatModifiers>();
                    
                    additionalData.adaptiveMovementSpeed = 0f;
                    additionalData.adaptiveGravity = 0f;
                    adaptiveSizingMono.Reset();
                    characterStatsModifiers.Invoke("ConfigureMassAndSize", 0f);
                }
            }
            
            yield break;
        }
    }
}