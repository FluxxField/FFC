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
        public int extendedMags;
        public int kingOfFools;

        public CharacterStatModifiersAdditionalData() {
            hasAdaptiveSizing = false;
            adaptiveMovementSpeed = 0f;
            adaptiveGravity = 0f;
            extendedMags = 1;
            kingOfFools = 0;
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

        internal static IEnumerator Reset(
            IGameModeHandler gm
        ) {
            foreach (var player in PlayerManager.instance.players) {
                var additionalData = player.data.stats.GetAdditionalData();

                if (additionalData.hasAdaptiveSizing) {
                    var adaptiveSizingMono = player.gameObject.GetComponent<SizeMattersMono>();
                    var characterStatsModifiers = player.gameObject.GetComponent<CharacterStatModifiers>();

                    additionalData.adaptiveMovementSpeed = 0f;
                    additionalData.adaptiveGravity = 0f;
                    adaptiveSizingMono.Reset();
                    characterStatsModifiers.Invoke("ConfigureMassAndSize", 0f);
                }
            }

            yield break;
        }

        [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
        private class CharacterStatModifiersPatchResetStats {
            private static void Prefix(
                CharacterStatModifiers __instance
            ) {
                var additionalData = __instance.GetAdditionalData();
                additionalData.adaptiveMovementSpeed = 0f;
                additionalData.adaptiveGravity = 0f;
                additionalData.hasAdaptiveSizing = false;
                additionalData.extendedMags = 1;
                additionalData.kingOfFools = 0;
            }
        }
    }
}