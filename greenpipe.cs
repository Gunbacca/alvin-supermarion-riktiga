using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin
{
    public class greenpipe
    {
       private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox; 
        public Rectangle Hitbox{
            get{return hitbox;}
        }

         public greenpipe(Texture2D texture, Vector2 position, Vector2 size){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(),size.ToPoint());
        }    
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
    
}