using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace HyppoCollector
{

    public class RenderingAnimal : RenderingStar
    {

        public Texture2D animal, animal_eat, fart;
        public bool farted;

        public RenderingAnimal(ContentManager Content) : base(Content)
        {

            animal = Content.Load<Texture2D>("Textures/animal");
            animal_eat = Content.Load<Texture2D>("Textures/animal2");
            fart = Content.Load<Texture2D>("Textures/fart");
            farted = false;
        }

        public void Draw()
        {

               base.Draw();

               GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
               Rectangle rect = new Rectangle((int)gs.player.currentPos.X - (gs.player.radX), (int)gs.player.currentPos.Y - (gs.player.radY), gs.player.width, gs.player.height);

               if (gs.player.isEating)
               {
                   spriteBatch.Draw(animal_eat, rect, Color.White);
                   gs.player.isEating = false;
               }
               else
               {
                   spriteBatch.Draw(animal, rect, Color.White);
               }

               if (gs.fartingON && !farted)
               {
                   Rectangle fart_rect = new Rectangle((int)gs.player.currentPos.X + 20, (int)gs.player.currentPos.Y + 20, 45, 45);
                   spriteBatch.Draw(fart, fart_rect, Color.White);
                   farted = true;
               }
               else
                   farted = false;
               

        }

    }
}
