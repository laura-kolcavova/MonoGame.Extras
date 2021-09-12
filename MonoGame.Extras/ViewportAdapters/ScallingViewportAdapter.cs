namespace MonoGame.Extras.ViewportAdapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ScalingViewportAdapter : ViewportAdapter
    {
        public ScalingViewportAdapter(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight)
            : base(graphicsDevice)
        {
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
        }

        public override int VirtualWidth { get; }
        public override int VirtualHeight { get; }
        public override int ViewportWidth => GraphicsDevice.Viewport.Width;
        public override int ViewportHeight => GraphicsDevice.Viewport.Height;

        public override Matrix GetScaleMatrix()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }

        public Vector2 GetScale()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;

            return new Vector2(scaleX, scaleY);
        }
    }
}
