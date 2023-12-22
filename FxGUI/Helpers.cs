using OpenTK.Platform;
using System.Globalization;

namespace FxGUI
{
    public static class Helpers
    {
        public static float ToFloat(this string str)
        {
            if (str.EndsWith("f"))
            {
                str = str.Replace("f", "");
            }
            return float.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
        }
    }
}
