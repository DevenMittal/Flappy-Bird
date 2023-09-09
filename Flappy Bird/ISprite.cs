using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    internal interface ISprite
    {
        Texture2D image { get; set; }
        Color tint { get; set; }
        Rectangle rect { get; set; }
        Vector2 position { get; set; }
        Vector2 speed { get; set; }

        void Draw(SpriteBatch sb);


    }
}
