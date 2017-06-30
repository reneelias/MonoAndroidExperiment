using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAndroidMonoGame
{
    public class Sprite
    {
        protected Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public int Width
        {
            get { return (int)(texture.Width * Scale.X); }
        }
        public int Height
        {
            get { return (int)(texture.Height * Scale.Y); }
        }

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public float Alpha { get; set; }

        protected Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        
        public Vector2 Scale { get; set; }

        public Vector2 Origin { get; set; }

        public void SetCenterOrigin()
        {
            Origin = new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f);
        }

        public float Rotation { get; set; }

        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            hitbox = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            Scale = new Vector2(1, 1);
            Origin = Vector2.Zero;
            Alpha = 1;
        }

        public virtual void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, color);
            spriteBatch.Draw(texture, position, null, color * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 1f);
        }
    }
}
