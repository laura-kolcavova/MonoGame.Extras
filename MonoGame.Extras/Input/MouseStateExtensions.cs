namespace MonoGame.Extras.Input
{
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    public static class MouseStateExtensions
    {
        public static MouseButtons[] GetPressedButtons(this MouseState mouseState)
        {
            var pressedButtons = new List<MouseButtons>();

            if (mouseState.LeftButton == ButtonState.Pressed)
                pressedButtons.Add(MouseButtons.LeftButton);

            if (mouseState.RightButton == ButtonState.Pressed)
                pressedButtons.Add(MouseButtons.RightButton);

            if (mouseState.MiddleButton == ButtonState.Pressed)
                pressedButtons.Add(MouseButtons.MiddleButton);

            if (mouseState.XButton1 == ButtonState.Pressed)
                pressedButtons.Add(MouseButtons.XButton1);

            if (mouseState.XButton2 == ButtonState.Pressed)
                pressedButtons.Add(MouseButtons.XButton2);

            return pressedButtons.ToArray();
        }
    }
}
