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
    class ShineBlock : Block
    {
        public ShineBlock(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.animate = true;
            this.frame = 0;
            this.spriteName = "Brick";
            this.frameTotal = 6;
            this.animationTime = 8;
        }

        public override void EmitPrize(Mario mario)
        {
            if (mario.powerLevel > 0)
                BlockBreak();
        }
    }
}
