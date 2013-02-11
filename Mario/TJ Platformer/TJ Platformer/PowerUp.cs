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
    public class PowerUp : Object
    {
        bool grounded = false;
        bool aliveprev;
        public PowerUp(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.hspd = 5;
            this.vspd = 4;
            this.spriteName = "PowerUps";
            this.animate = false;
            this.animationNumber = 0;
            this.animationTotal = 1;
            this.frameTotal = 2;
            this.alive = false;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            collision = new Rectangle(0, 0, area.Width, area.Height);
        }

        public void Update(Mario mario)
        {
            if (alive)
            {
                if (aliveprev != alive)
                {
                    frame = mario.powerLevel;
                }
                foreach (Block o in Game1.blocks)
                {
                    Rectangle collisionX = collision;
                    collisionX.X += (int)hspeed;
                    if (collisionX.Intersects(o.collision))
                    {
                        if (hspeed < 0)
                        {
                            position.X = o.position.X + (o.collision.Width / 2) + (collisionX.Width / 2);
                            UpdateCollisionRect();
                            hspeed = -hspeed;
                            break;
                        }
                        if (hspeed > 0)
                        {
                            position.X = o.position.X - (o.collision.Width / 2) - (collisionX.Width / 2);
                            UpdateCollisionRect();
                            hspeed = -hspeed;
                            break;
                        }
                    }
                }
                hspeed = MathHelper.Clamp(hspeed, -hspd, hspd);
                if (hspeed != 0)
                {
                    if (hspeed > 0)
                        hspeed -= .02f;
                    if (hspeed < 0)
                        hspeed += .02f;
                }
                if (Math.Abs(hspeed) < .02f)
                {
                    hspeed = 0;
                }
                position += new Vector2(hspeed, 0);
                UpdateCollisionRect();
                rotation += hspeed * 4;
                if (grounded == false)
                    vspeed += .5f;
                collision.Y += (int)vspeed;
                bool canMove = true;
                foreach (Block o in Game1.blocks)
                {
                    if (collision.Intersects(o.collision))
                    {
                        if (position.Y > o.position.Y)
                        {
                            position.Y = o.position.Y + (o.collision.Height / 2) + (collision.Height / 2);
                            UpdateCollisionRect();
                            vspeed = -vspeed;
                            canMove = false;
                        }
                        if (position.Y < o.position.Y)
                        {
                            position.Y = o.position.Y - (o.collision.Height / 2) - (collision.Height / 2);
                            UpdateCollisionRect();
                            if (vspeed >= 1)
                                vspeed = -vspeed + 1;
                            else vspeed = 0;
                            canMove = false;
                        }
                    }
                    Rectangle collisionRectangle = collision;
                    collisionRectangle.Y += 1;
                    if (collisionRectangle.Intersects(o.collision))
                    {
                        grounded = true;
                        break;
                    }
                    grounded = false;
                }
                vspeed = MathHelper.Clamp(vspeed, -vspd, vspd);
                if (canMove)
                    position += new Vector2(0, vspeed);
                UpdateCollisionRect();
                if (position.X - (area.Width / 2) < 0)
                {
                    position.X -= hspeed;
                    UpdateCollisionRect();
                    hspeed = -hspeed;
                }
                if (position.X + (area.Width / 2) > Game1.graphics.PreferredBackBufferWidth)
                {
                    position.X -= hspeed;
                    UpdateCollisionRect();
                    hspeed = -hspeed;
                }
                base.Update();
            }
            aliveprev = alive;
        }
    }
}
