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
    public class Object
    {
        public Texture2D texture;
        public Vector2 position;
        public float hspeed = 0;
        public float hspd = 0;
        public float vspeed = 0;
        public float vspd = 0;
        public float direction = 0;
        public string spriteName = "Carl";
        public Rectangle area;
        public int frame = 0;
        public bool animate = true;
        public int animationTime = 30;
        public int animationTimer = 0;
        public int animationNumber = 0;
        public int animationTotal = 1;
        public int frameTotal =  2;
        public float rotation = 0;
        public float scale = 1;
        public Rectangle collision;
        public bool alive = true;

        public Object(Vector2 position)
        {
            this.position = position;
        }

        public virtual void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(this.spriteName);
            area = new Rectangle(0, 0, texture.Width / frameTotal, texture.Height / animationTotal);
        }

        public void UpdateCollisionRect()
        {
            collision.X = (int)position.X - area.Width / 2;
            collision.Y = (int)position.Y - area.Height / 2;
        }

        public virtual void Update()
        {
            if (alive)
            {
                if (animate)
                {
                    animationTimer++;
                    if (animationTimer >= animationTime)
                    {
                        frame++;
                        animationTimer = 0;
                    }
                    if (frame >= frameTotal && animate)
                        frame = 0;
                }
                UpdateCollisionRect();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                Rectangle place = new Rectangle(frame * area.Width, animationNumber * area.Height, area.Width, area.Height);
                Vector2 center = new Vector2(area.Width / 2, area.Height / 2);
                spriteBatch.Draw(texture, position, place, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            }
        }
    }
}
