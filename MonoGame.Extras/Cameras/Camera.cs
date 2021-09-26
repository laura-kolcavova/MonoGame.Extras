namespace MonoGame.Extras.Cameras
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents an abstract base class for game cameras.
    /// </summary>
    /// <typeparam name="T">A vector type.</typeparam>
    public abstract class Camera<T>
        where T : struct
    {
        /// <summary>
        /// Gets or sets position of the camera.
        /// </summary>
        public abstract T Position { get; set; }

        /// <summary>
        /// Gets or sets rotation of the camera.
        /// </summary>
        public abstract float Rotation { get; set; }

        /// <summary>
        /// Gets or sets zoom value of the camera.
        /// </summary>
        public abstract float Zoom { get; set; }

        /// <summary>
        /// Gets or sets minimum zoom value of the camera.
        /// </summary>
        public abstract float MinimumZoom { get; set; }

        /// <summary>
        /// Gets or sets maximum zoom value of the camera.
        /// </summary>
        public abstract float MaximumZoom { get; set; }

        /// <summary>
        /// Gets bounding rectangle of the camera.
        /// </summary>
        public abstract Rectangle BoundingRectangle { get; }

        /// <summary>
        /// Gets or sets origin position of the camera.
        /// </summary>
        public abstract T Origin { get; set; }

        /// <summary>
        /// Gets center position of the camera.
        /// </summary>
        public abstract T Center { get; }

        /// <summary>
        /// Moves the camera by given direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public abstract void Move(T direction);

        /// <summary>
        /// Rotates the camera by given value in radians.
        /// </summary>
        /// <param name="deltaRadians">The value in radians.</param>
        public abstract void Rotate(float deltaRadians);

        /// <summary>
        /// Zooms in the camera by given value as delta.
        /// </summary>
        /// <param name="deltaZoom">The value as delta.</param>
        public abstract void ZoomIn(float deltaZoom);

        /// <summary>
        /// Zooms out the camera by given value as delta.
        /// </summary>
        /// <param name="deltaZoom">The value as delta.</param>
        public abstract void ZoomOut(float deltaZoom);

        /// <summary>
        /// Moves the camera to given position.
        /// </summary>
        /// <param name="position">The position.</param>
        public abstract void LookAt(T position);

        /// <summary>
        /// Calculates position as world to screen.
        /// </summary>
        /// <param name="worldPosition">A world position.</param>
        /// <returns>Calculated position as world to screen.</returns>
        public abstract T WorldToScreen(T worldPosition);

        /// <summary>
        /// Calculates position as screen to world.
        /// </summary>
        /// <param name="screenPosition">A screen position.</param>
        /// <returns>Calculated position as world to screen.</returns>
        public abstract T ScreenToWorld(T screenPosition);

        /// <summary>
        /// Gets new created view matrix.
        /// </summary>
        /// <returns>New <see cref="Matrix"/> instance.</returns>
        public abstract Matrix GetViewMatrix();

        /// <summary>
        /// Gets new created inverse view matrix.
        /// </summary>
        /// <returns>New inversed <see cref="Matrix"/> instance.</returns>
        public abstract Matrix GetInverseViewMatrix();

        /// <summary>
        /// Gets bounding frustum.
        /// </summary>
        /// <returns><see cref="BoundingFrustum"/> instance.</returns>
        public abstract BoundingFrustum GetBoundingFrustum();

        /// <summary>
        ///  Gets <see cref="ContainmentType"/>.
        /// </summary>
        /// <param name="vector2">The <see cref="Vector2"/> object.</param>
        /// <returns><see cref="ContainmentType"/>.</returns>
        public abstract ContainmentType Contains(Vector2 vector2);

        /// <summary>
        /// Gets <see cref="ContainmentType"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> object.</param>
        /// <returns><see cref="ContainmentType"/>.</returns>
        public abstract ContainmentType Contains(Rectangle rectangle);
    }
}
