using System;
using System.Drawing;
using System.Text;

namespace Sukasa.ExtensionFunctions
{
    public static class Extensions
    {
        public static string Join(this string[] Source, string Delimiter)
        {
            StringBuilder SB = new StringBuilder(Source[0]);
            for (int i = 1; i < Source.Length; i++)
            {
                SB.Append(Delimiter);
                SB.Append(Source[i]);
            }
            return SB.ToString();
        }

        public static Point Turn(this Point Source, int AngleDegrees)
        {
            while (AngleDegrees < 0)
                AngleDegrees += 360;
            switch(AngleDegrees % 360)
            {
                case 90:
                    return new Point(Source.Y, -Source.X);
                case 180:
                    return new Point(-Source.X, -Source.Y);
                case 270:
                    return new Point(-Source.Y, Source.X);
                case 0:
                    return Source;
                default:
                    throw new ArgumentOutOfRangeException("AngleDegrees", "Angle must be an increment of 90 degrees");
            }
        }

        public static bool Contains<T>(this T[] Source, T Value)
        {
            for (int i = 0; i < Source.Length; i++)
                if (Equals(Source[i], Value))
                    return true;
            return false;
        }
    }
}
