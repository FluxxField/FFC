using FFC.Extensions;
using UnityEngine;

namespace FFC.MonoBehaviours {
    public class Barret50CalMono : MonoBehaviour {
        private Player _player;

        private void Awake() {
            if (_player == null) _player = gameObject.GetComponent<Player>();
        }

        private void Update() {
            var extendedMags = _player.data.stats.GetAdditionalData().extendedMags;
            gameObject.GetComponent<Holding>().holdable.GetComponent<Gun>().GetComponentInChildren<GunAmmo>().maxAmmo =
                extendedMags;
        }
    }
}