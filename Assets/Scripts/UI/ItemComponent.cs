using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ItemComponent : GenericItemComponent<Item> {
        private Image image;

        private TMP_Text text;
        private TMP_Text money;
        private void Start() {
            image = transform.Find("Image").GetComponent<Image>();
            text = transform.Find("Text").GetComponent<TMP_Text>();
            money = transform.Find("Money").GetComponent<TMP_Text>();
            InvalidateItem();
        }
    
    

        protected override void InvalidateItem() {
            if (item != null && item.icon != null && image != null) {
                image.sprite = Sprite.Create(item.icon, new Rect(0, 0, item.icon.width, item.icon.height), Vector2.zero);
                text.text = item.itemName;
                money.text = $"${item.cost}";
            }
        }
    }
}
