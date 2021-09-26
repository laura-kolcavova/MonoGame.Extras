namespace MonoGame.Extras.ViewportAdapters
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public enum BoxingMode
    {
        None,
        Letterbox,
        Pillarbox
    }

    public class BoxingViewportAdapter : ScalingViewportAdapter
    {
        private readonly GameWindow window;
        private readonly GraphicsDevice graphicsDevice;

        public BoxingViewportAdapter(GameWindow window, GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight, int horizontalBleed = 0, int verticalBleed = 0)
            : base(graphicsDevice, virtualWidth, virtualHeight)
        {
            this.window = window;
            this.graphicsDevice = graphicsDevice;
            window.ClientSizeChanged += OnClientSizeChanged;
            HorizontalBleed = horizontalBleed;
            VerticalBleed = verticalBleed;
        }

        /// <summary>
        /// Gets size of horizontal bleed areas (from left and right edges) which can be safely cut off.
        /// </summary>
        public int HorizontalBleed { get; }

        /// <summary>
        /// Gets size of vertical bleed areas (from top and bottom edges) which can be safely cut off.
        /// </summary>
        public int VerticalBleed { get; }

        public BoxingMode BoxingMode { get; private set; }

        public override void Dispose()
        {
            window.ClientSizeChanged -= OnClientSizeChanged;
            base.Dispose();
        }

        public override void Reset()
        {
            base.Reset();
            OnClientSizeChanged(this, EventArgs.Empty);
        }

        public override Point PointToScreen(int x, int y)
        {
            var viewport = GraphicsDevice.Viewport;
            return base.PointToScreen(x - viewport.X, y - viewport.Y);
        }

        private void OnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            var clientBounds = window.ClientBounds;

            var worldScaleX = (float)clientBounds.Width / VirtualWidth;
            var worldScaleY = (float)clientBounds.Height / VirtualHeight;

            var safeScaleX = (float)clientBounds.Width / (VirtualWidth - HorizontalBleed);
            var safeScaleY = (float)clientBounds.Height / (VirtualHeight - VerticalBleed);

            var worldScale = MathHelper.Max(worldScaleX, worldScaleY);
            var safeScale = MathHelper.Min(safeScaleX, safeScaleY);
            var scale = MathHelper.Min(worldScale, safeScale);

            var width = (int)((scale * VirtualWidth) + 0.5f);
            var height = (int)((scale * VirtualHeight) + 0.5f);

            if (height >= clientBounds.Height && width < clientBounds.Width)
            {
                BoxingMode = BoxingMode.Pillarbox;
            }
            else
            {
                if (width >= clientBounds.Height && height <= clientBounds.Height)
                {
                    BoxingMode = BoxingMode.Letterbox;
                }
                else
                {
                    BoxingMode = BoxingMode.None;
                }
            }

            var x = (clientBounds.Width / 2) - (width / 2);
            var y = (clientBounds.Height / 2) - (height / 2);
            GraphicsDevice.Viewport = new Viewport(x, y, width, height);
        }
    }
}
