using System;
using System.Collections.Generic;
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

        public static T HighestScore<T>(this IEnumerable<T> Source, Func<T, double> Scoring)
        {
            T Candidate = default(T);
            double Score = double.MinValue;
            foreach (T Check in Source)
            {
                if (Score < Scoring(Check))
                    Candidate = Check;
            }
            return Candidate;
        }


        public static void Shuffle<T>(this IList<T> List)
        {
            int Count = List.Count;
            Random RNG = new Random();

            while (Count > 1)
            {
                Count--;
                int ShuffleWith = RNG.Next(0, Count + 1);
                T Temp = List[ShuffleWith];
                List[ShuffleWith] = List[Count];
                List[Count] = Temp;
            }
        }

        public static T LowestScore<T>(this IEnumerable<T> Source, Func<T, double> Scoring)
        {
            T Candidate = default(T);
            double Score = double.MinValue;
            foreach (T Check in Source)
            {
                if (Score < Scoring(Check))
                    Candidate = Check;
            }
            return Candidate;
        }

        public static bool IsNumeric(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
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

        public static T DefaultValue<T>(this T Source)
        {
            if (Source == null)
            {
                return default(T);
            }
            Type TY = typeof(T);
            if (TY.IsValueType)
            {
                return (T)Activator.CreateInstance(TY);
            }
            return default(T);
        }
    }
}
