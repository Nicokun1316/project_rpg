using System;
using System.Collections.Generic;

namespace Items {
    public static class ItemExtensions {
        public static List<Stat> EnumerateStats(this Item item) {
            var stats = new List<Stat>();
            if (item is EquippableItem eqItem) {
                stats.AddNonZero(StatType.Attack, eqItem.attack);

                stats.AddNonZero(StatType.Defense, eqItem.defense);

                stats.AddNonZero(StatType.Health, eqItem.health);

                stats.AddNonZero(StatType.Magic, eqItem.magic);

                stats.AddNonZero(StatType.Luck, eqItem.luck);
                stats.AddNonZero(StatType.Speed, eqItem.speed);
            } else if (item is UtilityItem conItem) {
                stats.AddNonZero(StatType.Health, conItem.health);
                stats.AddNonZero(StatType.Magic, conItem.magic);
            }
            return stats;
        }

        private static void AddNonZero(this List<Stat> stats, StatType name, int value) {
            if (value != 0) {
                stats.Add(new Stat(name, value));
            }
        }
    }
}
