using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Flappy_Bird
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Bird Flappy;
        Texture2D pixel;
        Rectangle birdHitbox;
        Vector2 birdPosition;
        Vector2 birdSpeed;
        Random rand;
        List<Obstacle> obstacles;

        TimeSpan timespan;
        bool Lose = false;
        int score;
       
        
        int[] neuronsPerLayer;

        Func<double, double> functionTan;
        Func<double, double> derivativeTan;
        Func<double, double, double> errorFunc;
        Func<double, double, double> errorFunctionDerivative;

        ActivationFunction activationFunction;
        ErrorFunction errorFunction;
        
        (Bird bird, double fitness)[] birds;

        NeuralNetwork network;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //hi
            // TODO: Add your initialization logic here
            rand = new Random();
            timespan= new TimeSpan();
            score = 0;

            birdPosition = new Vector2(100, 100);
            birdSpeed = new Vector2(5, 5);
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.Yellow });
            birdHitbox = new Rectangle((int)birdPosition.X, (int)birdPosition.Y, 50, 50);


            functionTan = (input) => Math.Tanh(input);
            derivativeTan = (input) => 1 - Math.Pow(Math.Tanh(input), 2);

            errorFunc = (output, desired) => Math.Pow(desired - output, 2);
            errorFunctionDerivative = (input, desired) => -2 * (desired - input);

            activationFunction = new ActivationFunction(functionTan, derivativeTan);
            errorFunction = new ErrorFunction(errorFunc, errorFunctionDerivative); 
            neuronsPerLayer = new int[] { 2, 4, 1 };

            network = new NeuralNetwork(activationFunction, errorFunction, neuronsPerLayer);
            Flappy = new Bird(network, birdPosition, pixel, Color.Yellow, birdPosition, birdHitbox);


            obstacles = new List<Obstacle>();
            int pastX = 800/*(int)obstacles[obstacles.Count-1].Bottom.position.X + 100*/;

            Rectangle pipeRectangle1 = new Rectangle(pastX, rand.Next(-150, 0), 50, 250);
            Rectangle pipeRectangle2 = new Rectangle(pastX, pipeRectangle1.Y + pipeRectangle1.Height + 200, 50, 300);
            Pipe topPipe = new Pipe(pixel, pipeRectangle1, Color.Green);
            Pipe bottomPipe = new Pipe(pixel, pipeRectangle2, Color.Green);

            obstacles.Add(new Obstacle(topPipe, bottomPipe));

            birds = new (Bird net, double fitness)[10];
            for (int i = 0; i < birds.Length; i++)
            {
                NeuralNetwork net = new NeuralNetwork(activationFunction, errorFunction, neuronsPerLayer);
                birds[i] = (new Bird(net, new Vector2(100, 100), pixel, Color.Yellow, birdPosition, birdHitbox), fitness);
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Lose == false)
            {
                this.Window.Title = $"{Mouse.GetState().Position}                    {score}";
                timespan += gameTime.ElapsedGameTime;

                for (int i = 0; i < obstacles.Count; i++)
                {
                    obstacles[i].Update();

                    if (obstacles[i].Top.hitbox.X + 60 < 0)
                    {
                        obstacles.RemoveAt(i);
                        score++;
                    }
                    if (timespan.Seconds > 1.5)
                    {
                        int pastX = 800/*(int)obstacles[obstacles.Count-1].Bottom.position.X + 100*/;
                        Rectangle pipeRectangle1 = new Rectangle(pastX, rand.Next(-150, 0), 50, 250);
                        Rectangle pipeRectangle2 = new Rectangle(pastX, pipeRectangle1.Y + pipeRectangle1.Height + 200, 50, 300);
                        Pipe topPipe = new Pipe(pixel, pipeRectangle1, Color.Green);
                        Pipe bottomPipe = new Pipe(pixel, pipeRectangle2, Color.Green);

                        obstacles.Add(new Obstacle(topPipe, bottomPipe));
                        timespan = TimeSpan.Zero;                        
                    }
                }

                

                Lose = Flappy.Collision(obstacles);
                Flappy.Update();

                base.Update(gameTime);
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            Flappy.Draw(spriteBatch);
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}