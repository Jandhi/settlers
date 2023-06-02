using SadRogue.Primitives;

namespace SettlersSharp.Utils
{
    public static class ColorUtils
    {
        public static string ToForegroundTag(Color color)
        {
            return $"[c:r f:{color.R},{color.G},{color.B},{color.A}]";
        }

        public static string ToBackgroundTag(Color color)
        {
            return $"[c:r b:{color.R},{color.G},{color.B},{color.A}]";
        }
    }
}