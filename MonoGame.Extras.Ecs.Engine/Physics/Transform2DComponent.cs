namespace MonoGame.Extras.Ecs.Engine.Physics
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Math;

    /// <summary>
    /// Represents a 2D transform component.
    /// </summary>
    public class Transform2DComponent : BaseTransform, IEntityComponent
    {
        private Vector2 position = Vector2.Zero;

        private Vector2 scale = Vector2.One;

        private float rotation = 0f;

        /// <summary>
        /// Gets position of world matrix.
        /// </summary>
        public Vector2 WorldPosition
        {
            get
            {
                var translation = WorldMatrix.Translation;
                return new Vector2(translation.X, translation.Y);
            }
        }

        /// <summary>
        /// Gets the scale of world matrix.
        /// </summary>
        public Vector2 WorldScale
        {
            get
            {
                WorldMatrix.Decompose(out _, out _, out Vector2 scale);
                return scale;
            }
        }

        /// <summary>
        /// Gets the rotation of world matrix.
        /// </summary>
        public float WorldRotation
        {
            get
            {
                WorldMatrix.Decompose(out _, out float rotation, out _);
                return rotation;
            }
        }

        /// <summary>
        /// Gets or sets local position.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// Gets or sets local scale.
        /// </summary>
        public Vector2 Scale
        {
            get => scale;
            set
            {
                scale = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// Gets or sets local rotation angle in radinas.
        /// </summary>
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                UpdateTransform();
            }
        }

        /// <inheritdoc/>
        protected override Matrix CalculateLocalMatrix()
        {
            return
                Matrix.CreateScale(new Vector3(Scale, 1f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(new Vector3(Position, 0f));
        }

        /// <inheritdoc/>
        protected override Matrix CalculateWorldMatrix(Matrix localMatrix)
        {
            if (Parent != null)
            {
                return Matrix.Multiply(localMatrix, Parent.WorldMatrix);
            }
            else
            {
                return localMatrix;
            }
        }
    }
}
