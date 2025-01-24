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
    private CelAnimationSequence _sun, _guySpin;

    private CelAnimationPlayer _animationSun, _animationGuySpin;

    private CelAnimationSequenceMultiRow _littleGuyD, _littleGuyU, _littleGuyL, _littleGuyR;
    private CelAnimationPlayerMultiRow _animationGuyD, _animationGuyU, _animationGuyL, _animationGuyR;
    private KeyboardState _kbPreviousState;
    private float _x = 60, _y = 210;
    private bool _isMoving;
    private int _direction;

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

        Texture2D spriteSheetMovingD = Content.Load<Texture2D>("LittleGuySpriteSheet");
        _littleGuyD = new CelAnimationSequenceMultiRow(spriteSheetMovingD, 112, 96, 1 / 6f, 0);
        _animationGuyD = new CelAnimationPlayerMultiRow();
        _animationGuyD.Play(_littleGuyD);

        Texture2D spriteSheetMovingU = Content.Load<Texture2D>("LittleGuySpriteSheet");
        _littleGuyU = new CelAnimationSequenceMultiRow(spriteSheetMovingU, 112, 96, 1 / 6f, 1);
        _animationGuyU = new CelAnimationPlayerMultiRow();
        _animationGuyU.Play(_littleGuyU);

        Texture2D spriteSheetMovingL = Content.Load<Texture2D>("LittleGuySpriteSheet");
        _littleGuyL = new CelAnimationSequenceMultiRow(spriteSheetMovingL, 112, 96, 1 / 6f, 2);
        _animationGuyL = new CelAnimationPlayerMultiRow();
        _animationGuyL.Play(_littleGuyL);

        Texture2D spriteSheetMovingR = Content.Load<Texture2D>("LittleGuySpriteSheet");
        _littleGuyR = new CelAnimationSequenceMultiRow(spriteSheetMovingR, 112, 96, 1 / 6f, 3);
        _animationGuyR = new CelAnimationPlayerMultiRow();
        _animationGuyR.Play(_littleGuyR);

        Texture2D spriteSheet03 = Content.Load<Texture2D>("LittleGuyIdleSpriteSheet");
        _guySpin = new CelAnimationSequence(spriteSheet03, 112, 1 / 4f);
        _animationGuySpin = new CelAnimationPlayer();
        _animationGuySpin.Play(_guySpin);
        
    }

    protected override void Update(GameTime gameTime)
    {
        _animationSun.Update(gameTime);
        _animationGuyD.Update(gameTime);
        _animationGuyU.Update(gameTime);
        _animationGuyL.Update(gameTime);
        _animationGuyR.Update(gameTime);
        _animationGuySpin.Update(gameTime);
        KeyboardState kbCurrentState = Keyboard.GetState();
        if (kbCurrentState.IsKeyDown(Keys.Down))
        {
            _y++;
            _isMoving = true;
            _direction = 0;
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _y--;
            _isMoving = true;
            _direction = 1;

        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _x--;
            _isMoving = true;
            _direction = 2;
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _x++;
            _isMoving  = true;
            _direction = 3;
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
       else if (_direction == 0)
       {
            _animationGuyD.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
       else if (_direction == 1)
       {
            _animationGuyU.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
       else if (_direction == 2)
       {
            _animationGuyL.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
       else if (_direction == 3)
       {
            _animationGuyR.Draw(_spriteBatch, new Vector2(_x, _y), SpriteEffects.None);
       }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
