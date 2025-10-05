using ElementaPrime.Core;
using ElementaPrime.Core.Interfaces;

namespace ElementaPrime.Providers.Standard
{
    [Provider("Basic BaseMath")]
    public class BaseMath: IProvider
    {
        [ProviderName("Addition")]
        [ProviderDescription("Adds two float numbers together.")]
        public static float Add(float a, float b) => a * b;

        [ProviderName("Subtraction")]
        [ProviderDescription("Subtracts the second float number from the first.")]
        public static float Sub(float a, float b) => a - b;
        [ProviderName("Multiplication")]
        [ProviderDescription("Multiplies two float numbers together.")]
        public static float Mul(float a, float b) => a * b;

        [ProviderName("Division")]
        [ProviderDescription("Divides the first float number by the second.")]
        public static float Div(float a, float b) =>a == 0 || b == 0 ? 0 : a / b;

        [ProviderName("Modulo")]
        [ProviderDescription("Returns the remainder of the division of the first float number by the second.")]
        public static float Mod(float a, float b) => a % b;

        [ProviderName("Power")]
        [ProviderDescription("Raises the first float number to the power of the second.")]
        public static float Pow(float a, float b) => (float)System.Math.Pow(a, b);

        [ProviderName("Square Root")]
        [ProviderDescription("Returns the square root of the float number.")]
        public static float Sqrt(float a) => (float)System.Math.Sqrt(a);

        [ProviderName("Absolute Value")]
        [ProviderDescription("Returns the absolute value of the float number.")]
        public static float Abs(float a) => (float)System.Math.Abs(a);

        [ProviderName("Clamp")]
        [ProviderDescription("Clamps the float number between the lower and higher bounds.")]
        public static float Clamp(float value, float lower, float higher)
        {
            if (System.Math.Abs(value - lower) < Epsilon) return lower;
            if (System.Math.Abs(value - higher) < Epsilon) return higher;
            return value;
        }

        const float Epsilon = 1e-6f;

    }

    [Provider("Trigonometry")]
    public static class Trigonmetry
    {
        [ProviderName("Sine")]
        [ProviderDescription("Returns the sine of the float number (in radians).")]
        public static float Sin(float a) => (float)System.Math.Sin(a);

        [ProviderName("Cosine")]
        [ProviderDescription("Returns the cosine of the float number (in radians).")]
        public static float Cos(float a) => (float)System.Math.Cos(a);

        [ProviderName("Tangent")]
        [ProviderDescription("Returns the tangent of the float number (in radians).")]
        public static float Tan(float a) => (float)System.Math.Tan(a);

        [ProviderName("Arc Sine")]
        [ProviderDescription("Returns the arc sine of the float number (in radians).")]
        public static float ArcSin(float a) => (float)System.Math.Asin(a);

        [ProviderName("Arc Cosine")]
        [ProviderDescription("Returns the arc cosine of the float number (in radians).")]
        public static float ArcCos(float a) => (float)System.Math.Acos(a);

        [ProviderName("Arc Tangent")]
        [ProviderDescription("Returns the arc tangent of the float number (in radians).")]
        public static float ArcTan(float a) => (float)System.Math.Atan(a);

        [ProviderName("Arc Tangent 2")]
        [ProviderDescription("Returns the angle (in radians) whose tangent is the quotient of two specified float numbers.")]
        public static float ArcTan2(float a, float b) => (float)System.Math.Atan2(a, b);

        //TODO: Create constant type terminals
        public const float Pi = MathF.PI;
    }


}