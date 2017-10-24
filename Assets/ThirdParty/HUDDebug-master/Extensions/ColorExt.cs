using UnityEngine;
using System.Collections;

namespace Playflock.Extension
{

    public static class ColorPF
    {
        public static string ToHex(Color color)
        {
            Color32 color32 = color;
            string htmlColor = "#" +
                                color32.r.ToString("X2") +
                                color32.g.ToString("X2") +
                                color32.b.ToString("X2");
            return htmlColor;
        }
    }

}
