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
            if (item != null && icon != null) {
                icon.sprite = Sprite.Create(item.icon, new Rect(0, 0, item.icon.width, item.icon.height), Vector2.zero);
                skillName.text = item.skillName;
                description.text = $"{item.description/*[..36]*/}...";
                //description.maxVisibleCharacters = 36;
            } 
        }
    }
}
