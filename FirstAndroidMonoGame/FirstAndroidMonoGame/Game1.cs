using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace FirstAndroidMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MovingSprite sprite;
        SpriteFont font;
        float spriteGrowSpeed;
        byte rColorChange, gColorChange, bColorChange;
        KeyboardState ks;
        TouchCollection touch;
        Vector2 spriteSpeed;
        List<ParticleSprite> particles;
        TimeSpan generateSpriteTime;
        TimeSpan elapsedTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprite = new MovingSprite(Content.Load<Texture2D>("WhitePixel"), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            //sprite.Position = Vector2.Zero;
            sprite.Scale = new Vector2(300, 300);
            sprite.SetCenterOrigin();
            spriteGrowSpeed = 2;
            font = Content.Load<SpriteFont>("Font");
            rColorChange = 1;
            gColorChange = 1;
            bColorChange = 1;
            sprite.Color = new Color(100, 255, 255);
            spriteSpeed = Vector2.Zero;
            particles = new List<ParticleSprite>();
            generateSpriteTime = TimeSpan.FromMilliseconds(20);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            touch = TouchPanel.GetState();

            if (touch.Count > 0 && touch[0].State == TouchLocationState.Moved)
            {
                spriteSpeed = touch[0].Position - sprite.Position;
                spriteSpeed.X /= 12;
                spriteSpeed.Y /= 12;
                sprite.Speed = spriteSpeed;
                sprite.Update(gameTime);

                elapsedTime += gameTime.ElapsedGameTime;

                if(elapsedTime >= generateSpriteTime && spriteSpeed.Length() > 0.005f)
                {
                    particles.Add(new ParticleSprite(Content.Load<Texture2D>("WhitePixel"), sprite.Position, sprite.Color, spriteSpeed, 15, 40, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(2000)));
                    elapsedTime = new TimeSpan();
                }
            }

            for(int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
                if(particles[i].Dead)
                {
                    particles.Remove(particles[i]);
                    i--;
                }
            }

            sprite.Rotation += .025f;
            sprite.Scale = new Vector2(sprite.Scale.X + spriteGrowSpeed, sprite.Scale.Y + spriteGrowSpeed);
            if (sprite.Scale.X >= 310 || sprite.Scale.X <= 100)
            {
                spriteGrowSpeed = -spriteGrowSpeed;
            }

            if (sprite.Color.R < 255 && sprite.Color.G == 0 && sprite.Color.B == 0)
            {
                sprite.Color = new Color(sprite.Color.R + rColorChange, sprite.Color.G, sprite.Color.B);
            }
            else if (sprite.Color.R == 255 && sprite.Color.G < 255 && sprite.Color.B == 0)
            {
                sprite.Color = new Color(sprite.Color.R, sprite.Color.G + gColorChange, sprite.Color.B);
            }
            else if (sprite.Color.R == 255 && sprite.Color.G == 255 && sprite.Color.B < 255)
            {
                sprite.Color = new Color(sprite.Color.R, sprite.Color.G, sprite.Color.B + bColorChange);
            }
            else if (sprite.Color.R > 0 && sprite.Color.G == 255 && sprite.Color.B == 255)
            {
                sprite.Color = new Color(sprite.Color.R - rColorChange, sprite.Color.G, sprite.Color.B);
            }
            else if (sprite.Color.R == 0 && sprite.Color.G > 0 && sprite.Color.B == 255)
            {
                sprite.Color = new Color(sprite.Color.R, sprite.Color.G - gColorChange, sprite.Color.B);
            }
            else if (sprite.Color.R == 0 && sprite.Color.G == 0 && sprite.Color.B > 0)
            {
                sprite.Color = new Color(sprite.Color.R, sprite.Color.G, sprite.Color.B - bColorChange);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MistyRose);
            spriteBatch.Begin(); foreach (ParticleSprite particle in particles)
            {
                particle.Draw(spriteBatch);
            }
            sprite.Draw(spriteBatch);
            spriteBatch.DrawString(font, touch.Count.ToString(), Vector2.Zero, Color.White,0f, Vector2.Zero, 5f, SpriteEffects.None, 1f);
            if(touch.Count > 0)
            {
                spriteBatch.DrawString(font, touch[0].State.ToString(), new Vector2(0, 50), Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 1f);
            }
            spriteBatch.DrawString(font, spriteSpeed.Length().ToString(), new Vector2(0, 100), Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 1f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
