using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin
{
    public class Brick
    {
       private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox; 
        public Rectangle Hitbox{
            get{return hitbox;}
        }
        private bool draw;

        public Brick(Texture2D texture, Vector2 position, Vector2 size, bool draw = true)
        {
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(), size.ToPoint());
            this.draw = draw;
        }    
        public void Draw(SpriteBatch spriteBatch){
            if(draw)
                spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
    
}