using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Kyara {
    [Serializable]
    public class Character {
        [SerializeField] private CharacterProgressionStats _statBlock;
        [SerializeField] private String _characterName;
        [SerializeField] private Equipment _equipment;
        [SerializeField] private int _level;
        [SerializeField] private int _experience;

        public Equipment equipment => _equipment;
        public int level => _level;
        public int experience => _experience;
        public int experienceTilNextLevel => _statBlock.statProgression[level + 1].requiredXP - experience;
        public String characterName => _characterName;
        public CharacterStatBlock currentStats => _statBlock.statProgression[level];
        
        public bool AddExperience(int exp) {
            _experience += exp;
            var i = _statBlock.statProgression.FindIndex(b => b.requiredXP > _experience) - 1;
            if (i < 0) i = _statBlock.statProgression.Count - 1;
            if (_level != i) {
                _level = i;
                return true;
            }

            return false;
        }

        public void Equip(EquippableItem item, EquipmentSlot slot) {
            switch (item.type, slot) {
                case (EquippableType.Armour, EquipmentSlot.Armour):
                    _equipment.armour = item;
                    break;
                case (EquippableType.Weapon, EquipmentSlot.Weapon):
                    _equipment.weapon = item;
                    break;
                case (EquippableType.Trinket, EquipmentSlot.LeftTrinket):
                    _equipment.leftTrinket = item;
                    break;
                case (EquippableType.Trinket, EquipmentSlot.RightTrinket):
                    _equipment.rightTrinket = item;
                    break;
            }
        }
    }
}
