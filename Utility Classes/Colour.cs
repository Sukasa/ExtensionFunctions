using System;
using System.Runtime.InteropServices;

// ReSharper disable RedundantCast
// *** Because I've found a weird issue where (byte) + (value over 255) comes out to (invalid value).

namespace Sukasa.UtilityClasses
{
    /// <summary>
    ///     Color struct.  Similar to System.Drawing.Color, but is designed for extremely fast draw and blend calls
    /// </summary>
    /// <remarks>
    ///     The Colour structure wraps a speed-oriented 32bppARGB colour value.  It supports per-channel and as-whole addressing, and basic blend functions.  This struct assumes little-endian packing, such as in x86 or x64
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct Colour
    {
        // *** Members
        /// <summary>
        ///     32bppARGB colour value.  Memory-overlapped with individual channels
        /// </summary>
        [FieldOffset(0)]
        public UInt32 Color;

        /// <summary>
        ///     Blue channel
        /// </summary>
        [FieldOffset(0)]
        public byte B;

        /// <summary>
        ///     Green channel
        /// </summary>
        [FieldOffset(1)]
        public byte G;

        /// <summary>
        ///     Red channel
        /// </summary>
        [FieldOffset(2)]
        public byte R;

        /// <summary>
        ///     Alpha channel
        /// </summary>
        [FieldOffset(3)]
        public byte A;

        public Colour(uint ColourVal)
        {
            A = B = G = R = 0;
            Color = ColourVal;
        }

        public Colour(byte A, byte R, byte G, byte B)
        {
            Color = 0x0;
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public Colour(byte R, byte G, byte B) : this(255, R, G, B)
        {
        }

        /*
         *  These functions all include repeated code, specifically the integer blending.  This is on purpose; the overhead of the function call forms more than 50% of the
         *  total consumed CPU cycles for Blend()! 
         */

        // *** Functions
        /// <summary>
        ///     Returns a new instance of this colour
        /// </summary>
        /// <remarks>
        ///     Copy() allocates a new Colour struct and sets its values to the same values as this Colour.
        /// </remarks>
        /// <returns>
        ///     A fresh copy of the struct
        /// </returns>
        public Colour Copy()
        {
            return new Colour { Color = Color };
        }

        /// <summary>
        ///     Blend another colour onto this one, storing the result into this colour
        /// </summary>
        /// <param name="Top">
        ///     What colour to blend on top of this one
        /// </param>
        /// <returns>
        ///     This colour 
        /// </returns>
        /// <remarks>
        ///     Blend another colour onto this one, storing the result into this colour.  The final alpha is set to the blended colour's alpha value.
        /// </remarks>
        public Colour Blend(Colour Top)
        {
            Color = (Top.Color & 0xFF000000) | // *** Alpha
                    (((Color & 0x00FF00FFU) + ((((Top.Color & 0x00FF00FFU) - (Color & 0x00FF00FFU)) * (UInt32)Top.A + 0x00800080U) >> 8)) & 0x00FF00FFU) | // Red, Blue
                    (((Color & 0x0000FF00U) + ((((Top.Color & 0x0000FF00U) - (Color & 0x0000FF00U)) * (UInt32)Top.A + 0x00008000U) >> 8)) & 0x0000FF00U); // Green
            return this;
        }

        /// <summary>
        ///     Blend another colour onto this one with the specified alpha, storing the result into this colour
        /// </summary>
        /// <param name="Top">
        ///     What colour to blend on top of this one.  Alpha is ignored for blending, but returned as part of the resulting colour.
        /// </param>
        /// <param name="UseAlpha">
        ///     What alpha value to use for the source colour
        /// </param>
        /// <returns>
        ///     This colour
        /// </returns>
        /// <remarks>
        ///     Blend another colour onto this one with a supplied alpha value, storing the result into this colour.  The final alpha is set to the blended colour's alpha
        /// </remarks>
        public Colour Blend(Colour Top, UInt32 UseAlpha)
        {
            Color = (Top.Color & 0xFF000000) | // *** Alpha
                    (((Color & 0x00FF00FFU) + ((((Top.Color & 0x00FF00FFU) - (Color & 0x00FF00FFU)) * UseAlpha + 0x00800080U) >> 8)) & 0x00FF00FFU) | // Red, Blue
                    (((Color & 0x0000FF00U) + ((((Top.Color & 0x0000FF00U) - (Color & 0x0000FF00U)) * UseAlpha + 0x00008000U) >> 8)) & 0x0000FF00U); // Green
            return this;
        }

        /// <summary>
        ///     Set this Colour to full alpha
        /// </summary>
        /// <remarks>
        ///     Sets this Colour to full alpha and returns it.
        /// </remarks>
        /// <returns>
        ///     This Colour
        /// </returns>
        public Colour FullAlpha()
        {
            Color |= 0xFF000000U;
            return this;
        }


        public override string ToString()
        {
            return string.Format("Colour #{0:X8}", Color);
        }


        // *** Colour Constants
        /// <summary>
        ///     Opaque black
        /// </summary>
        public static Colour Black
        {
            get
            {
                return new Colour { Color = 0xFF000000 };
            }
        }

        /// <summary>
        ///  Opaque White
        /// </summary>
        public static Colour White
        {
            get
            {
                return new Colour { Color = 0xFFFFFFFF };
            }
        }

        /// <summary>
        ///     Transparent Black
        /// </summary>
        public static Colour Transparent
        {
            get
            {
                return new Colour { Color = 0x00000000U };
            }
        }
    }
}
