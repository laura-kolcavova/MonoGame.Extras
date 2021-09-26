namespace MonoGame.Extras.Ecs.Engine.Interaction
{
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extras.Input;
    using System;

    public class MouseEventArgs : EventArgs
    {
        public MouseState MouseState { get; set; }
        public int TargetId { get; set; }
        public MouseButtons Button { get; set; }
        public MouseButtons[] Buttons { get; set; }
    }
}
