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
    public class LogicItem
    {
        public ContentManager content;
        public GameDifficulty difficulty;
        public Vector2 speedRange;
        public Vector2 backupSpeedRange;
        public int stepGenerationWaste, stepGenerationItems;
        public int powerUpActivationLimit = 50;
        public int genX = 0, genY = 0, spdX = 0, spdY = 0;

        public float timer, timerFarting;

        public LogicItem() { }

        public LogicItem(GameDifficulty difficulty,ContentManager Content)
        {
            content = Content;
            timer = 0.0f;
            timerFarting = 0.0f;
            
            backupSpeedRange = new Vector2(0,0);

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    speedRange = new Vector2(-80,80);
                    stepGenerationItems = 5;
                    stepGenerationWaste = 15;
                    break;
                case GameDifficulty.Normal:
                    speedRange = new Vector2(-110,110);
                    stepGenerationItems = 10;
                    stepGenerationWaste = 25;
                    break;
                case GameDifficulty.Hard:
                    speedRange = new Vector2(-170,170);
                    stepGenerationItems = 17;
                    stepGenerationWaste = 35;
                    break;
            }
        }

        public bool checkPosition(float X, float Y, float objX, float objY, float radX, float radY)
        {
             if (X >= objX - radX - 20 &&
                    X <= objX + radX + 20 &&
                        Y >= objY - radY - 20 &&
                           Y <= objY + radY + 20)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void generateLocation()
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            /* OKKIO PER WINDOWS */



            SpriteBatch sb = (SpriteBatch)content.ServiceProvider.GetService(typeof(SpriteBatch));

#if WINDOWS
            int width = sb.GraphicsDevice.Viewport.Width;
            int height = sb.GraphicsDevice.Viewport.Height;
#endif

#if WINDOWS_PHONE
            int width = 800;
            int height = 480;
#endif


            Random position = new Random();
            Random randomXY = new Random();
            Random randomSpeedXY = new Random();

            int location = position.Next(1, 4);

            // diagonal movement
            if (gs.player.wasteEaten%2!=0)
             genX = genY = spdX = spdY = 0;

            switch (location)
            {
                case 1:
                    // left display position
                    genX = -50;
                    genY = randomXY.Next(30, height - 30);
                    spdX = randomSpeedXY.Next(10, (int)speedRange.Y);
                    break;
                case 2:
                    // right display position
                    genX = width + 50;;
                    genY = randomXY.Next(30, height - 30);
                    spdX = randomSpeedXY.Next((int)speedRange.X, -10);
                    break;
                case 3:
                    // top display position
                    genX = randomXY.Next(30, width - 30);
                    genY = -50;
                    spdY = randomSpeedXY.Next(10, (int)speedRange.Y);
                    break;
                case 4:
                    // bottom display position
                    genX = randomXY.Next(30, width - 30);
                    genY = height + 50;
                    spdY = randomSpeedXY.Next((int)speedRange.X, -10);
                    break;
           }
        }


        public void generateWaste(int n_elements){

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            Animal player = gs.player;

            
            // waste generation
            for (int i = 0; i < n_elements; i++)
            {
                Random life = new Random();

                generateLocation();

                
                /*
                 * avoid item generation around other items
                 */

                bool insert = true;

                for (int k = 0; k < gs.wasteList.Count && insert; k++)
                {
                    Item t = gs.wasteList.ElementAt(k);

                    if (checkPosition(genX, genY, t.currentPos.X, t.currentPos.Y, t.radX, t.radY))
                    {
                        insert = false;
                    }
                }
                
                if(insert)
                {
                    float time = life.Next(10, 25);
                    gs.wasteList.Add(new Item(new Vector2(genX, genY), spdX, spdY, (new Random().Next(1, 6)), time));
                 }
            }
        }


        public void generateItems(int n_elements)
        {
            
            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            Animal player = gs.player;

            // item generation
            for (int i = 0; i < n_elements; i++)
            {

                Random life = new Random();

                generateLocation();

                /*
                 * avoid item generation around other items
                 */

                bool insert = true;

                for (int k = 0; k < gs.itemList.Count && insert; k++)
                {
                    Item t = gs.itemList.ElementAt(k);

                    if (checkPosition(genX, genY, t.currentPos.X, t.currentPos.Y, t.radX, t.radY))
                    {
                        insert = false;
                    }
                }

                if (insert)
                {
                    float time = life.Next(5, 10);
                    gs.itemList.Add(new Item(new Vector2(genX, genY), spdX, spdY, (new Random().Next(7,9)), time));
                }
            }


        }


        public void moveItem(float dt)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Item> waste = gs.wasteList;
            List<Item> item = gs.itemList;

            for (int i = 0; i < waste.Count; i++)
            {
                Item t = waste.ElementAt(i);
                t.currentPos.X += t.speedX * dt;
                t.currentPos.Y += t.speedY * dt;
            }

            for (int i = 0; i < item.Count; i++)
            {
                Item t = item.ElementAt(i);
                t.currentPos.X += t.speedX * dt;
                t.currentPos.Y += t.speedY * dt;

            }
        }

        public void removeItem(Item t)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Item> waste = gs.wasteList;
            List<Item> item = gs.itemList;

            for (int i = 0; i < waste.Count; i++ )
            {
                if (t == waste.ElementAt(i))
                {
                    waste.RemoveAt(i);
                }
            }

            for (int i = 0; i < item.Count; i++)
            {
                if (t == item.ElementAt(i))
                {
                    item.RemoveAt(i);
                }
            }
        }


        public void Update(float dt)
        {

            timer += dt;
            

            float timeGap = (new Random()).Next(2,9); // modalità base da incrementare
            float timeFarting = 0.15f;

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Item> waste = gs.wasteList;
            List<Item> item = gs.itemList;
            List<PowerUp> power = gs.powerUpList;

            // activate a power up every 50 waste
            if (gs.player.wasteEaten > powerUpActivationLimit)
            {
                powerUpActivationLimit += gs.player.wasteEaten;

                int type = new Random().Next(1,4);
                float time = new Random().Next(8,10);
                if (type < 4) // check obbligatorio  --> MODIFICARE IN BASE AL NUMERO DI POWERUP
                {
                    power.Add(new PowerUp(type, time));
                }
                else
                {
                    power.Add(new PowerUp(1, time));
                }

                if (power.ElementAt(0).objectType == 2)
                {
                    backupSpeedRange = new Vector2(speedRange.X,speedRange.Y);
                    speedRange = new Vector2(backupSpeedRange.X / 3, backupSpeedRange.Y / 3);
                }

            }

            /*
             * update the lifetime for objects
             */ 

            for (int i = 0; i < waste.Count; i++)
            {

                Item t = waste.ElementAt(i);
                t.timeLife += dt;
                if (t.timeLife >= t.timeLimit)
                    waste.RemoveAt(i);
            }

            for (int i = 0; i < item.Count; i++)
            {

                Item t = item.ElementAt(i);
                t.timeLife += dt;
                if (t.timeLife >= t.timeLimit)
                    item.RemoveAt(i);
            }


            for (int i = 0; i < power.Count; i++)
            {

                PowerUp p = power.ElementAt(i);
                p.timeLife += dt;
                if (p.timeLife >= p.timeLimit)
                {
                    power.RemoveAt(i);
                    if(backupSpeedRange.X!=0 && backupSpeedRange.Y!=0)
                     speedRange = new Vector2(backupSpeedRange.X,backupSpeedRange.Y);
                }
            }


            /*
             * update the position for object
             */

            moveItem(dt);


            /*
             * populate items
             */

            if (timer > timeGap)
            {
                timer = 0.0f;
                generateWaste(stepGenerationWaste + ((int)Math.Log10(gs.player.wasteEaten)*4));  // modificare in base all'esecuzione del gioco e alla difficoltà

                // if fartingON is true, no generate other items, and remove  all of them

                if (!gs.fartingON)
                    generateItems(stepGenerationItems + ((int)Math.Log10(gs.player.wasteEaten) * 3));
                else
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        item.RemoveAt(i);
                    }

                    timerFarting += dt;

                    // reset condition after fart
                    if (timerFarting > timeFarting)
                    {
                        timerFarting = 0.0f;
                        gs.fartingON = false;
                    }
                 
                }

            }

        }
    }
}
