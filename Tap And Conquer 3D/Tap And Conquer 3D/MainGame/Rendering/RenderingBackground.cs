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
    public class RenderingBackground
    {
        public SpriteBatch spriteBatch;
        public Texture2D background;

        public ContentManager content;

        public RenderingBackground(ContentManager Content)
        {
            content = Content;
            background = Content.Load<Texture2D>("Textures/background");
            spriteBatch = Content.ServiceProvider.GetService(typeof(SpriteBatch)) as SpriteBatch;
            
        }

        public void Draw()
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            if (gs.powerUpList.Count > 0)
            {

                switch (gs.powerUpList.ElementAt(0).objectType)
                {

                    case 1:
                        background = content.Load<Texture2D>("Textures/backgroundIceTime");
                        break;

                    case 2:
                        background = content.Load<Texture2D>("Textures/backgroundSlowTime");
                        break;

                    case 3:
                        background = content.Load<Texture2D>("Textures/backgroundPointsX2");
                        break;
                    }

            }
            else
            {
                background = content.Load<Texture2D>("Textures/background");
            }


            spriteBatch.Draw(background, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);

        }

    }
}
