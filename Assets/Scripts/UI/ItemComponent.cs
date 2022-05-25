using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ItemComponent : MonoBehaviour {
        private Image image;

        private TMP_Text text;
        private TMP_Text money;

        [SerializeField] private Item _item;

        public Item item {
            get => _item;
            set {
                _item = value;
                InvalidateItem();
            }
        }

        // Start is called before the first frame update
        private void Start() {
            image = transform.Find("Image").GetComponent<Image>();
            text = transform.Find("Text").GetComponent<TMP_Text>();
            money = transform.Find("Money").GetComponent<TMP_Text>();
            InvalidateItem();
        }
    
    

        private void InvalidateItem() {
            if (_item != null && _item.icon != null && image != null) {
                image.sprite = Sprite.Create(_item.icon, new Rect(0, 0, _item.icon.width, _item.icon.height), Vector2.zero);
                text.text = _item.itemName;
                money.text = $"${_item.cost}";
            }
        }
    }
}
