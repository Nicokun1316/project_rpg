using System;
using Items;
using UnityEngine;

namespace Kyara {
    public class Character : MonoBehaviour {
        [SerializeField] private CharacterStatBlock statBlock;
        [SerializeField] private int level;
        [SerializeField] private String characterName;
        [SerializeField] private Equipment equipment;
        [SerializeField] private int experience;
    }
}
