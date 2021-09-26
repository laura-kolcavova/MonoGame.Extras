namespace MonoGame.Extras.Ecs.Engine.Graphics.Sprites
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Ecs.Systems;

    public class AnimatedSpriteSystem : EntitySystem, IUpdateSystem
    {
        #region Constructors

        public AnimatedSpriteSystem() : base(Aspect
            .All(typeof(AnimatedSpriteComponent)))
        {
        }

        #endregion

        #region Fields

        private ComponentMapper<AnimatedSpriteComponent> _animatedSpriteMapper;

        #endregion

        #region Public Methods

        public override void Initialize(IComponentMapperService componentService)
        {
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSpriteComponent>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                var animatedSprite = _animatedSpriteMapper.Get(entityId);
                animatedSprite.Update(gameTime);
            }
        }

        #endregion
    }
}
