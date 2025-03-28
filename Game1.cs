using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D Supermario;
    private Texture2D Grass;
    private Platform platform;
    private Texture2D bakgrundsbild;
    private Brick box;
    private Texture2D brick;
    private List<Brick> boxar = new List<Brick>();
    Song theme;
    SoundEffect effect;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        effect = Content.Load<SoundEffect>("jumppp22");
        Supermario = Content.Load<Texture2D>("supermario");
        Grass = Content.Load<Texture2D>("grass");
        player = new Player (Supermario,new Vector2(380, 350),50, effect);
        platform = new Platform (Grass,new Vector2(0, 350),new Vector2(830,130));
        brick = Content.Load<Texture2D>("Brick");
        bakgrundsbild = Content.Load<Texture2D>("bakgrundsbild");
        AddBricks();
        theme = Content.Load<Song>("Героическая минорная (1)");
        MediaPlayer.Play(theme);
    }
    

    protected override void Update(GameTime gameTime)
    {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update();
        playerbrickcollision();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Rectangle bgRect = new(0,0,800,600);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(bakgrundsbild, bgRect, Color.White);
        player.Draw(_spriteBatch);
        platform.Draw(_spriteBatch);
        foreach(Brick b in boxar){
            b.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
        
    }

    private void AddBricks(){
            boxar.Add(new Brick (brick,new Vector2(250, 150),new Vector2(100,80)));        
            boxar.Add(new Brick (brick,new Vector2(500, 200),new Vector2(100,80)));   
            boxar.Add(new Brick (brick,new Vector2(40, 60),new Vector2(100,80)));    
    }

    private void playerbrickcollision(){
        foreach(Brick b in boxar){
            if(b.Hitbox.Intersects(player.Hitbox)){
                player.BrickCollision(b.Hitbox);
            }
        }
    } 
}
