namespace MonoGame.Extras.Ecs.Engine.Physics
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a 2D transform component.
    /// </summary>
    public class Transform3DComponent : BaseTransform, IEntityComponent
    {
        private Vector3 position = Vector3.Zero;

        private Vector3 scale = Vector3.One;

        private Quaternion rotation = Quaternion.Identity;

        /// <summary>
        /// Gets position of world matrix.
        /// </summary>
        public Vector3 WorldPosition => WorldMatrix.Translation;

        /// <summary>
        /// Gets scale of world matrix.
        /// </summary>
        public Vector3 WorldScale
        {
            get
            {
                WorldMatrix.Decompose(out var scale, out _, out _);
                return scale;
            }
        }

        /// <summary>
        /// Gets rotation of world matrix.
        /// </summary>
        public Quaternion WorldRotation
        {
            get
            {
                WorldMatrix.Decompose(out _, out var rotation, out _);
                return rotation;
            }
        }

        /// <summary>
        /// Gets or sets local position.
        /// </summary>
        public Vector3 Position
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
        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// Gets or sets local rotation angle as quaternion.
        /// </summary>
        public Quaternion Rotation
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
                Matrix.CreateScale(Scale) *
                Matrix.CreateFromQuaternion(Rotation) *
                Matrix.CreateTranslation(Position);
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
