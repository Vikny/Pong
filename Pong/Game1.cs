using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Pong
{
   /// <summary>
   /// This is the main type for your game.
   /// </summary>
   public class Game1 : Game
   {
      Game1 game;
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;
      public Texture2D playerTexture, enemyTexture, ballTexture;
      private Sprite enemySprite;
      private Player player;
      public Ball ball;
      public int windowWidth = 800;
      public int windowHeight = 600;
      private Vector2 playerPosition = new Vector2(20, 20);
      private Vector2 enemyPosition = new Vector2(800 - 40 , 20);
      private Vector2 ballPosition = new Vector2(40, 40);
      private SpriteFont debugFont;
      private Vector2  Movement { get; set; }
      private Vector2 Bounce { get; set; }

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";

         //windows resolution 800 x 600
         graphics.PreferredBackBufferWidth = 800;
         graphics.PreferredBackBufferHeight = 600;
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         // TODO: Add your initialization logic here
         Bounce = new Vector2(2, 2);

         base.Initialize();
      }

      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         spriteBatch = new SpriteBatch(GraphicsDevice);
         playerTexture = Content.Load<Texture2D>("PongPlayer");
         enemyTexture = Content.Load<Texture2D>("PongPlayer");
         ballTexture = Content.Load<Texture2D>("Ball");
         player = new Player(playerTexture, playerPosition, spriteBatch);
         enemySprite = new Sprite(playerTexture, enemyPosition, spriteBatch);
         ball = new Ball(ballTexture, ballPosition, spriteBatch, game);
         debugFont = Content.Load<SpriteFont>("DebugFont");

         // TODO: use this.Content to load your game content here
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

         // TODO: Add your update logic here
         player.Update(gameTime);
         Movement = BounceLogic();
         ball.Position += Movement * gameTime.ElapsedGameTime.Milliseconds * 0.1f;




         base.Update(gameTime);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         // TODO: Add your drawing code here
         spriteBatch.Begin();
         player.Draw();
         enemySprite.Draw();
         ball.Draw();
         WriteDebugInformation();
         spriteBatch.End();

         base.Draw(gameTime);
         
      }

      private void WriteDebugInformation()
      {
         string positionInText = string.Format("Position of Ball:" +
            "({0:0.0}, {1:0.0})", ball.Position.X, ball.Position.Y);
         string bounceInText = string.Format("Bounce : " +
            "({0:0.0},{1:0.0})", Bounce.X, Bounce.Y);

         spriteBatch.DrawString(debugFont, positionInText,
            new Vector2(20, Window.ClientBounds.Height - 100), Color.Red);
         spriteBatch.DrawString(debugFont, bounceInText,
            new Vector2(20, Window.ClientBounds.Height - 130), Color.Red);
      }


      private Vector2  BounceLogic()
      {
         //if it hit the lower/upper wall
         if (ball.Position.Y > 600 - ballTexture.Height/2 ||
           ball.Position.Y < ballTexture.Height/2)
         {
            Bounce *= new Vector2(1, -1);
         }
         //if it hit the right/left wall
         if (ball.Position.X > 800 - 5 / 2 ||
            ball.Position.X < 5 / 2)
         {
            Bounce *= new Vector2(-1, 1);
         }
         Console.WriteLine(ball.Position.Y);
         Console.WriteLine(Bounce);

         return Bounce;
      }
   }
}
