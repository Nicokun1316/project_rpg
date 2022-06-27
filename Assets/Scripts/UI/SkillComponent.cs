using System;
using Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SkillComponent : GenericItemComponent<Skill> {
        private Image icon;
        private TMP_Text skillName;
        private TMP_Text description;

        private void Start() {
            icon = transform.Find("Image").GetComponent<Image>();
            skillName = transform.Find("Text").GetComponent<TMP_Text>();
            description = transform.Find("Description").GetComponent<TMP_Text>();
            InvalidateItem();
        }

        protected override void InvalidateItem() {
            if (Item != null && icon != null) {
                icon.sprite = Sprite.Create(Item.icon, new Rect(0, 0, Item.icon.width, Item.icon.height), Vector2.zero);
                skillName.text = Item.skillName;
                description.text = $"{Item.description/*[..36]*/}...";
                //description.maxVisibleCharacters = 36;
            } 
        }
    }
}
