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
            if (Item != null && Item.icon != null && image != null) {
                image.sprite = Sprite.Create(Item.icon, new Rect(0, 0, Item.icon.width, Item.icon.height), Vector2.zero);
                text.text = Item.itemName;
                money.text = $"${Item.cost}";
            }
        }
    }
}
