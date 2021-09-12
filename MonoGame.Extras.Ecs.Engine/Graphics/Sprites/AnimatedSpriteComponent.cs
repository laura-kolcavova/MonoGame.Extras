namespace MonoGame.Extras.Ecs.Engine.Graphics.Sprites
{
    using MonoGame.Extras.Graphics.Sprites;

    public class AnimatedSpriteComponent : AnimatedSprite, IEntityComponent
    {
        public AnimatedSpriteComponent(SpriteSheet spriteSheet, string playAnimation) : base(spriteSheet, playAnimation)
        {
        }
    }
}
