namespace MonoGame.Extras.Ecs.Engine.Physics
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents an abstract base class for transform components.
    /// </summary>
    public abstract class BaseTransform
    {
        private Matrix localMatrix;

        private Matrix worldMatrix;

        private bool recalculateLocalMatrix = true;

        private bool recalculateWorldMatrix = true;

        private BaseTransform parent;

        private event Action TransformUpdated;

        /// <summary>
        /// Gets local matrix.
        /// </summary>
        public Matrix LocalMatrix
        {
            get
            {
                if (recalculateLocalMatrix)
                {
                    RecalculateLocalMatrix();
                }

                return localMatrix;
            }
        }

        /// <summary>
        /// Gets world matrix.
        /// </summary>
        public Matrix WorldMatrix
        {
            get
            {
                if (recalculateWorldMatrix)
                {
                    RecalculateWorldMatrix();
                }

                return worldMatrix;
            }
        }

        /// <summary>
        /// Gets or sets a parent transform.
        /// </summary>
        public BaseTransform Parent
        {
            get => parent;
            set
            {
                if (parent != value)
                {
                    SetNewParent(value);
                }
            }
        }

        /// <summary>
        /// Updates transform.
        /// </summary>
        protected void UpdateTransform()
        {
            recalculateLocalMatrix = true;
            recalculateWorldMatrix = true;
            TransformUpdated?.Invoke();
        }

        /// <summary>
        /// Creates new <see cref="Matrix"/> instance which represents local matrix.
        /// </summary>
        /// <returns>New <see cref="Matrix"/> instance.</returns>
        protected abstract Matrix CalculateLocalMatrix();

        /// <summary>
        /// Creates new <see cref="Matrix"/> instance which represents world matrix using given <see cref="Matrix"/> instance which represents local matrix..
        /// </summary>
        /// <param name="localMatrix">The <see cref="Matrix"/> instance which represents local matrix.</param>
        /// <returns>New <see cref="Matrix"/> instance. </returns>
        protected abstract Matrix CalculateWorldMatrix(Matrix localMatrix);

        private void RecalculateLocalMatrix()
        {
            localMatrix = CalculateLocalMatrix();
            recalculateLocalMatrix = false;
        }

        private void RecalculateWorldMatrix()
        {
            worldMatrix = CalculateWorldMatrix(LocalMatrix);
            recalculateWorldMatrix = false;
        }

        private void SetNewParent(BaseTransform newParent)
        {
            var oldParent = parent;

            if (oldParent != null)
            {
                oldParent.TransformUpdated -= UpdateTransform;
            }

            if (newParent != null)
            {
                newParent.TransformUpdated += UpdateTransform;
            }

            parent = newParent;
        }
    }
}
