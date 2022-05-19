using System;
using System.Collections.Generic;

namespace Items {
    public static class ItemExtensions {
        public static List<Stat> EnumerateStats(this Item item) {
            var stats = new List<Stat>();
            if (item is EquippableItem eqItem) {
                stats.AddNonZero("Attack", eqItem.attack);

                stats.AddNonZero("Defense", eqItem.defense);

                stats.AddNonZero("Health", eqItem.health);

                stats.AddNonZero("Magic", eqItem.magic);

                stats.AddNonZero("Luck", eqItem.luck);
                stats.AddNonZero("Speed", eqItem.speed);
            } else if (item is ConsumableItem conItem) {
                stats.AddNonZero("Health", conItem.health);
                stats.AddNonZero("Magic", conItem.magic);
            }
            return stats;
        }

        private static void AddNonZero(this List<Stat> stats, String name, int value) {
            if (value != 0) {
                stats.Add(new Stat(name, value));
            }
        }
    }
}
