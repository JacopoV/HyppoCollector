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
    public class RenderingItem : RenderingBackground
    {
        public Texture2D waste1, waste2, waste3, waste4, waste5, waste6;
        public Texture2D waste, zebra_sx, zebra_dx, croc_sx, croc_dx, rhino_sx, rhino_dx, ice;
        public ContentManager m_content;

        public RenderingItem(ContentManager Content) : base(Content)
        {
            waste1 = Content.Load<Texture2D>("Textures/fruit/waste1");
            waste2 = Content.Load<Texture2D>("Textures/fruit/waste2");
            waste3 = Content.Load<Texture2D>("Textures/fruit/waste3");
            waste4 = Content.Load<Texture2D>("Textures/fruit/waste4");
            waste5 = Content.Load<Texture2D>("Textures/fruit/waste5");
            waste6 = Content.Load<Texture2D>("Textures/fruit/waste6");
            zebra_sx = Content.Load<Texture2D>("Textures/zeb_sx");
            zebra_dx = Content.Load<Texture2D>("Textures/zeb_dx");
            croc_sx = Content.Load<Texture2D>("Textures/croc_sx");
            croc_dx = Content.Load<Texture2D>("Textures/croc_dx");
            rhino_sx = Content.Load<Texture2D>("Textures/rhino_sx");
            rhino_dx = Content.Load<Texture2D>("Textures/rhino_dx");
            ice = Content.Load<Texture2D>("Textures/icecube");

        }

        public void Draw()
        {
            base.Draw();

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Item> wasteList = gs.wasteList;

            for (int i = 0; i < wasteList.Count; i++)
            {
                Item t = wasteList.ElementAt(i);
                Rectangle rect = new Rectangle((int)t.currentPos.X - t.radX,(int)t.currentPos.Y - t.radY, t.width, t.height);

                switch (t.objectType)
                {
                    case 1:
                        spriteBatch.Draw(waste1, rect, Color.White);
                        break;
                    case 2: 
                        spriteBatch.Draw(waste2, rect, Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(waste3, rect, Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(waste4, rect, Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(waste5, rect, Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(waste6, rect, Color.White);
                        break;


                }

            }

            List<Item> itemList = gs.itemList;
            List<PowerUp> power = gs.powerUpList;

            for (int i = 0; i < itemList.Count; i++)
            {
                Item t = itemList.ElementAt(i);
                Rectangle rect = new Rectangle((int)t.currentPos.X - t.radX, (int)t.currentPos.Y - t.radY, t.width, t.height);
               
                if (t.speedX > 0)
                {

                    switch(t.objectType)
                    {
                        case 7:
                            spriteBatch.Draw(zebra_dx, rect, Color.White);
                            break;

                        case 8:
                            spriteBatch.Draw(rhino_dx, rect, Color.White);
                            break;

                        case 9:
                            spriteBatch.Draw(croc_dx, rect, Color.White);
                            break;

                    }
}
                else
                {
                    switch(t.objectType)
                    {
                        case 7:
                            spriteBatch.Draw(zebra_sx, rect, Color.White);
                            break;

                        case 8:
                            spriteBatch.Draw(rhino_sx, rect, Color.White);
                            break;

                        case 9:
                            spriteBatch.Draw(croc_sx, rect, Color.White);
                            break;

                     }
                   

                }


                if (power.Count > 0)
                {
                    if (power.ElementAt(0).objectType == 1)
                    {
                        spriteBatch.Draw(ice, rect, Color.White);

                    }

                }


            }
        }

    }
}
