namespace MonoGame.Extras.Physics
{
    using Microsoft.Xna.Framework;

    public class Transform3D : BaseTransform
    {
        #region Fields

        private Vector3 _position = Vector3.Zero;

        private Vector3 _scale = Vector3.One;

        private Quaternion _rotation = Quaternion.Identity;

        #endregion

        #region Properties

        /// <summary>
        /// The position of world matrix
        /// </summary>
        public Vector3 WorldPosition => WorldMatrix.Translation;

        /// <summary>
        ///  The scale of world matrix
        /// </summary>
        public Vector3 WorldScale
        {
            get
            {
                Vector3 scale = Vector3.Zero;
                Quaternion rotation = Quaternion.Identity;
                Vector3 translation = Vector3.Zero;
                WorldMatrix.Decompose(out scale, out rotation, out translation);
                return scale;
            }
        }


        /// <summary>
        /// The rotation of world matrix
        /// </summary>
        public Quaternion WorldRotation
        {
            get
            {
                Vector3 scale = Vector3.Zero;
                Quaternion rotation = Quaternion.Identity;
                Vector3 translation = Vector3.Zero;
                WorldMatrix.Decompose(out scale, out rotation, out translation);
                return rotation;
            }
        }

        /// <summary>
        /// The local position of entity
        /// </summary>
        public Vector3 Position
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
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                UpdateTransform();
            }
        }

        /// <summary>
        /// The local rotation angle as quaternion
        /// </summary>
        public Quaternion Rotation
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
                Matrix.CreateScale(Scale) *
                Matrix.CreateFromQuaternion(Rotation) *
                Matrix.CreateTranslation(Position);
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
