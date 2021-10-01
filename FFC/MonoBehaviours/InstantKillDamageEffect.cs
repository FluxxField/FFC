using System.Reflection;
using ModdingUtils.RoundsEffects;
using UnityEngine;
using UnboundLib;

namespace FFC.MonoBehaviours {
    public class InstantKillHitEffect : HitEffect {
        public override void DealtDamage(
            Vector2 damage,
            bool selfDamage,
            Player damagedPlayer = null
        ) {
            if (damagedPlayer == null) {
                return;
            }

            Unbound.Instance.ExecuteAfterSeconds(0f, () => {
                typeof(HealthHandler).InvokeMember(
                    "RPCA_Die",
                    BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                    null,
                    damagedPlayer.data.healthHandler,
                    new object[] {new Vector2(0, 1)}
                );
            });
        }
    }
} 