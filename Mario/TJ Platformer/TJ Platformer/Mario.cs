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
    public class Mario : Object
    {
        public List<FireBall> fireBalls = new List<FireBall>();
        public int powerLevel = 0;
        //if powerLevelCap is changed, change frameTotal in PowerUp
        public int powerLevelCap = 2;
        int isFacing = 1;
        bool grounded = false;
        bool sprint = false;
        int spacetime = 100;
        public Mario(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.hspd = 2;
            this.vspd = 4;
            this.spriteName = "Mario";
            this.animate = true;
            this.animationNumber = 0;
            this.animationTime = (int)(10 / hspd);
            this.animationTotal = 3;
            this.frameTotal = 2;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            collision = new Rectangle(0, 0, area.Width - 4, area.Height);
        }

        public void Update(ContentManager Content)
        {
            if (powerLevel == 0 && spriteName != "Mario")
            {
                frameTotal = 2;
                spriteName = "Mario";
                LoadContent(Content);
                position.Y += 5;
                UpdateCollisionRect();
            }
            if (powerLevel == 1 && spriteName != "BigMario")
            {
                frameTotal = 3;
                if (spriteName != "Mario")
                    position.Y -= 1;
                if (spriteName == "Mario")
                    position.Y -= area.Height / 2;
                UpdateCollisionRect();
                spriteName = "BigMario";
                LoadContent(Content);
            }
            if (powerLevel == 2 && spriteName != "FireMario")
            {
                frameTotal = 3;
                if (spriteName != "Mario")
                    position.Y -= 1;
                if (spriteName == "Mario")
                    position.Y -= area.Height / 2;
                UpdateCollisionRect();
                spriteName = "FireMario";
                LoadContent(Content);
            }
            if (InputDevice.IsLeftMousePressed())
            {
                Vector2 mousedistance = MHelper.LengthDirection(MHelper.Distance(position.X, position.Y, InputDevice.position.X, InputDevice.position.Y), MHelper.PointDirection(position.X, position.Y, InputDevice.position.X, InputDevice.position.Y));
                if (fireBalls.Count() < 4 && powerLevel == 2)
                    fireBalls.Add(new FireBall(position, mousedistance.X / 12, mousedistance.Y / 12, false, Content));
            }
            if (InputDevice.IsKeyPressed(Keys.Down))
                powerLevel--;
            if (InputDevice.IsKeyPressed(Keys.Up))
                powerLevel++;
            if (InputDevice.IsKeyDown(Keys.LeftShift))
            {
                hspd = 5;
                animationTime = (int)(15 / hspd);
                sprint = true;
            }
            else
            {
                hspd = 2;
                animationTime = (int)(10 / hspd);
                sprint = false;
            }
            if (InputDevice.IsKeyDown(Keys.D) && !InputDevice.IsKeyDown(Keys.A) && hspeed < hspd)
            {
                hspeed += .075f;
                isFacing = 1;
            }
            if (InputDevice.IsKeyDown(Keys.A) && !InputDevice.IsKeyDown(Keys.D) && hspeed > -hspd)
            {
                hspeed -= .075f;
                isFacing = 0;
            }
            if (!InputDevice.IsKeyDown(Keys.A) == !InputDevice.IsKeyDown(Keys.D))
            {
                if (hspeed < 0 && hspeed != 0)
                    if (hspeed > -3)
                        hspeed += .1f;
                    else hspeed += .25f;
                if (hspeed > 0 && hspeed != 0)
                    if (hspeed < 3)
                        hspeed -= .1f;
                    else hspeed -= .25f;
                if (hspeed < .1f && hspeed > -.1f)
                    hspeed = 0;
            }
            if ((isFacing == 0 && hspeed > 0) || (isFacing == 1 && hspeed < 0))
            {
                if (sprint)
                {
                    if (hspeed < 0 && hspeed != 0)
                        if (hspeed > -3)
                            hspeed += .1f;
                        else hspeed += .25f;
                    if (hspeed > 0 && hspeed != 0)
                        if (hspeed < 3)
                            hspeed -= .1f;
                        else hspeed -= .25f;
                }
                animationNumber = 2;
            }
            else animationNumber = 0;
            if (hspeed == 0)
            {
                animate = false;
                frame = 0;
            }
            else animate = true;
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
                        hspeed = 0;
                    }
                    if (hspeed > 0)
                    {
                        position.X = o.position.X - (o.collision.Width / 2) - (collisionX.Width / 2);
                        UpdateCollisionRect();
                        hspeed = 0;
                    }
                }
            }
            position += new Vector2(hspeed, 0);
            if (InputDevice.IsKeyPressed(Keys.Space) && grounded == true)
                spacetime = 0;
            if (InputDevice.IsKeyDown(Keys.Space))
            {
                spacetime++;
                if (spacetime < 20)
                    vspeed -= vspd;
            }
            if (InputDevice.IsKeyReleased(Keys.Space))
                spacetime = 100;
            vspeed = MathHelper.Clamp(vspeed, -vspd, vspd);
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
                        spacetime = 100;
                        canMove = false;
                        o.EmitPrize(this);
                    }
                    if (position.Y < o.position.Y)
                    {
                        position.Y = o.position.Y - (o.collision.Height / 2) - (collision.Height / 2);
                        UpdateCollisionRect();
                        vspeed = 0;
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
            if (grounded == false)
            {
                vspeed += .5f;
                animate = false;
                if (vspeed > 0)
                {
                    animationNumber = 1;
                    frame = 1;
                }
                else
                {
                    animationNumber = 1;
                    frame = 0;
                }
            }
            if (canMove)
                position += new Vector2(0, vspeed);
            if (position.X - (area.Width / 2) < 0)
            {
                position.X -= hspeed;
                UpdateCollisionRect();
                hspeed = 0;
            }
            if (position.X + (area.Width / 2) > Game1.graphics.PreferredBackBufferWidth)
            {
                position.X -= hspeed;
                UpdateCollisionRect();
                hspeed = 0;
            }
            foreach (Coin c in Game1.coins)
            {
                if (collision.Intersects(c.collision) && c.alive == true)
                {
                    c.alive = false;
                }
            }
            foreach (PowerUp c in Game1.powerUps)
            {
                if (collision.Intersects(c.collision) && c.alive == true)
                {
                    c.alive = false;
                    if (powerLevel < powerLevelCap && powerLevel < c.frame + 1)
                        powerLevel += 1;
                }
            }
            base.Update();
            List<FireBall> removing = new List<FireBall>() ;
            foreach (FireBall f in fireBalls)
            {
                f.Update(this);
                if (f.lifeTime >= f.lifeTimeTop)
                    removing.Add(f);
            }
            foreach (FireBall f in removing)
                fireBalls.Remove(f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle place = new Rectangle(frame * area.Width, animationNumber * area.Height, area.Width, area.Height);
            Vector2 center = new Vector2(area.Width / 2, area.Height / 2);
            Vector2 posround = new Vector2((int)position.X, (int)position.Y);
            if (isFacing == 1)
                spriteBatch.Draw(texture, posround, place, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            else spriteBatch.Draw(texture, posround, place, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.FlipHorizontally, 0);
            foreach (FireBall f in fireBalls)
                f.Draw(spriteBatch);
            spriteBatch.DrawString(Game1.timesNewRoman, Convert.ToString(powerLevel), Vector2.Zero, Color.Black);
        }
    }
}
