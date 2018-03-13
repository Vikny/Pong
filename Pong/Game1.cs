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
      enum GameState
      {
         StartMenu,
         Playing,
         Pause
      }
      public Texture2D startButton;
      public Texture2D resumeButton;
      public Texture2D pauseButton;
      public Texture2D exitButton;
      public Vector2 startButtonPosition =
      new Vector2(800 / 2 - 50, 200);
      public Vector2 exitButtonPosition =
         new Vector2(800 / 2 - 50, 250);
      public Vector2 pauseButtonPosition;
      public Vector2 resumeButtonPosition;

      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;
      public Texture2D playerTexture, enemyTexture, ballTexture;
      private Sprite enemy;
      private Player player;
      public Ball ball;
      public int windowWidth = 800;
      public int windowHeight = 600;
      private Vector2 playerPosition = new Vector2(20, 20);
      private Vector2 enemyPosition = new Vector2(800 - 40 , 20);
      private Vector2 ballPosition = new Vector2(40, 40);
      private SpriteFont debugFont;
      private Vector2  Movement { get; set; }
      public  Vector2 Bounce { get; set; }
      private GameState gameState;

      private Vector2 enemyMovement { get; set; }

      private int PlayerScore { get; set; }
      private int EnemyScore { get; set; }
   

      MouseState mouseState, previousMouseState;

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
         IsMouseVisible = true;
         Bounce = new Vector2(4, 4);
         enemyMovement = new Vector2(0, 3.5f);
         previousMouseState = mouseState = Mouse.GetState();
         gameState = GameState.StartMenu;
         EnemyScore = 0;
         PlayerScore = 0;
         

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
         enemy = new Sprite(playerTexture, enemyPosition, spriteBatch);
         ball = new Ball(ballTexture, ballPosition, spriteBatch);
         debugFont = Content.Load<SpriteFont>("DebugFont");
         startButton = Content.Load<Texture2D>("Start");
         resumeButton = Content.Load<Texture2D>("Resume");
         exitButton = Content.Load<Texture2D>("Exit");

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

         mouseState = Mouse.GetState();
         if(previousMouseState.LeftButton == ButtonState.Pressed &&
            mouseState.LeftButton == ButtonState.Released)
         {
            MouseClicked(mouseState.X, mouseState.Y);
         }

         previousMouseState = mouseState;

         if(gameState == GameState.Playing)
         {
            player.Update(gameTime);
            Movement = BounceLogic();
            ball.Position += Movement * gameTime.ElapsedGameTime.Milliseconds * 0.1f;
            enemyLogic();
            enemy.Position += enemyMovement * gameTime.ElapsedGameTime.Milliseconds * 0.1f;
         }

         // TODO: Add your update logic here
         




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

         if(gameState == GameState.StartMenu)
         {
            spriteBatch.Draw(startButton, startButtonPosition, Color.White);
            spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
         }
         player.Draw();
         enemy.Draw();
         ball.Draw();
         WriteDebugInformation();
         ScoreInformation();
         spriteBatch.End();

         base.Draw(gameTime);
         
      }

      private void WriteDebugInformation()
      {
         string positionInText = string.Format("Position of Ball:" +
            "({0:0.0}, {1:0.0})", ball.Position.X, ball.Position.Y);
         string bounceInText = string.Format("Bounce : " +
            "({0:0.0},{1:0.0})", Bounce.X, Bounce.Y);
         string playerInText = string.Format("Position of Player:" +
            "({0:0.0}, {1:0.0}", player.Position.X, player.Position.Y);
         string logicInText = string.Format("{0} {1}",
            (ball.Position.Y > enemy.Position.Y), (ball.Position.Y < enemy.Position.Y));

         spriteBatch.DrawString(debugFont, positionInText,
            new Vector2(20, Window.ClientBounds.Height - 100), Color.Red);
         spriteBatch.DrawString(debugFont, bounceInText,
            new Vector2(20, Window.ClientBounds.Height - 120), Color.Red);
         spriteBatch.DrawString(debugFont, playerInText,
            new Vector2(20, Window.ClientBounds.Height - 140), Color.Red);
      }

      private void ScoreInformation()
      {
         string playerScoreInText = string.Format($"Your Score: {PlayerScore}");
         spriteBatch.DrawString(debugFont, playerScoreInText,
            new Vector2(100, 10), Color.Black);

         string enemyScoreInText = string.Format($"Enemy Score: {EnemyScore}");
         spriteBatch.DrawString(debugFont, enemyScoreInText,
            new Vector2(Window.ClientBounds.Width - 200, 10), Color.Black);
      }


      private Vector2  BounceLogic()
      {
         //or you can use math.cos and math.sin
         //asign them to speed and rectangle.intersects if true

         //if it hit the lower/upper wall or 
         //bottom/top player
         if (ball.Position.Y > Window.ClientBounds.Height||
           ball.Position.Y < 0|| 
           ball.IsTouchingBottom(player)||
           ball.IsTouchingTop(player)||
           ball.IsTouchingBottom(enemy)||
           ball.IsTouchingTop(player))
         {
            Bounce *= new Vector2(1, -1);
         }
         //if it hit the right/left wall or
         //
         if (ball.Position.X > Window.ClientBounds.Width ||
            ball.Position.X < 0 || 
            ball.IsTouchingLeft(player) ||
            ball.IsTouchingRight(player) ||
            ball.IsTouchingLeft(enemy) ||
            ball.IsTouchingRight(enemy))
         {
            Bounce *= new Vector2(-1, 1);
         }

         //if it hit the left and right wall, score will increase
         if(ball.Position.X > Window.ClientBounds.Width)
         {
            ++PlayerScore;
         }
         if(ball.Position.X < 0)
         {
            ++EnemyScore;
         }

         //Console Debug 
         /*Console.WriteLine("{0} {1}", ball.Position.X, ball.Position.Y);
         Console.WriteLine(player.Position);
         Console.WriteLine(Bounce);
         Console.WriteLine("{0} {1}",
            ball.IsTouchingBottom(player), ball.IsTouchingTop(player));
         Console.WriteLine("{0} {1}",
            ball.Bounds.Bottom, ball.Bounds.Top);
         Console.WriteLine("{0} {1}",
            ball.IsTouchingLeft(player), ball.IsTouchingRight(player));
         Console.WriteLine("{0} {1}",
            ball.Bounds.Right, ball.Bounds.Left); */

         return Bounce;
      }

      private void enemyLogic()
      {
         if (ball.Position.Y + ballTexture.Height/2 < 
            enemy.Position.Y + enemyTexture.Height/2)
         {
            enemyMovement = new Vector2(0, -3.5f);
         }
         else
         {
            enemyMovement = new Vector2(0, 3.5f);
         }
      }

      private void MouseClicked(int x, int y)
      {
         Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

         if(gameState == GameState.StartMenu)
         {
            Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
               (int)startButtonPosition.Y, 100, 20);
            Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
               (int)exitButtonPosition.Y, 100, 20);

            if(mouseClickRect.Intersects(startButtonRect))
            {
               gameState = GameState.Playing;
            }
            if (mouseClickRect.Intersects(exitButtonRect))
            {
               Exit();
            }
         }
         if(gameState == GameState.Playing)
         {
            Rectangle pauseButtonRect = new Rectangle(0, 0, 70, 70);

            if(mouseClickRect.Intersects(pauseButtonRect))
            {
               gameState = GameState.Pause;
            }
         }

         if(gameState == GameState.Pause)
         {
            Rectangle resumeButtonRect = new Rectangle((int)resumeButtonPosition.X,
               (int)resumeButtonPosition.Y, 100, 20);

            if(mouseClickRect.Intersects(resumeButtonRect))
            {
               gameState = GameState.Playing;
            }
         }
      }
   }
}
