using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Flappy_Bird
{
    internal class Pipe
    {        
        public Vector2 speed;
        public Texture2D image;
        public Color tint;
        public Vector2 position;
        public Rectangle hitbox;
        

        public Pipe(Vector2 speed, Texture2D image, Color tint, Vector2 position, Rectangle hitbox)
        {
            this.speed = speed;
            this.image = image;
            this.tint = tint;
            this.position = position;
            this.hitbox = hitbox;
        }
        public Pipe(Texture2D image, Rectangle hitbox, Color tint)
        {
            this.image = image;
            this.tint = tint;
            this.hitbox = hitbox;
        }

        public void Update()
        { 
            hitbox.X -= 4;
        }




    }
}
