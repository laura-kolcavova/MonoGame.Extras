namespace MonoGame.Extras.Cameras
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extras.ViewportAdapters;

    /// <summary>
    /// Represents an orthographic game camera.
    /// </summary>
    public sealed class OrthographicCamera : Camera<Vector2>
    {
        private readonly ViewportAdapter viewportAdapter;
        private float maximumZoom = float.MaxValue;
        private float minimumZoom;
        private float zoom;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrthographicCamera"/> class.
        /// Uses <see cref="DefaultViewportAdapter"/> as default.
        /// </summary>
        /// <param name="graphicsDevice">A <see cref="GraphicsDevice"/> instance.</param>
        public OrthographicCamera(GraphicsDevice graphicsDevice)
            : this(new DefaultViewportAdapter(graphicsDevice))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrthographicCamera"/> class.
        /// </summary>
        /// <param name="viewportAdapter">A <see cref="ViewportAdapter"/> instance.</param>
        public OrthographicCamera(ViewportAdapter viewportAdapter)
        {
            this.viewportAdapter = viewportAdapter;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewportAdapter.VirtualWidth / 2f, viewportAdapter.VirtualHeight / 2f);
            Position = Vector2.Zero;
        }

        /// <inheritdoc/>
        public override Vector2 Position { get; set; }

        /// <inheritdoc/>
        public override float Rotation { get; set; }

        /// <inheritdoc/>
        public override Vector2 Origin { get; set; }

        /// <inheritdoc/>
        public override Vector2 Center => Position + Origin;

        /// <inheritdoc/>
        public override float Zoom
        {
            get => zoom;
            set
            {
                if ((value < MinimumZoom) || (value > MaximumZoom))
                {
                    throw new ArgumentException("Zoom must be between MinimumZoom and MaximumZoom");
                }

                zoom = value;
            }
        }

        /// <inheritdoc/>
        public override float MinimumZoom
        {
            get => minimumZoom;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("MinimumZoom must be greater than zero");
                }

                if (Zoom < value)
                {
                    Zoom = MinimumZoom;
                }

                minimumZoom = value;
            }
        }

        /// <inheritdoc/>
        public override float MaximumZoom
        {
            get => maximumZoom;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("MaximumZoom must be greater than zero");
                }

                if (Zoom > value)
                {
                    Zoom = value;
                }

                maximumZoom = value;
            }
        }

        /// <inheritdoc/>
        public override Rectangle BoundingRectangle
        {
            get
            {
                var frustum = GetBoundingFrustum();
                var corners = frustum.GetCorners();
                var topLeft = corners[0];
                var bottomRight = corners[2];
                var width = bottomRight.X - topLeft.X;
                var height = bottomRight.Y - topLeft.Y;
                return new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)width, (int)height);
            }
        }

        /// <inheritdoc/>
        public override void Move(Vector2 direction)
        {
            Position += Vector2.Transform(direction, Matrix.CreateRotationZ(-Rotation));
        }

        /// <inheritdoc/>
        public override void Rotate(float deltaRadians)
        {
            Rotation += deltaRadians;
        }

        /// <inheritdoc/>
        public override void ZoomIn(float deltaZoom)
        {
            ClampZoom(Zoom + deltaZoom);
        }

        /// <inheritdoc/>
        public override void ZoomOut(float deltaZoom)
        {
            ClampZoom(Zoom - deltaZoom);
        }

        /// <inheritdoc/>
        public override void LookAt(Vector2 position)
        {
            Position = position - new Vector2(viewportAdapter.VirtualWidth / 2f, viewportAdapter.VirtualHeight / 2f);
        }

        public Vector2 WorldToScreen(float x, float y)
        {
            return WorldToScreen(new Vector2(x, y));
        }

        /// <inheritdoc/>
        public override Vector2 WorldToScreen(Vector2 worldPosition)
        {
            var viewport = viewportAdapter.Viewport;
            return Vector2.Transform(worldPosition + new Vector2(viewport.X, viewport.Y), GetViewMatrix());
        }

        public Vector2 ScreenToWorld(float x, float y)
        {
            return ScreenToWorld(new Vector2(x, y));
        }

        /// <inheritdoc/>
        public override Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            var viewport = viewportAdapter.Viewport;
            return Vector2.Transform(
                screenPosition - new Vector2(viewport.X, viewport.Y),
                Matrix.Invert(GetViewMatrix()));
        }

        public Matrix GetViewMatrix(Vector2 parallaxFactor)
        {
            return GetVirtualViewMatrix(parallaxFactor) * viewportAdapter.GetScaleMatrix();
        }

        /// <inheritdoc/>
        public override Matrix GetViewMatrix()
        {
            return GetViewMatrix(Vector2.One);
        }

        /// <inheritdoc/>
        public override Matrix GetInverseViewMatrix()
        {
            return Matrix.Invert(GetViewMatrix());
        }

        /// <inheritdoc/>
        public override BoundingFrustum GetBoundingFrustum()
        {
            var viewMatrix = GetVirtualViewMatrix();
            var projectionMatrix = GetProjectionMatrix(viewMatrix);
            return new BoundingFrustum(projectionMatrix);
        }

        public ContainmentType Contains(Point point)
        {
            return Contains(point.ToVector2());
        }

        /// <inheritdoc/>
        public override ContainmentType Contains(Vector2 vector2)
        {
            return GetBoundingFrustum().Contains(new Vector3(vector2.X, vector2.Y, 0));
        }

        /// <inheritdoc/>
        public override ContainmentType Contains(Rectangle rectangle)
        {
            var max = new Vector3(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, 0.5f);
            var min = new Vector3(rectangle.X, rectangle.Y, 0.5f);
            var boundingBox = new BoundingBox(min, max);
            return GetBoundingFrustum().Contains(boundingBox);
        }

        private void ClampZoom(float value)
        {
            if (value < MinimumZoom)
            {
                Zoom = MinimumZoom;
            }
            else
            {
                Zoom = value > MaximumZoom ? MaximumZoom : value;
            }
        }

        private Matrix GetVirtualViewMatrix(Vector2 parallaxFactor)
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position * parallaxFactor, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        private Matrix GetVirtualViewMatrix()
        {
            return GetVirtualViewMatrix(Vector2.One);
        }

        private Matrix GetProjectionMatrix(Matrix viewMatrix)
        {
            var projection = Matrix.CreateOrthographicOffCenter(0, viewportAdapter.VirtualWidth, viewportAdapter.VirtualHeight, 0, -1, 0);
            Matrix.Multiply(ref viewMatrix, ref projection, out projection);
            return projection;
        }
    }
}
