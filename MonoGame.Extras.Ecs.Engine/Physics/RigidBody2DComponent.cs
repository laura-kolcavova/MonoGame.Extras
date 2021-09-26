namespace MonoGame.Extras.Ecs.Engine.Physics
{
    using Microsoft.Xna.Framework;
    using System;

    public class RigidBody2DComponent : IEntityComponent
    {
        public float Speed { get; set; }

        /// <summary>
        /// An angle measured in degrees
        /// </summary>
        public float Angle { get; set; }

        public Vector2 Direction
        {
            get
            {
                float radians = MathHelper.ToRadians(Angle);
                float x = (float)Math.Cos(radians);
                float y = (float)Math.Sin(radians);
                return new Vector2(x, y);
            }
        }

        public Vector2 Velocity => Direction * Speed;
    }
}
