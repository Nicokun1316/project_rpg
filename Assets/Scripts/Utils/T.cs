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

        public static String Colorize(this String str, GColor color) => $"<{Color(color)}>{str}</color>";

        public static String Rep(this Skill skill) => skill.skillName.ToUpper().Colorize(GColor.Skill);
        public static String Rep(this MenuEnum menu) => menu.toString().Colorize(GColor.MenuRef);
        public static String Rep(this Item item) => item == null ? "xxx" : item.itemName.ToUpper().Colorize(GColor.Item);

    }

    public enum Color {
        Purple, Red, Green, Golden, Blue
    }

    public enum GColor {
        Skill, Item, Money, GoodStat, BadStat, MenuRef
    }
}
