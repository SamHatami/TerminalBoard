using System.Numerics;
using TerminalBoard.Core;
using TerminalBoard.Core.Interfaces;

namespace TerminalBoard.Providers.Standard
{
    //Wrapper-class for system numerics Vector operations
    [TerminalProvider("Vector Math")]
    public class VectorMath : IProvider
    {
        public static float Vector2DotProduct(Vector2 a, Vector2 b) => Vector2.Dot(a, b);

        public static float Vector3DotProduct(Vector3 a, Vector3 b) => Vector3.Dot(a, b);

        public static float Vector2CrossProduct(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;

        public static Vector3 Vetor3CrossProduct(Vector3 a, Vector3 b) => Vector3.Cross(a, b);

        public static Vector2 Vetor2Lerp(Vector2 a, Vector2 b, float t) => Vector2.Lerp(a, b, t);

        public static Vector3 Vetor3Lerp(Vector3 a, Vector3 b, float t) => Vector3.Lerp(a, b, t);

        public static Vector2 Vector2Normalize(Vector2 a) => Vector2.Normalize(a);

        public static Vector3 Vector3Normalize(Vector3 a) => Vector3.Normalize(a);

        public static float Vector2Length(Vector2 a) => a.Length();

        public static float Vector3Length(Vector3 a) => a.Length();

        public static float Vector2Distance(Vector2 a, Vector2 b) => Vector2.Distance(a, b);

        public static float Vector3Distance(Vector3 a, Vector3 b) => Vector3.Distance(a, b);

        //angles
        public static float Vector2Angle(Vector2 a, Vector2 b)
        {
            var dot = Vector2.Dot(a, b);
            var lengths = a.Length() * b.Length();
            if (lengths == 0) return 0;
            var cosAngle = dot / lengths;
            cosAngle = BaseMath.Clamp(cosAngle, -1f, 1f); // Clamp to avoid NaN due to floating point errors
            return MathF.Acos(cosAngle); // Result in radians
        }

        public static Vector2 Vector2Reflect(Vector2 vector, Vector2 normal) => Vector2.Reflect(vector, normal);

        public static Vector3 Vector3Reflect(Vector3 vector, Vector3 normal) => Vector3.Reflect(vector, normal);

        public static Vector2 Vector2Transform(Vector2 vector, Matrix3x2 matrix) => Vector2.Transform(vector, matrix);

        public static Vector3 Vector3Transform(Vector3 vector, Matrix4x4 matrix) => Vector3.Transform(vector, matrix);

        public static Matrix3x2 CreateMatrix3X2Rotation(float radians) => Matrix3x2.CreateRotation(radians);

        public static Matrix4x4 CreateMatrix4X4RotationX(float radians) => Matrix4x4.CreateRotationX(radians);

        public static Matrix4x4 CreateMatrix4X4RotationY(float radians) => Matrix4x4.CreateRotationY(radians);

        public static Matrix4x4 CreateMatrix4X4RotationZ(float radians) => Matrix4x4.CreateRotationZ(radians);

        public static Matrix4x4 CreateMatrix4X4Translation(float x, float y, float z) => Matrix4x4.CreateTranslation(x, y, z);

        public static Matrix4x4 CreateMatrix4X4Scale(float x, float y, float z) => Matrix4x4.CreateScale(x, y, z);

        public static Matrix3x2 CreateMatrix3X2Scale(float x, float y) => Matrix3x2.CreateScale(x, y);
    }
}