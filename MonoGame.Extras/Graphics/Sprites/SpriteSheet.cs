namespace MonoGame.Extras.Graphics.Sprites
{
    using MonoGame.Extras.Graphics.TextureAtlases;
    using System.Collections.Generic;
    using System.Linq;

    public class SpriteSheet
    {
        #region Constructors

        public SpriteSheet()
        {
            Cycles = new Dictionary<string, SpriteSheetAnimationCycle>();
        }

        #endregion

        #region Properties

        public TextureAtlas TextureAtlas { get; set; }

        public Dictionary<string, SpriteSheetAnimationCycle> Cycles { get; set; }

        #endregion

        #region Public Methods

        public SpriteSheetAnimation CreateAnimation(string name)
        {
            var cycle = Cycles[name];
            var frames = cycle.Frames
                .Select(f => TextureAtlas[f])
                .ToArray();

            return new SpriteSheetAnimation(name, frames, cycle.FrameDuration, cycle.IsLooping, cycle.IsReversed);
        }

        #endregion
    }
}
