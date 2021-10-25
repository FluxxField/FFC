using FFC.Extensions;
using HarmonyLib;
using UnityEngine;

namespace FFC.Patches {
    [HarmonyPatch(typeof(Gun), "Attack")]
    public class BloodMageAttackCostsHealthPatch {
        static void Prefix(Gun __instance) {
            var player = __instance.player;
            var data = player.data;
            var additionalData = data.stats.GetAdditionalData();
            
            if (additionalData.isBloodMage) {
                // data.healthHandler.TakeDamage(new Vector2());
            }
        }
    }
}