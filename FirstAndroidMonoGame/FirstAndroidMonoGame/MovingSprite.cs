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
    public class MovingSprite : Sprite
    {
        public Vector2 Speed { get; set; }

        public MovingSprite(Texture2D texture, Vector2 position, Color color)
            : base(texture, position, color)
        {
            Speed = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
        }
    }
}