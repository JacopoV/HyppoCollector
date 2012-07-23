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
using HyppoCollector.GameObjects;

namespace HyppoCollector
{
    public class RenderingStar : RenderingItem
    {

        public Texture2D star;

        public RenderingStar(ContentManager Content) : base(Content)
        {

            star = Content.Load<Texture2D>("Textures/star");
            
        }

        public void Draw()
        {
            base.Draw();

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Star> starList = gs.starList;

            for(int i=0; i < starList.Count; i++)
            {
                Star s = starList.ElementAt(0);
                Vector2 screenPos = new Vector2(s.currentPos.X - s.radX, s.currentPos.Y - s.radY);
                Vector2 origin = new Vector2(s.height /2 , s.width / 2);
                spriteBatch.Draw(star, screenPos, null, Color.White, s.degree, origin, 1.0f, SpriteEffects.None, 0f);

            }
        }

    }
}
