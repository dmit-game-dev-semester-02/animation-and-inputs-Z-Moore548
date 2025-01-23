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
    private CelAnimationSequence _littleGuy, _sun;

    private CelAnimationPlayer _animation01, _animation02;

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
        _sun = new CelAnimationSequence(spriteSheet01, 106, 1 / 5f);
        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_sun);
        Texture2D spriteSheet02 = Content.Load<Texture2D>("LittleGuySpriteSheet");
        _littleGuy = new CelAnimationSequence(spriteSheet02, 112, 1 / 5f);
        _animation02 = new CelAnimationPlayer();
        _animation02.Play(_littleGuy);
        
    }

    protected override void Update(GameTime gameTime)
    {
        _animation01.Update(gameTime);
        _animation02.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_cottage, new Vector2(300f, 50f), Color.White);
        _animation01.Draw(_spriteBatch, new Vector2(250f, 0), SpriteEffects.None);
        _animation02.Draw(_spriteBatch, new Vector2(60f, 210f), SpriteEffects.None);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
