using System;
using Items;
using Skills;
using UI;

namespace Utils {
    public static class T {
        public static String Color(Color color) => color switch {
            Utils.Color.Purple => "color=\"purple\"",
            Utils.Color.Red => "color=\"red\"",
            Utils.Color.Green => "color=\"green\"",
            Utils.Color.Golden => "color=\"yellow\"",
            Utils.Color.Blue => "color=\"blue\"",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

        public static String Color(GColor color) => color switch {
            GColor.Skill => Color(Utils.Color.Purple),
            GColor.Item => Color(Utils.Color.Golden),
            GColor.Money => Color(Utils.Color.Golden),
            GColor.GoodStat => Color(Utils.Color.Green),
            GColor.BadStat => Color(Utils.Color.Red),
            GColor.MenuRef => Color(Utils.Color.Blue),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

        public static String Rep(Skill skill) => $"<{Color(GColor.Skill)}>{skill.skillName.ToUpper()}</color>";
        public static String Rep(MenuEnum menu) => $"<{Color(GColor.MenuRef)}>{menu.toString()}</color>";
        public static String Rep(Item item) => $"<{Color(GColor.Item)}>{item.itemName.ToUpper()}</color>";

    }

    public enum Color {
        Purple, Red, Green, Golden, Blue
    }

    public enum GColor {
        Skill, Item, Money, GoodStat, BadStat, MenuRef
    }
}
