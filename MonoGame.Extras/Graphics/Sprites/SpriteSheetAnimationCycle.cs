namespace MonoGame.Extras.Graphics.Sprites
{
    using System.Collections.Generic;

    public class SpriteSheetAnimationCycle
    {
        #region Constructors

        public SpriteSheetAnimationCycle()
        {
            Frames = new List<int>();
            FrameDuration = 0.2f;
        }

        public SpriteSheetAnimationCycle(params int[] frames)
            : this()
        {
            foreach (int frame in frames)
                Frames.Add(frame);
        }

        public SpriteSheetAnimationCycle(int from, int to)
            : this()
        {
            for (int i = from; i <= to; i++)
            {
                Frames.Add(i);
            }
        }

        #endregion

        #region Properties

        public List<int> Frames { get; set; }

        public bool IsLooping { get; set; }

        public bool IsReversed { get; set; }

        public float FrameDuration { get; set; }

        #endregion
    }
}
