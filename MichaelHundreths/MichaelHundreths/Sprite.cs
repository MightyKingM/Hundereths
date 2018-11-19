using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichaelHundreths
{
    class Sprite
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Color color;
        public float scale;

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,(int)(texture.Width * scale), (int)(texture.Height * scale));
            }
        }
        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            this.color = color;
            this.texture = texture;
            this.position = position;
            scale = 1f;
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, color);
            spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }
    }
}
