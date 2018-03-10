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
      public Texture2D Texture { get; set; }
      public SpriteBatch SpriteBatch { get; set; }

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
   }
}
