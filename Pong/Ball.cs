//Ball.cs
//contains properties and movement of the ball
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
   public class Ball: Sprite
   {
      

      public Ball(Texture2D texture, Vector2 position, SpriteBatch spriteBatch,Game1 game) 
         : base(texture, position, spriteBatch)
      {
      }


   }
}
