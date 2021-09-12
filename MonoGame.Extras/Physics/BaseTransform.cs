namespace MonoGame.Extras.Physics
{
    using Microsoft.Xna.Framework;
    using System;

    public abstract class BaseTransform
    {
        #region Fields

        private Matrix _localMatrix;

        private Matrix _worldMatrix;

        private bool _recalculateLocalMatrix = true;

        private bool _recalculateWorldMatrix = true;

        private BaseTransform _parent;

        #endregion

        #region Events

        private event Action TransformUpdated;

        #endregion

        #region Properties

        /// <summary>
        /// The local matrix of entity
        /// </summary>
        public Matrix LocalMatrix
        {
            get
            {
                if (_recalculateLocalMatrix)
                    RecalculateLocalMatrix();

                return _localMatrix;
            }
        }

        /// <summary>
        /// The world matrix of entity
        /// </summary>
        public Matrix WorldMatrix
        {
            get
            {
                if (_recalculateWorldMatrix)
                    RecalculateWorldMatrix();

                return _worldMatrix;
            }
        }

        /// <summary>
        /// The parent instance
        /// </summary>
        public BaseTransform Parent
        {
            get => _parent;
            set
            {
                if (_parent != value)
                    SetNewParent(value);
            }
        }

        #endregion

        #region Private Methods

        private void RecalculateLocalMatrix()
        {
            _localMatrix = CalculateLocalMatrix();
            _recalculateLocalMatrix = false;
        }

        private void RecalculateWorldMatrix()
        {
            _worldMatrix = CalculateWorldMatrix(LocalMatrix);
            _recalculateWorldMatrix = false;
        }

        private void SetNewParent(BaseTransform newParent)
        {
            var oldParent = _parent;

            if (oldParent != null)
            {
                oldParent.TransformUpdated -= UpdateTransform;
            }

            if(newParent != null)
            {
                newParent.TransformUpdated += UpdateTransform;
            }

            _parent = newParent;
        }

        #endregion

        #region Protected Methods

        protected void UpdateTransform()
        {
            _recalculateLocalMatrix = true;
            _recalculateWorldMatrix = true;
            TransformUpdated?.Invoke();
        }

        #endregion

        #region Abstract Methods

        protected abstract Matrix CalculateLocalMatrix();

        protected abstract Matrix CalculateWorldMatrix(Matrix localMatrix);

        #endregion
    }
}
