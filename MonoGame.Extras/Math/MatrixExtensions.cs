﻿namespace MonoGame.Extras.Math
{
    using Microsoft.Xna.Framework;
    using System;

    public static class MatrixExtensions
    {
        public static void Decompose(this Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            Vector3 position3, scale3;
            Quaternion rotationQ;

            matrix.Decompose(out scale3, out rotationQ, out position3);

            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2((double)(direction.Y), (double)(direction.X));
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }
    }
}
