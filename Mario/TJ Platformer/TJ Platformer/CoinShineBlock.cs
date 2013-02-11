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
    class CoinShineBlock : Block
    {
        int coinnumber;
        public CoinShineBlock(Vector2 position, int randomlow, int randomhigh)
            : base(position)
        {
            this.position = position;
            this.animate = true;
            this.frame = 0;
            this.spriteName = "CoinBrick";
            this.frameTotal = 7;
            this.animationTime = 8;
            coinnumber = Game1.rand.Next(randomlow, randomhigh);
        }

        public CoinShineBlock(Vector2 position, int coinemitnumber)
            : base(position)
        {
            this.position = position;
            this.animate = true;
            this.frame = 0;
            this.spriteName = "CoinBrick";
            this.frameTotal = 7;
            this.animationTime = 8;
            coinnumber = coinemitnumber;
        }

        public CoinShineBlock(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.animate = true;
            this.frame = 0;
            this.spriteName = "CoinBrick";
            this.frameTotal = 7;
            this.animationTime = 8;
            Random rand = new Random();
            coinnumber = rand.Next(5, 8);
        }

        public override void EmitPrize(Mario mario)
        {
            if (coinnumber > 0)
            {
                coinnumber--;
                if (coinnumber == 0)
                {
                    animate = false;
                    frame = 6;
                }
                foreach (Coin c in Game1.coins)
                {
                    if (c.alive == false)
                    {
                        c.alive = true;
                        c.position = new Vector2(position.X, position.Y - area.Height - 2);
                        c.hspeed = mario.hspeed;
                        c.vspeed = -4;
                        break;
                    }
                }
            }

        }

        public override void Update()
        {
            base.Update();
            if (frame > 5 && animate == true)
                frame = 0;
        }
    }
}
