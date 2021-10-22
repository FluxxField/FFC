using FFC.Extensions;
using UnityEngine;

namespace FFC.MonoBehaviours {
    public class AdaptiveSizingMono : MonoBehaviour {
        public float prePointMaxHealth;
        public float prePointGravity;
        public float prePointSizeMultiplier;
        
        private Player _player;
        private CharacterStatModifiers _characterStatModifiers;
        
        private float _lastHealth;
        private float _maxAdditionalMovementSpeed;
        private float _maxAdditionalGravity;

        private void Awake() {
            if (_player == null) {
                _player = gameObject.GetComponent<Player>();
            }

            if (_characterStatModifiers == null) {
                _characterStatModifiers = _player.GetComponent<CharacterStatModifiers>();
            }

            var data = _player.data;
            var additionalData = _player.data.stats.GetAdditionalData();

            prePointMaxHealth = data.health;
            prePointGravity = data.stats.gravity;
            prePointSizeMultiplier = data.stats.sizeMultiplier;
            _lastHealth = data.health;
            
            _maxAdditionalMovementSpeed = additionalData.adaptiveMovementSpeed;
            _maxAdditionalGravity = additionalData.adaptiveGravity;
        }

        private void Update() {
            var maxHealth = _player.data.maxHealth;
            var currentHealth = _player.data.health;

            // Health hasn't changed since last check
            if (_lastHealth == currentHealth) {
                return;
            }

            _lastHealth = _player.data.health;

            var healthDelta = currentHealth / maxHealth;

            if (healthDelta > 0.25f) {
                var movementSpeedDelta = _maxAdditionalMovementSpeed * healthDelta;
                var gravityDelta = _maxAdditionalGravity * healthDelta;

                _characterStatModifiers.sizeMultiplier *= Mathf.Max(healthDelta);
                _characterStatModifiers.movementSpeed += movementSpeedDelta;
                _characterStatModifiers.gravity -= gravityDelta;
                _characterStatModifiers.Invoke("ConfigureMassAndSize", 0f);   
            }
        }

        public void SetPrePointStats(CharacterData data) {
            var stats = data.stats;
            
            prePointMaxHealth = data.maxHealth;
            prePointGravity = stats.gravity;
            prePointSizeMultiplier = stats.sizeMultiplier;
        }

        public void Reset() {
            _characterStatModifiers.health = prePointMaxHealth;
            _characterStatModifiers.gravity = prePointGravity;
            _characterStatModifiers.sizeMultiplier = prePointSizeMultiplier;
            _characterStatModifiers.Invoke("ConfigureMassAndSize", 0f);
        }
    }
}