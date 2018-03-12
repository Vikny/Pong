using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
   public class Player: Sprite
   {
      public Player(Texture2D texture, Vector2 position, SpriteBatch spriteBatch) 
         : base(texture, position, spriteBatch)
      {
      }




      public void Update(GameTime gameTime)
      {
         playerMove();

      }

      public void playerMove()
      {
         KeyboardState keyBoardState = Keyboard.GetState();
         if (keyBoardState.IsKeyDown(Keys.Down))
         {
            Position += Vector2.UnitY * 5;
         }
         if (keyBoardState.IsKeyDown(Keys.Up))
         {
            Position -= Vector2.UnitY * 5;
         }
      }
   }
}
