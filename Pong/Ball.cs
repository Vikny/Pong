//Ball.cs
//contains properties and movement of the ball
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
   public class Ball: Sprite
   {
      Game1 game;

      public Ball(Texture2D texture, Vector2 position, SpriteBatch spriteBatch,Game1 game) 
         : base(texture, position, spriteBatch)
      {
      }

      
     


   }
}
