namespace MonoGame.Extras.Graphics.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extras.Graphics.TextureAtlases;
    using MonoGame.Extras.Math;
    using MonoGame.Extras.Physics;

    public class Sprite
    {
        #region Constructors

        public Sprite(TextureRegion2D textureRegion)
        {
            TextureRegion = textureRegion;
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;
            Alpha = 1.0f;
        }

        #endregion

        #region Properties

        public TextureRegion2D TextureRegion { get; protected set; }

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

            return RectangleExtensions.Transform(rectBody, transform2D.WorldMatrix);
        }

        #endregion
    }
}
