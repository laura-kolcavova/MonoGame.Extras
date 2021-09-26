// -----------------------------------------------------------------------
// <copyright file="MouseStateExtensions.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extras.Input;

    /// <summary>
    /// A set of extensions methods for <see cref="MouseState"/> object.
    /// </summary>
    public static class MouseStateExtensions
    {
        /// <summary>
        /// Gets array of pressed mosue buttons.
        /// </summary>
        /// <param name="mouseState">The current <see cref="MouseState"/> object.</param>
        /// <returns>An array of pressed mouse button.</returns>
        public static MouseButtons[] GetPressedButtons(this MouseState mouseState)
        {
            var pressedButtons = new List<MouseButtons>();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                pressedButtons.Add(MouseButtons.LeftButton);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                pressedButtons.Add(MouseButtons.RightButton);
            }

            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                pressedButtons.Add(MouseButtons.MiddleButton);
            }

            if (mouseState.XButton1 == ButtonState.Pressed)
            {
                pressedButtons.Add(MouseButtons.XButton1);
            }

            if (mouseState.XButton2 == ButtonState.Pressed)
            {
                pressedButtons.Add(MouseButtons.XButton2);
            }

            return pressedButtons.ToArray();
        }
    }
}
