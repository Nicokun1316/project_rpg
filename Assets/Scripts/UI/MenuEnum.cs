using System;

namespace UI {
    public enum MenuEnum {
        Equipment, Skills, Items, System
    }

    public static class MenuEnumExtensions {
        public static String toString(this MenuEnum item) => item switch {
            MenuEnum.Equipment => "Equipment",
            MenuEnum.Skills => "Skills",
            MenuEnum.Items => "Items",
            MenuEnum.System => "System",
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
        };
    }
}
