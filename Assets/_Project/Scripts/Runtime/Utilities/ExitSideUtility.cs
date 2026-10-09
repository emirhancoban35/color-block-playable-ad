using UnityEngine;
using Data.Core;

namespace Utilities
{
    public static class ExitSideUtility
    {
        public static Vector2Int ToDirection(ExitSide side)
        {
            switch (side)
            {
                case ExitSide.Top: return Vector2Int.up;
                case ExitSide.Bottom: return Vector2Int.down;
                case ExitSide.Left: return Vector2Int.left;
                case ExitSide.Right: return Vector2Int.right;
                default: return Vector2Int.zero;
            }
        }

        public static bool IsHorizontalSide(ExitSide side)
        {
            return side == ExitSide.Top || side == ExitSide.Bottom;
        }

        public static bool IsVerticalSide(ExitSide side)
        {
            return side == ExitSide.Left || side == ExitSide.Right;
        }

        public static int GetSideLength(ExitSide side, int width, int height)
        {
            return IsHorizontalSide(side) ? width : height;
        }
    }
}
