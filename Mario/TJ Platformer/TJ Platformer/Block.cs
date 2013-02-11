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
    class Block : Object
    {
        public Block(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.animate = false;
            this.frame = 3;
            this.spriteName = "Blocks";
            this.frameTotal = 4;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            collision = new Rectangle(0, 0, area.Width, area.Height);
        }

        public virtual void EmitPrize(Mario mario)
        {
        }

        public virtual void BlockBreak()
        {
            Game1.blocks.Remove(this);
        }
    }
}
