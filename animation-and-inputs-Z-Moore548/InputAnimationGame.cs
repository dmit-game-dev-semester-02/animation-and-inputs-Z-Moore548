using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_input;

public class InputAnimationGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const int _WindowWidth = 626;
    private const int _WindowHeight = 380;

    private Texture2D _background, _cottage;
    private CelAnimationSequence _littleGuy, _sun, _guySpin;

    private CelAnimationPlayer _animationSun, _animationGuy, _animationGuySpin;
    private KeyboardState _kbPreviousState;
    private float _x = 60, _y = 210;
    private bool _isMoving;

    public InputAnimationGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("MountainBackground");
        _cottage = Content.Load<Texture2D>("Cottage");
        
        Texture2D spriteSheet01 = Content.Load<Texture2D>("Sun SpriteSheet");
        _sun = new CelAnimationSequence(spriteSheet01, 112, 1 / 5f);
        _animationSun = new CelAnimationPlayer();
        _animationSun.Play(_sun);

        Texture2D spriteSheetMoving = Content.Load<Texture2D>("GuyMovingSprite");
        _littleGuy = new CelAnimationSequence(spriteSheetMoving, 112, 1 / 5f);
        _animationGuy = new CelAnimationPlayer();
        _animationGuy.Play(_littleGuy);

        Texture2D spriteSheet03 = Content.Load<Texture2D>("LittleGuyIdleSpriteSheet");
        _guySpin = new CelAnimationSequence(spriteSheet03, 112, 1 / 4f);
        _animationGuySpin = new CelAnimationPlayer();
        _animationGuySpin.Play(_guySpin);
        
    }

    protected override void Update(GameTime gameTime)
    {
        _animationSun.Update(gameTime);
        _animationGuy.Update(gameTime);
        _animationGuySpin.Update(gameTime);
        KeyboardState kbCurrentState = Keyboard.GetState();
        if (kbCurrentState.IsKeyDown(Keys.Down))
        {
            _y++;
            _isMoving = true;
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _y--;
            _isMoving = true;
        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _x--;
            _isMoving = true;
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _x++;
            _isMoving  = true;
        }
        if (kbCurrentState.IsKeyUp(Keys.Down) && kbCurrentState.IsKeyUp(Keys.Up) && kbCurrentState.IsKeyUp(Keys.Left) && kbCurrentState.IsKeyUp(Keys.Right))
        {
            _isMoving = false;
        }
        
        _kbPreviousState = kbCurrentState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_cottage, new Vector2(300f, 50f), Color.White);
        _animationSun.Draw(_spriteBatch, new Vector2(250f, 0), SpriteEffects.None);
       if (!_isMoving)
       {
            _animationGuySpin.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
       else 
       {
            _animationGuy.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
