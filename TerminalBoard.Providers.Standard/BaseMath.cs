using TerminalBoard.Core;
using TerminalBoard.Core.Interfaces;

namespace TerminalBoard.Providers.Standard
{
    [TerminalProvider("Basic BaseMath")]
    public class BaseMath: IProvider
    {
        [TerminalName("Addition")]
        [TerminalDescription("Adds two float numbers together.")]
        public static float Add(float a, float b) => a * b;

        [TerminalName("Subtraction")]
        [TerminalDescription("Subtracts the second float number from the first.")]
        public static float Sub(float a, float b) => a - b;
        [TerminalName("Multiplication")]
        [TerminalDescription("Multiplies two float numbers together.")]
        public static float Mul(float a, float b) => a * b;

        [TerminalName("Division")]
        [TerminalDescription("Divides the first float number by the second.")]
        public static float Div(float a, float b) =>a == 0 || b == 0 ? 0 : a / b;

        [TerminalName("Modulo")]
        [TerminalDescription("Returns the remainder of the division of the first float number by the second.")]
        public static float Mod(float a, float b) => a % b;

        [TerminalName("Power")]
        [TerminalDescription("Raises the first float number to the power of the second.")]
        public static float Pow(float a, float b) => (float)System.Math.Pow(a, b);

        [TerminalName("Square Root")]
        [TerminalDescription("Returns the square root of the float number.")]
        public static float Sqrt(float a) => (float)System.Math.Sqrt(a);

        [TerminalName("Absolute Value")]
        [TerminalDescription("Returns the absolute value of the float number.")]
        public static float Abs(float a) => (float)System.Math.Abs(a);

        [TerminalName("Clamp")]
        [TerminalDescription("Clamps the float number between the lower and higher bounds.")]
        public static float Clamp(float value, float lower, float higher)
        {
            if (System.Math.Abs(value - lower) < Epsilon) return lower;
            if (System.Math.Abs(value - higher) < Epsilon) return higher;
            return value;
        }

        const float Epsilon = 1e-6f;

    }

    [TerminalProvider("Trigonometry")]
    public static class Trigonmetry
    {
        [TerminalName("Sine")]
        [TerminalDescription("Returns the sine of the float number (in radians).")]
        public static float Sin(float a) => (float)System.Math.Sin(a);

        [TerminalName("Cosine")]
        [TerminalDescription("Returns the cosine of the float number (in radians).")]
        public static float Cos(float a) => (float)System.Math.Cos(a);

        [TerminalName("Tangent")]
        [TerminalDescription("Returns the tangent of the float number (in radians).")]
        public static float Tan(float a) => (float)System.Math.Tan(a);

        [TerminalName("Arc Sine")]
        [TerminalDescription("Returns the arc sine of the float number (in radians).")]
        public static float ArcSin(float a) => (float)System.Math.Asin(a);

        [TerminalName("Arc Cosine")]
        [TerminalDescription("Returns the arc cosine of the float number (in radians).")]
        public static float ArcCos(float a) => (float)System.Math.Acos(a);

        [TerminalName("Arc Tangent")]
        [TerminalDescription("Returns the arc tangent of the float number (in radians).")]
        public static float ArcTan(float a) => (float)System.Math.Atan(a);

        [TerminalName("Arc Tangent 2")]
        [TerminalDescription("Returns the angle (in radians) whose tangent is the quotient of two specified float numbers.")]
        public static float ArcTan2(float a, float b) => (float)System.Math.Atan2(a, b);

        //TODO: Create constant type terminals
        public const float Pi = MathF.PI;
    }


}