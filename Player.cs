
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TEst_med_Alvin
{
    public class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
        private SoundEffect jumpsound;
        public Rectangle Hitbox{
            get{return hitbox;}
        }
        const float GRAVITY = 18.4f;
        Vector2 velocity;
        private bool canJump = true;

        public Player(Texture2D texture, Vector2 position, int pixelsize, SoundEffect jumpsound){
            this.texture = texture;
            this.position = position;
            this.jumpsound = jumpsound;
            hitbox = new Rectangle((int)position.X,(int)position.Y,pixelsize,pixelsize);
        }
        private void jump(){
                velocity.Y = -10;
                canJump = false;

        }
        public void Update(){
            KeyboardState Kstate = Keyboard.GetState();

            if(Kstate.IsKeyDown(Keys.A)){
                position.X -= 3;
            }
            else if(Kstate.IsKeyDown(Keys.D)){
                position.X += 3;
            }
            if(Kstate.IsKeyDown(Keys.Space)){
                if(canJump){
                    jump();
                    jumpsound.Play();
                }
            }
            if(position.Y > 300){
                velocity.Y = 0;
                position.Y = 300;
                canJump = true;
            }
            position.Y += velocity.Y;
            velocity.Y += GRAVITY * 1f/60f; 
            hitbox.Location = position.ToPoint();

        }
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
        public void BrickCollision(Rectangle brickHitbox){
            float lastYPos = position.Y - velocity.Y;
            position.Y = brickHitbox.Y + brickHitbox.Height;
            if(lastYPos + hitbox.Height < brickHitbox.Y){
                velocity.Y = 0;
                position.Y = brickHitbox.Y - hitbox.Height-2; 
                canJump=true;
            }
        }
        public void greenpipeCollision(Rectangle greenpipeHitbox){
            float lastYPos = position.Y - velocity.Y;
            position.Y = greenpipeHitbox.Y + greenpipeHitbox.Height;
            if(lastYPos + hitbox.Height < greenpipeHitbox.Y){
                velocity.Y = 0;
                position.Y = greenpipeHitbox.Y - hitbox.Height-2; 
                canJump=true;
            }
        }

        public void Collision(Rectangle greenpipeHitbox){
            Vector2 prevPos = position;
            prevPos.X -=3;
            hitbox.Location = prevPos.ToPoint();
            if(!hitbox.Intersects(greenpipeHitbox))
            {
                position.X= prevPos.X;
            }
            else{
                position.Y -= velocity.Y;
            }
            hitbox.Location = position.ToPoint();
        }
    }
}