using ModdingUtils.RoundsEffects;
using UnboundLib;
using UnityEngine;

namespace FFC.HitEffects {
    public class JokesOnYouHitEffect : HitEffect {
        public override void DealtDamage(
            Vector2 damage,
            bool selfDamage,
            Player damagedPlayer = null
        ) {
            if (!selfDamage || damagedPlayer == null || damagedPlayer != null && damagedPlayer.teamID != gameObject.GetComponent<Player>().teamID) {
                return;
            }
            
            if (damagedPlayer.data.health - damage.magnitude <= 0f) {
                damagedPlayer.data.healthHandler.Heal(damage.magnitude);
            } else {
                Unbound.Instance.ExecuteAfterFrames(2, () => damagedPlayer.data.healthHandler.Heal(damage.magnitude));
            }
        }
    }
}