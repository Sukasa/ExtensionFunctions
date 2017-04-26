using RngLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionFunctions
{
    public static class CollectionUtilities
    {
        public static T Pick<T>(IRng RNG) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(RNG.Next(0, values.Length));
        }

        public static void ForEach<T>(this IEnumerable<T> Collection, Action<T> Action)
        {
            foreach (T Item in Collection)
                Action(Item);
        }

        public static T Pick<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue((new Random()).Next(0, values.Length));
        }

        public static T Pick<T>(IEnumerable<T> Options, IRng RNG)
        {
            int Count = Options.Count();
            if (Count == 0)
                return default(T);
            return Options.ElementAt(RNG.Next(0, Count));
        }

        public static T Pick<T>(IEnumerable<T> Options)
        {
            int Count = Options.Count();
            if (Count == 0)
                return default(T);
            return Options.ElementAt((new Random()).Next(0, Count));
        }
    }
}
