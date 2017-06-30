using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstAndroidMonoGame
{
    public class ParticleSprite : Sprite
    {
        Vector2 speed;
        public Vector2 Speed
        {
            get { return speed; }
        }
        Random random;
        float angle;
        TimeSpan lifeSpan;
        TimeSpan elapsedTime;
        bool dead;
        public bool Dead
        {
            get { return dead; }
        }

        public ParticleSprite(Texture2D texture, Vector2 position, Color color, Vector2 originatingSpeed, int smallestScale, int largestScale, TimeSpan lifeMin, TimeSpan lifeMax)
            : base(texture, position, color)
        {
            angle = (float)Math.Atan2(originatingSpeed.Y, originatingSpeed.X) + (float)Math.PI;
            random = new Random();
            int degreeDiff = random.Next(0, 30);
            if (random.Next(0, 2) == 0)
            {
                angle -= MathHelper.ToRadians(degreeDiff);
            }
            else
            {
                angle += MathHelper.ToRadians(degreeDiff);
            }
            speed = new Vector2((float)Math.Cos(angle) * originatingSpeed.Length() / 3, (float)Math.Sin(angle) * originatingSpeed.Length() / 3);
            int randomScale = random.Next(smallestScale, largestScale);
            Scale = new Vector2(randomScale, randomScale);
            lifeSpan = TimeSpan.FromMilliseconds(random.Next((int)lifeMin.TotalMilliseconds, (int)lifeMax.TotalMilliseconds));
            dead = false;
        }

        public override void Update(GameTime gameTime)
        {
            Position += speed;
            elapsedTime += gameTime.ElapsedGameTime;
            Alpha = 1 - (float)(elapsedTime.TotalMilliseconds / lifeSpan.TotalMilliseconds);
            if (elapsedTime >= lifeSpan)
            {
                dead = true;
            }

            base.Update(gameTime);
        }
    }
}