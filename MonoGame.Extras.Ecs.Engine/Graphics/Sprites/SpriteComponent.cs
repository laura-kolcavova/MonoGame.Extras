namespace MonoGame.Extras.Ecs.Engine.Graphics.Sprites
{
    using MonoGame.Extras.Graphics.Sprites;
    using MonoGame.Extras.Graphics.TextureAtlases;

    public class SpriteComponent : Sprite, IEntityComponent
    {
        public SpriteComponent(TextureRegion2D textureRegion) : base(textureRegion)
        {
        }
    }
}
