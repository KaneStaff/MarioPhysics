using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Mario
{
    public static class InputDevice
    {
        static MouseState mouse = Mouse.GetState();
        static MouseState mousePrev = Mouse.GetState();
        static KeyboardState keyboard = Keyboard.GetState();
        static KeyboardState keyboardPrev = Keyboard.GetState();
        public static Vector2 position;

        public static void Update()
        {
            mousePrev = mouse;
            mouse = Mouse.GetState();
            keyboardPrev = keyboard;
            keyboard = Keyboard.GetState();
            position = new Vector2(mouse.X, mouse.Y);
        }

        public static bool IsLeftMouseDown()
        {
            if (mouse.LeftButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static bool IsRightMouseDown()
        {
            if (mouse.RightButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static bool IsLeftMousePressed()
        {
            if (mouse.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released)
                return true;
            else return false;
        }

        public static bool IsLeftMouseReleased()
        {
            if (mouse.LeftButton == ButtonState.Released && mousePrev.LeftButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static bool IsRightMousePressed()
        {
            if (mouse.RightButton == ButtonState.Pressed && mousePrev.RightButton == ButtonState.Released)
                return true;
            else return false;
        }

        public static bool IsrightMouseReleased()
        {
            if (mouse.RightButton == ButtonState.Released && mousePrev.RightButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static bool IsKeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
                return true;
            else return false;
        }

        public static bool IsKeyPressed(Keys key)
        {
            if (keyboard.IsKeyDown(key) && !keyboardPrev.IsKeyDown(key))
                return true;
            else return false;
        }

        public static bool IsKeyReleased(Keys key)
        {
            if (!keyboard.IsKeyDown(key) && keyboardPrev.IsKeyDown(key))
                return true;
            else return false;
        }
    }
}
