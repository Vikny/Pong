//Sprite.cs
//it holds the property of sprite position, texture and spritebatch

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
   public class Sprite
   {
      public Vector2 Position { get; set; }
      public Vector2 Movement { get; set; }
      public float speed;
      public Texture2D Texture { get; set; }
      public SpriteBatch SpriteBatch { get; set; }
      public Rectangle Bounds
      {
         get => new Rectangle((int)Position.X, (int)Position.Y, 
            Texture.Width, Texture.Height);
         
      }

      public Sprite(Texture2D texture, Vector2 position, SpriteBatch spriteBatch)
      {
         Texture = texture;
         Position = position;
         SpriteBatch = spriteBatch;
      }

      public virtual void Draw()
      {
         SpriteBatch.Draw(Texture, Position, Color.Green);
      }

      #region Collision
      public bool IsTouchingLeft(Sprite sprite)
      {
         return this.Bounds.Right > sprite.Bounds.Left &&
            this.Bounds.Left < sprite.Bounds.Left &&
            this.Bounds.Bottom > sprite.Bounds.Top &&
            this.Bounds.Top < sprite.Bounds.Bottom;
      }
      public bool IsTouchingRight(Sprite sprite)
      {
         return this.Bounds.Left < sprite.Bounds.Right &&
            this.Bounds.Right > sprite.Bounds.Right &&
            this.Bounds.Bottom > sprite.Bounds.Top &&
            this.Bounds.Top < sprite.Bounds.Bottom;
      }
      public bool IsTouchingTop(Sprite sprite)
      {
         return this.Bounds.Bottom > sprite.Bounds.Top &&
            this.Bounds.Top < sprite.Bounds.Top &&
            this.Bounds.Left < sprite.Bounds.Right &&
            this.Bounds.Right > sprite.Bounds.Left;
      }
      public bool IsTouchingBottom(Sprite sprite)
      {
         return this.Bounds.Top < sprite.Bounds.Bottom &&
            this.Bounds.Bottom > sprite.Bounds.Bottom &&
            this.Bounds.Left < sprite.Bounds.Right &&
            this.Bounds.Right > sprite.Bounds.Left;
      }
      #endregion
   }
}
