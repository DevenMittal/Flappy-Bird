using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Flappy_Bird
{
    internal class Bird
    {
        public Vector2 speed;
        public Texture2D image;
        public Color tint;
        public Vector2 position;
        public Rectangle hitbox;
        public Vector2 origin => new Vector2(hitbox.Width / 2, hitbox.Height / 2);

        const float gravVal = 12;
        const float gravity = 0.7f;
        bool isJumping = false;
        public NeuralNetwork brain;
        int fitness;
        public bool isDead;

        public Bird(NeuralNetwork brain, Vector2 speed, Texture2D image, Color tint, Vector2 position, Rectangle hitbox)
        {
            this.speed = speed;
            this.image = image;
            this.tint = tint;
            this.position = position;
            this.hitbox = hitbox;
            this.brain = brain;
            isDead = false;
        }

        public void Update(List<Obstacle> obstacles)
        {
            if (isDead == false)
            {


                hitbox.Y += 3;
                //if (Keyboard.GetState().IsKeyDown(Keys.Space) && isJumping == false)
                float XDistance = 0f;
                float YDistance = 0f;
                for (int i = 0; i < obstacles.Count; i++)
                {
                    if (obstacles[i].Top.hitbox.X > hitbox.X)
                    {
                        XDistance = obstacles[i].Top.hitbox.X - hitbox.X;
                        YDistance = obstacles[i].Top.hitbox.Y + obstacles[i].Top.hitbox.Height+70 - hitbox.Y;
                        break;
                    }
                }

                if (isJumping == false && brain.Compute(new double[] { (double)XDistance, (double)YDistance })[0] > .5)
                {
                    //add lerp
                    if (hitbox.Y - 60 < 0)
                    {
                        hitbox.Y = 0;
                    }
                    else
                    {
                        hitbox.Y -= 60;
                    }
                    isJumping = true;
                }
                //if (Keyboard.GetState().IsKeyUp(Keys.Space) && isJumping == true)

                if (isJumping == true)
                {
                    isJumping = false;
                }
            }
            ///hitbox = new Rectangle((int)position.X, (int)position.Y, hitbox.Width, hitbox.Height);
        }

        public bool Collision(List<Obstacle> obstacles)
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (hitbox.Intersects(obstacles[i].Top.hitbox))
                {
                    return true;
                }
                if (hitbox.Intersects(obstacles[i].Bottom.hitbox))
                {
                    return true;
                }
            }
            //if (hitbox.Y + hitbox.Height < 0)
            //{
            //    return true;
            //}
            if (hitbox.Y > 480)
            {
                return true;
            }
            return false;
        }


        public void Draw(SpriteBatch sb)
        {
            if (!isDead)
            {
                sb.Draw(image, hitbox, tint);
            }
        }
    }
}
