namespace MonoGame.Extras.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extras.Math;
    using MonoGame.Extras.Physics;

    public class Renderer2D
    {
        #region Constructors

        public Renderer2D()
        {
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;
            Depth = 0f;
            Alpha = 1.0f;
        }

        #endregion

        #region Properties

        public Texture2D MainTexture { get; set; }

        public bool IsVisible { get; set; }

        public Color Color { get; set; }

        public SpriteEffects Effects { get; set; }

        public float Depth { get; set; }

        public float Alpha { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Size { get; set; }

        public Vector2 Offset { get; set; }

        #endregion

        #region Public Methods

        public Rectangle GetBoundingRectangle(Transform2D transform2D)
        {
            var rectBody = new Rectangle(Offset.ToPoint(), Size.ToPoint());

            return rectBody.Transform(transform2D.WorldMatrix);
        }

        #endregion
    }
}
