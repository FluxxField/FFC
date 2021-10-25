using UnityEngine;

namespace FFC.Utilities {
    public class PreventRecursion {
        private static GameObject _stopRecursion = null;

        private static GameObject stopRecursion {
            get {
                if (_stopRecursion != null) {
                    return _stopRecursion;
                } else {
                    _stopRecursion = new GameObject("StopRecursion", typeof(StopRecursion),
                        typeof(DestroyOnUnparentAfterInitialized));
                    GameObject.DontDestroyOnLoad(_stopRecursion);

                    return _stopRecursion;
                }
            }
            set { }
        }

        internal static ObjectsToSpawn stopRecursionObjectToSpawn {
            get {
                ObjectsToSpawn obj = new ObjectsToSpawn() { };
                obj.AddToProjectile = stopRecursion;

                return obj;
            }
            set { }
        }
    }

    public class DestroyOnUnparentAfterInitialized : MonoBehaviour {
        private static bool initialized = false;
        private bool isOriginal = false;

        void Start() {
            if (!DestroyOnUnparentAfterInitialized.initialized) {
                this.isOriginal = true;
            }
        }

        void LateUpdate() {
            if (this.isOriginal) {
                return;
            } else if (this.gameObject.transform.parent == null) {
                UnityEngine.GameObject.Destroy(this.gameObject);
            }
        }
    }
}