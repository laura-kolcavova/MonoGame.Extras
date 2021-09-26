namespace MonoGame.Extras.Graphics.Sprites
{
    using Microsoft.Xna.Framework;
    using System;

    public class AnimatedSprite : Sprite
    {
        private readonly SpriteSheet _spriteSheet;

        private SpriteSheetAnimation _currentAnimation;

        public AnimatedSprite(SpriteSheet spriteSheet, string playAnimation = null)
            : base(spriteSheet.TextureAtlas[0])
        {
            _spriteSheet = spriteSheet;

            if (playAnimation != null)
                Play(playAnimation);
        }

        public SpriteSheetAnimation CurrentAnimation => _currentAnimation;

        public void Play(string name, Action onCompleted = null)
        {
            _currentAnimation = _spriteSheet.CreateAnimation(name);

            if(onCompleted != null)
                _currentAnimation.OnCompleted += onCompleted;
        }

        public void Pause()
        {
            _currentAnimation?.Pause();
        }

        public void Stop()
        {
            _currentAnimation?.Stop();
        }

        public void Rewind()
        {
            _currentAnimation?.Rewind();
        }

        public void Update(GameTime gameTime)
        {
            if (_currentAnimation?.IsComplete == false)
            {
                _currentAnimation.Update(gameTime);
                TextureRegion = _currentAnimation.CurrentFrame;
            }
        }
    }
}
