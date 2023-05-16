using System;
using UnityEngine;

public enum Orientation {
    Up, Down, Left, Right
}

public static class OrientationExtensions {
    public static Vector2 toVector(this Orientation orientation) => orientation switch {
        Orientation.Up => Vector2.up,
        Orientation.Down => Vector2.down,
        Orientation.Left => Vector2.left,
        Orientation.Right => Vector2.right,
        _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
    };
}