using UnityEngine;

namespace FFC.MonoBehaviours {
    public class WayOfTheJesterMono : MonoBehaviour {
        private const float Damage = 0.05f;
        private const float MovementSpeed = 0.01f;
        private const float ProjectileSpeed = 0.03f;
        private int _bounces;
        private Gun _gun;

        private Player _player;
        private int _previousBounces;
        private CharacterStatModifiers _stats;

        private void Awake() {
            if (_player == null) _player = gameObject.GetComponent<Player>();

            _stats = _player.data.stats;
            _gun = _player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            _bounces = _gun.reflects;
        }

        private void Update() {
            if (_bounces == _previousBounces || _bounces > 25) return;

            UnityEngine.Debug.Log($"[FFC] _bounces: {_bounces}");

            _previousBounces = _bounces;

            _stats.movementSpeed += _bounces * MovementSpeed;
            _gun.damage += _bounces * Damage;
            _gun.projectileSpeed += _bounces * ProjectileSpeed;
        }
    }
}