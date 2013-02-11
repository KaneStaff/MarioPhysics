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
    class CoinBlock : Block
    {
        public CoinBlock(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.animate = true;
            this.frame = 0;
            this.spriteName = "QuestionBlock";
            this.frameTotal = 5;
            this.animationTime = 8;
        }

        public override void EmitPrize(Mario mario)
        {
            if (animate == true)
            {
                animate = false;
                frame = 4;
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
            if (frame > 3 && animate == true)
                frame = 0;
        }
    }
}
