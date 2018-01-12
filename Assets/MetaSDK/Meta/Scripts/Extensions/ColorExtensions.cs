using UnityEngine;

namespace Meta.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Get a color with only the alpha value changed
        /// </summary>
        /// <param name="color">Target color</param>
        /// <param name="alpha">Target alpha</param>
        /// <returns></returns>
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}