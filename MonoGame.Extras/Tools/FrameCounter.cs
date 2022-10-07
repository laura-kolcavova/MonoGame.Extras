// -----------------------------------------------------------------------
// <copyright file="FrameCounter.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.Tools
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Frame counter tool.
    /// </summary>
    public class FrameCounter
    {
        /// <summary>
        /// Gets maximum samples count.
        /// </summary>
        public const int MaximumSamples = 100;

        private readonly Queue<float> sampleBuffer = new Queue<float>();

        /// <summary>
        /// Gets total frames count.
        /// </summary>
        public long TotalFrames { get; private set; }

        /// <summary>
        /// Gets total secons count.
        /// </summary>
        public float TotalSeconds { get; private set; }

        /// <summary>
        /// Gets average frames per second value.
        /// </summary>
        public float AverageFramesPerSecond { get; private set; }

        /// <summary>
        /// Gets current frames per second value.
        /// </summary>
        public float CurrentFramesPerSecond { get; private set; }

        /// <summary>
        /// Updates frame counter.
        /// </summary>
        /// <param name="deltaTime">Delta time.</param>
        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (sampleBuffer.Count > MaximumSamples)
            {
                sampleBuffer.Dequeue();
                AverageFramesPerSecond = sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }
    }
}
