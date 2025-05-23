using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;
using alvin_supermarion_riktiga;
using spaceshhoter;
using System;

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
    private Texture2D bakgrundsbild2;
    private Texture2D himmelpng;
    private Texture2D flagga;
    private Texture2D brick;
    private Texture2D mario;
    private List<enemy> enemies = new List<enemy>();
    private List<Brick> boxar = new List<Brick>();

    private Texture2D greenpipe;
    private List<greenpipe> pipes = new List<greenpipe>();
    private float timer = 0;
    private bool invincible = false;
    private int liv = 3;
    private float _Timeleft;
    private Texture2D heart;
    private bool win = false;
    private Texture2D fiende;

    Song theme;
    SoundEffect effect;
    private Camera camera;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        camera = new Camera(GraphicsDevice.Viewport);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        effect = Content.Load<SoundEffect>("jumppp22");
        Supermario = Content.Load<Texture2D>("supermario");
        mario = Content.Load<Texture2D>("wheelchair2-7379603_1280");
        Grass = Content.Load<Texture2D>("grass");
        player = new Player(Supermario, new Vector2(380, 350), 50, effect);
        platform = new Platform(Grass, new Vector2(-100, 350), new Vector2(1000, 400));
        brick = Content.Load<Texture2D>("Brick");
        greenpipe = Content.Load<Texture2D>("greenpipe");
        bakgrundsbild = Content.Load<Texture2D>("himmel");
        heart = Content.Load<Texture2D>("heart pixel art 254x254");
        bakgrundsbild2 = Content.Load<Texture2D>("himmel2");
        flagga = Content.Load<Texture2D>("png-clipart-supermario-flag-super-mario-bros-2-luigi-pole-angle-flag");
        himmelpng = Content.Load<Texture2D>("himmel3");

        AddBricks();
        Addpipes();
        theme = Content.Load<Song>("videoplayback");
        MediaPlayer.Play(theme);

        enemies.Add(new enemy(mario));
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        winningscreen();
        player.Update();
        playerbrickcollision();
        playergreenpipecollision();
        camera.UpdateCamera(GraphicsDevice.Viewport, player.Hitbox.Location.ToVector2());

        playerdie();
        spawnenemy();

        foreach (enemy enemy in enemies)
        {
            enemy.update();
        }

        if (invincible)
        {
            _Timeleft -= 1f / 60f;

            if (_Timeleft <= 0)
            {
                invincible = false;
            }

        }




        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        if (!win)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            Rectangle bgRect = new(-100, -170, 1000, 600);
            Rectangle bgRect2 = new(900, -170, 1000, 600);
            Rectangle bgRect3 = new(1900, -170, 1000, 600);
            Rectangle bgRect4 = new(2900, -170, 1000, 600);
            Rectangle bgRect5 = new(-100, -750, 1000, 600);
            Rectangle bgRect6 = new(900, -750, 1000, 600);
            Rectangle bgRect7 = new(1900, -750, 1000, 600);
            Rectangle bgRect8 = new(2900, -750, 1000, 600);
            Rectangle bgRect9 = new(-100, -1130, 1000, 600);
            Rectangle bgRect10 = new(900, -1130, 1000, 600);
            Rectangle bgRect11 = new(1900, -1130, 1000, 600);
            Rectangle bgRect12 = new(2900, -1130, 1000, 600);
            Rectangle bgRect13 = new(3600, -76, 300, 700);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Draw(bakgrundsbild, bgRect, Color.White);
            _spriteBatch.Draw(bakgrundsbild, bgRect2, Color.White);
            _spriteBatch.Draw(bakgrundsbild, bgRect3, Color.White);
            _spriteBatch.Draw(bakgrundsbild, bgRect4, Color.White);
            _spriteBatch.Draw(bakgrundsbild2, bgRect5, Color.White);
            _spriteBatch.Draw(bakgrundsbild2, bgRect6, Color.White);
            _spriteBatch.Draw(bakgrundsbild2, bgRect7, Color.White);
            _spriteBatch.Draw(bakgrundsbild2, bgRect8, Color.White);
            _spriteBatch.Draw(himmelpng, bgRect9, Color.White);
            _spriteBatch.Draw(himmelpng, bgRect10, Color.White);
            _spriteBatch.Draw(himmelpng, bgRect11, Color.White);
            _spriteBatch.Draw(himmelpng, bgRect12, Color.White);
            _spriteBatch.Draw(flagga, bgRect13, Color.White);
            player.Draw(_spriteBatch, invincible);
            platform.Draw(_spriteBatch);
            foreach (enemy enemy in enemies)
                enemy.Draw(_spriteBatch);
            foreach (Brick b in boxar)
            {
                b.Draw(_spriteBatch);
                foreach (greenpipe g in pipes)
                {
                    g.Draw(_spriteBatch);
                }
            }



            _spriteBatch.End();

            _spriteBatch.Begin();
            if (liv == 3)
            {
                _spriteBatch.Draw(heart, new Rectangle(64, 0, 30, 30), Color.White);
                _spriteBatch.Draw(heart, new Rectangle(32, 0, 30, 30), Color.White);
                _spriteBatch.Draw(heart, new Rectangle(0, 0, 30, 30), Color.White);
            }
            if (liv == 2)
            {
                _spriteBatch.Draw(heart, new Rectangle(32, 0, 30, 30), Color.White);
                _spriteBatch.Draw(heart, new Rectangle(0, 0, 30, 30), Color.White);
            }
            if (liv == 1)
            {
                _spriteBatch.Draw(heart, new Rectangle(0, 0, 30, 30), Color.White);
            }
            _spriteBatch.End();

        }
        else
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(Font, new Vector2, color.White);
            _spriteBatch.End();
        }
        base.Draw(gameTime);

    }


    private void AddBricks()
    {
        boxar.Add(new Brick(brick, new Vector2(250, 150), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(500, 200), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(40, 60), new Vector2(50, 50))); /* början*/
        boxar.Add(new Brick(brick, new Vector2(900, 107), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(1200, 110), new Vector2(50, 50)));/*andra sectionen*/
        boxar.Add(new Brick(brick, new Vector2(1960, 200), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(1960, 50), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(1960, -100), new Vector2(50, 50)));/*tredje section*/
        boxar.Add(new Brick(brick, new Vector2(2065, 200), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(2065, 50), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(2065, -100), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(2665, -91), new Vector2(50, 50)));/*fjärde section*/
        boxar.Add(new Brick(brick, new Vector2(2930, -160), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(3180, -230), new Vector2(50, 50)));
        boxar.Add(new Brick(brick, new Vector2(3400, -0), new Vector2(50, 50), false));/*fakeblock*/
    }
    private void Addpipes()
    {
        pipes.Add(new greenpipe(greenpipe, new Vector2(800, 276), new Vector2(75, 75)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(1300, 76), new Vector2(75, 275)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(2000, -120), new Vector2(75, 475)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(2450, 76), new Vector2(75, 275)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(2450, -500), new Vector2(75, 475)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(2550, 190), new Vector2(75, 160)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(3400, 76), new Vector2(75, 275)));
        pipes.Add(new greenpipe(greenpipe, new Vector2(3400, -390), new Vector2(75, 375)));

    }
    private void spawnenemy()
    {
        Random rand = new Random();
        int value = rand.Next(1, 1000);
        int spawnChancePercent = 6;
        if (value <= spawnChancePercent)
            enemies.Add(new enemy(mario));
    }
    private void playerbrickcollision()
    {
        foreach (Brick b in boxar)
        {
            if (b.Hitbox.Intersects(player.Hitbox))
            {
                player.Collision(b.Hitbox);
            }
        }

    }

    private void playergreenpipecollision()
    {
        foreach (greenpipe g in pipes)
        {
            if (g.Hitbox.Intersects(player.Hitbox))
            {
                player.Collision(g.Hitbox);
            }
        }

    }

    private void playerdie()
    {
        if (invincible)
            return;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].Hitbox.Intersects(player.Hitbox))
            {

                if (player.Velocity.Y > 0 &&
                    player.Hitbox.Bottom <= enemies[i].Hitbox.Top + 10)
                {

                    // Mario dödar fienden
                    enemies.RemoveAt(i);
                    // Mario studsar upp
                    player.Velocity = new Vector2(player.Velocity.X, -5f);

                }
                else
                {
                    liv--;
                    invincible = true;
                    _Timeleft = 3;
                    if (liv <= 0)
                    {
                        Exit();
                    }
                    break;
                }

            }
        }
    }


    public void winningscreen() {
        if (player.Position.X >= 3400)
        {
            win = true;
        }
    }

}
