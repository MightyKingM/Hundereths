using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichaelHundreths
{
    class Circle : Sprite
    {

        public int size = 0;
        public int xspeed;
        public int yspeed;

        public float Radius
        {
            get { return Hitbox.Width / 2; }
        }


        public Vector2 Center
        {
            get { return new Vector2(Hitbox.X + Radius, Hitbox.Y + Radius); }
        }

        public bool Hover(Rectangle mouse)
        {
            if (Hitbox.Intersects(mouse))
            {
                return true;
            }
            return false;
        }
        public Circle(Texture2D texture, Vector2 position, Color color, int speed)
        : base(texture, position, color)
        {
            xspeed = speed;
            yspeed = speed;
        }
        public void DrawText(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, size.ToString(), Center - font.MeasureString(size.ToString()) / 2, Color.Black);
        }


        public void move(int screenheight, int screenwidth)
        {
            position.X = position.X + xspeed;
            position.Y = position.Y + yspeed;
            if (position.X + texture.Width * scale >= screenwidth)
            {
                xspeed = -Math.Abs(xspeed);
            }
            if (position.X <= 0)
            {
                xspeed = Math.Abs(xspeed);
            }
            if (position.Y + texture.Height * scale >= screenheight)
            {
                yspeed = -Math.Abs(yspeed);
            }
            if (position.Y <= 0)
            {
                yspeed = Math.Abs(yspeed);
            }
        }

        public bool Intersects(Vector2 pos)
        {
            Vector2 distance = Center - pos;
            if(distance.Length() <= Radius)
            {
                return true;
            }
            return false;
        }
        public void grow(int amount)
        {
            size = size + amount;
            scale += .01f;
        }

        public bool Bounce(Circle other)
        {
            Vector2 distance = Center - other.Center;
            if(distance.Length() <= Radius + other.Radius)
            {
                int otherx = other.xspeed;
                int othery = other.yspeed;
                other.xspeed = xspeed;
                other.yspeed = yspeed;
                yspeed = othery;
                xspeed = otherx;
                return true;
            }
            return false;
        }
    }
}
