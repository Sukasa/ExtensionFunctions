using System;

namespace Sukasa.UtilityClasses
{
    public struct ColourHSL
    {
        public double Hue;
        public double Saturation;
        public double Luminosity;

        // HSL to RGB and inverse code grabbed from Stack Overflow; copied from blog post by Garry Tan.  Original code credited to (defunct) mijijackson.com
        public ColourHSL(double Hue, double Sat, double Lum)
        {
            this.Hue = Hue;
            Saturation = Sat;
            Luminosity = Lum;
        }

        public ColourHSL(Colour Source)
        {
            double Red = Source.R / 255.0;
            double Green = Source.G / 255.0;
            double Blue = Source.B / 255.0;

            double Max = Math.Max(Red, Math.Max(Green, Blue));
            double Min = Math.Min(Red, Math.Min(Green, Blue));

            Luminosity = (Max + Min) / 2.0;

            if (Max == Min)
            {
                Hue = Saturation = 0.0;
            }
            else
            {
                double Delta = Max - Min;
                Saturation = Luminosity > 0.5 ? Delta / (2 - Max - Min) : Delta / (Max + Min);

                if (Max == Red)
                {
                    Hue = (Green - Blue) / Delta + (Green < Blue ? 6.0 : 0.0);
                }
                else if (Max == Green)
                {
                    Hue = (Blue - Red) / Delta + 2.0;
                }
                else
                {
                    Hue = (Red - Green) / Delta + 4.0;
                }

                Hue /= 6.0;
            }
        }

        private double Hue2RGB(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1 / 6) return p + (q - p) * 6 * t;
            if (t < 1 / 2) return q;
            if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
            return p;
        }

        public Colour ToColour()
        {
            Colour C = new Colour();
            C.A = 0xff;

            if (Saturation == 0)
            {
                C.R = C.G = C.B = (byte)(Luminosity * 255);
            }
            else
            {
                double q = Luminosity < 0.5 ? Luminosity * (1 + Saturation) : Luminosity + Saturation - Luminosity * Saturation;
                double p = 2 * Luminosity - q;

                C.R = (byte)(Hue2RGB(p, q, Hue + 1 / 3) * 255);
                C.G = (byte)(Hue2RGB(p, q, Hue) * 255);
                C.B = (byte)(Hue2RGB(p, q, Hue - 1 / 3) * 255);
            }

            return C;
        }
    }
}
