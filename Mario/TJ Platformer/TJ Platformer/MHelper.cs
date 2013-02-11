using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mario
{
    public static class MHelper
    {
        public static Vector2 LengthDirection(float length, float direction)
        {
            return new Vector2((float)Math.Cos(MathHelper.ToRadians(direction)) * length, (float)Math.Sin(MathHelper.ToRadians(direction)) * length);
        }

        public static float PointDirection(float x1, float y1, float x2, float y2)
        {
            return ((MathHelper.ToDegrees((float)Math.Atan2(y1 - y2, x1 - x2))) % 360) - 180;
        }

        public static Vector2 Midpoint(float x1, float y1, float x2, float y2)
        {
            return new Vector2((x1 + x2) / 2, (y1 + y2) / 2);
        }

        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return (float)(Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }
    }
}
