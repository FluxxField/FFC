using System.Collections.Generic;
using FFC.MonoBehaviours;
using ModdingUtils.RoundsEffects;
using UnboundLib;
using UnityEngine;
using Random = System.Random;

namespace FFC.HitEffects {
    public class KingOfFoolsGun : Gun {
    }

    public class KingOfFoolsHitSurfaceEffect : HitSurfaceEffect {
        private readonly Random _rng = new Random();
        private Gun _gun;
        private Player _player;

        public override void Hit(
            Vector2 position,
            Vector2 normal,
            Vector2 velocity
        ) {
            var role = _rng.Next(1, 101);

            // 15% chance
            if (role > 15) return;

            _player = gameObject.GetComponent<Player>();
            _gun = _player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            var newGun = _player.gameObject.GetOrAddComponent<KingOfFoolsGun>();
            var effect = _player.gameObject.GetOrAddComponent<SpawnBulletsEffect>();
            var parallel = ((Vector2) Vector3.Cross(Vector3.forward, normal)).normalized;
            var positions = GetPositions(position, normal, parallel);
            var directions = GetDirections(position, positions);

            effect.SetPositions(positions);
            effect.SetDirections(directions);
            effect.SetNumBullets(1);
            effect.SetTimeBetweenShots(0f);
            effect.SetInitialDelay(0f);

            SpawnBulletsEffect.CopyGunStats(_gun, newGun);

            newGun.spread = 0.2f;
            newGun.destroyBulletAfter = 5f;
            newGun.numberOfProjectiles = 1;

            effect.SetGun(newGun);
        }

        private List<Vector3> GetPositions(
            Vector2 position,
            Vector2 normal,
            Vector2 parallel
        ) {
            var res = new List<Vector3>();

            for (var i = 0; i < 5; i++)
                res.Add(position + 0.2f * normal + 0.1f * (float) _rng.NextGaussianDouble() * parallel);

            return res;
        }

        private static List<Vector3> GetDirections(
            Vector2 position,
            List<Vector3> shootPos
        ) {
            var res = new List<Vector3>();

            foreach (var shootPosition in shootPos) res.Add(((Vector2) shootPosition - position).normalized);

            return res;
        }
    }
}