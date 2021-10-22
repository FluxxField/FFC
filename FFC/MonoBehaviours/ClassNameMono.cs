using System.Linq;
using TMPro;
using UnityEngine;

namespace FFC.MonoBehaviours {
    // destroy object once its no longer a child
    public class DestroyOnUnParent : MonoBehaviour
    {
        void LateUpdate()
        {
            if (gameObject.transform.parent == null) {
                Destroy(gameObject);
            }
        }
    }
    
    public class ClassNameMono : MonoBehaviour {
        private void Start() {
            var card = gameObject.GetComponent<CardInfo>();
            var allChildrenRecursive = gameObject.GetComponentsInChildren<RectTransform>();
            var BottomLeftCorner = allChildrenRecursive.Where(obj => obj.gameObject.name == "EdgePart (1)").FirstOrDefault().gameObject;
            var modNameObj = Instantiate(new GameObject("ExtraCardText", typeof(TextMeshProUGUI), typeof(DestroyOnUnParent)), BottomLeftCorner.transform.position, BottomLeftCorner.transform.rotation, BottomLeftCorner.transform);
            var modText = modNameObj.gameObject.GetComponent<TextMeshProUGUI>();
            
            modText.text = card.categories[0].name;
            modText.enableWordWrapping = false;
            modText.alignment = TextAlignmentOptions.Bottom;
            modText.alpha = 0.1f;
            modText.fontSize = 50;
            
            modNameObj.transform.Rotate(0f, 0f, 135f);
            modNameObj.transform.localScale = new Vector3(1f, 1f, 1f);
            modNameObj.transform.localPosition = new Vector3(-50f, -50f, 0f);
        }
    }
}