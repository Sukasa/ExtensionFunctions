using System;
using System.Collections.Generic;

// Not complete!

namespace Sukasa.UtilityClasses
{
    public class ValueCurve<T>
    {
        private SortedList<double, Tuple<double, T>> Points = new SortedList<double, Tuple<double, T>>();
        public void AddPoint(double RawValue, T CurveValue)
        {
            Points.Add(RawValue, new Tuple<double, T>(RawValue, CurveValue));
        }

        public void Clear()
        {
            Points.Clear();
        }
        
        public double RawValue { get; set; }
        public T Value
        {
            get
            {
                return default(T);
            }
        }
    }
}
