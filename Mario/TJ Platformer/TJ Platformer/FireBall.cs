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
    public class FireBall : Object
    {
        bool grounded = false;
        bool hitMario = false;
        public int lifeTime = 0;
        public int lifeTimeTop = 60 * 5;
        public FireBall(Vector2 position, float hspeed, float vspeed, bool hitmario, ContentManager Content)
            : base(position)
        {
            this.position = position;
            this.hspd = 6;
            this.vspd = 6;
            this.spriteName = "FireBall";
            this.animate = false;
            this.animationNumber = 0;
            this.animationTotal = 1;
            this.frameTotal = 1;
            this.hitMario = hitmario;
            this.hspeed = hspeed;
            this.vspeed = vspeed;
            LoadContent(Content);
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
                lifeTime++;
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
                position += new Vector2(hspeed, 0);
                UpdateCollisionRect();
                rotation += hspeed * 4;
                if (grounded == false)
                    vspeed += .5f;
                Rectangle collrect = collision;
                collrect.Y += (int)vspeed;
                bool canMove = true;
                foreach (Block o in Game1.blocks)
                {
                    if (collrect.Intersects(o.collision))
                    {
                        if (position.Y > o.position.Y)
                        {
                            position.Y = o.position.Y + (o.collision.Height / 2) + (collrect.Height / 2);
                            UpdateCollisionRect();
                            vspeed = -vspeed;
                            canMove = false;
                        }
                        if (position.Y < o.position.Y)
                        {
                            position.Y = o.position.Y - (o.collision.Height / 2) - (collrect.Height / 2);
                            UpdateCollisionRect();
                            vspeed = -vspeed + 1;
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
                {
                    position += new Vector2(0, vspeed);
                    UpdateCollisionRect();
                }
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
        }
    }
}
