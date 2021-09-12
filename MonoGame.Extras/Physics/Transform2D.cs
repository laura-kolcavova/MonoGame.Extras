namespace MonoGame.Extras.Physics
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Math;

    public class Transform2D : BaseTransform
    {
        #region Fields

        private Vector2 _position = Vector2.Zero;

        private Vector2 _scale = Vector2.One;

        private float _rotation = 0f;

        #endregion

        #region Properties

        /// <summary>
        /// The position of world matrix
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
        ///  The scale of world matrix
        /// </summary>
        public Vector2 WorldScale
        {
            get
            {
                WorldMatrix.Decompose(out Vector2 position, out float rotation, out Vector2 scale);
                return scale;
            }
        }


        /// <summary>
        /// The rotation of world matrix
        /// </summary>
        public float WorldRotation
        {
            get
            {
                WorldMatrix.Decompose(out Vector2 position, out float rotation, out Vector2 scale);
                return rotation;
            }
        }

        /// <summary>
        /// The local position of entity
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// The local scale of entity
        /// </summary>
        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// The local rotation angle in radinas
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                UpdateTransform();
            }
        }

        #endregion

        #region Private Methods

        protected override Matrix CalculateLocalMatrix()
        {
            return
                Matrix.CreateScale(new Vector3(Scale, 1f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(new Vector3(Position, 0f));
        }

        protected override Matrix CalculateWorldMatrix(Matrix localMatrix)
        {
            if(Parent != null)
            {
                return Matrix.Multiply(localMatrix, Parent.WorldMatrix);
            }
            else
            {
                return localMatrix;
            }
        }

        #endregion
    }
}
