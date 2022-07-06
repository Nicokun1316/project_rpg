using System.Collections.Generic;
using Items;
using Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Persist {
    public class ResourceIndex : ScriptableObject {
        private static Dictionary<int, Item> Items;
        private static Dictionary<int, Skill> Skills;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Init() {
            Items = new Dictionary<int, Item>();
            Skills = new Dictionary<int, Skill>();
            
            var items = Addressables.LoadAssetsAsync<Item>("item", null).WaitForCompletion();
            foreach (var item in items) {
                Items.Add(item.id, item);
            }

            var skills = Addressables.LoadAssetsAsync<Skill>("skill", null).WaitForCompletion();
            foreach (var skill in skills) {
                Skills.Add(skill.id, skill);
            }
        }

        public static Item GetItem(int id) => Items[id];
        public static Skill GetSkill(int id) => Skills[id];
    }

}
