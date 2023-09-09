using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Flappy_Bird
{
    internal class Obstacle
    {
        public Pipe Top;
        public Pipe Bottom;

        public Obstacle(Pipe top, Pipe bottom)
        {
            Top = top;
            Bottom = bottom;
        }

        public void Update()
        {
            Top.Update(); 
            Bottom.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Top.image, Top.hitbox, Top.tint);
            sb.Draw(Bottom.image, Bottom.hitbox, Bottom.tint);
        }
    }
}
