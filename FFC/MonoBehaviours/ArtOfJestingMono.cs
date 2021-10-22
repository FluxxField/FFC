using UnityEngine;

namespace FFC.MonoBehaviours {
    public class ArtOfJestingMono : MonoBehaviour {
        private const float Damage = 0.02f;
        private const float MovementSpeed = 0.01f;
        private const float ProjectileSpeed = 0.01f;
        
        private Player _player;
        private CharacterStatModifiers _stats;
        private Gun _gun;
        private int _bounces;
        private int _previousBounces;
        
        private void Awake() {
            if (_player == null) {
                _player = gameObject.GetComponent<Player>();
            }

            _stats = _player.data.stats;
            _gun = _player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            _bounces = _gun.reflects;
        }

        private void Update() {
            if (_bounces == _previousBounces) {
                return;
            }

            _previousBounces = _bounces;
            
            _stats.movementSpeed += _bounces * MovementSpeed;
            _gun.damage += _bounces * Damage;
            _gun.projectileSpeed += _bounces * ProjectileSpeed;
        }
    }
}