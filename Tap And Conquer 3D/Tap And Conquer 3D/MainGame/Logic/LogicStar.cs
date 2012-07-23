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
    public class LogicStar : LogicItem
    {

        public float timer;

        public LogicStar() : base() { }

        public LogicStar(GameDifficulty difficulty,ContentManager Content) : base(difficulty,Content) {

            timer = 0.0f;
        
        }

        public void generateStar(int n_elements)
        {

          /*
           * generate star
           */

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            Animal player = gs.player;


            /* OKKIO PER WINDOWS */

            int width = 800;
            int height = 480;

            // star generation
            for (int i = 0; i < n_elements; i++)
            {

                Random randomX = new Random();
                Random randomY = new Random();
                Random life = new Random();
                Random bonus = new Random();

                int rx = randomX.Next(30, width - 30);
                int ry = randomY.Next(30, height - 30);

                /*
                 * avoid the generation around player
                 */
                if (checkPosition(rx, ry, player.currentPos.X, player.currentPos.Y, player.radX, player.radY))
                {
                    i--;
                }

                /*
                 * avoid item generation around other items
                 */

                bool insert = true;

                for (int k = 0; k < gs.starList.Count && insert; k++)
                {
                    Star t = gs.starList.ElementAt(k);

                    if (checkPosition(rx, ry, t.currentPos.X, t.currentPos.Y, t.radX, t.radY))
                    {
                        insert = false;
                    }
                }

                if (insert)
                {
                    int points = bonus.Next(1000,2000);
                    float time = life.Next(5, 13);
                    gs.starList.Add(new Star(new Vector2(rx, ry), time, points));

                }
            }

        }

        public void moveStar(float dt)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Star> star = gs.starList;

            for (int i = 0; i < star.Count; i++)
            {
                Star s = star.ElementAt(i);
                s.degree += dt*5;
            }

        }

        public void removeStar(Star s)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Star> star = gs.starList;

            for (int i = 0; i < star.Count; i++)
            {
                if (s == star.ElementAt(i))
                {
                    star.RemoveAt(i);

                }

            }

        }

        public void Update(float dt)
        {

            timer += dt;
            
            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

            List<Star> star = gs.starList;

            /*
             * update the lifetime for object
             */

            for (int i = 0; i < star.Count; i++)
            {

                Star s = star.ElementAt(i);
                s.timeLife += dt;
                if (s.timeLife >= s.timeLimit)
                  star.RemoveAt(i);
            }


            /*
             * update the position for object
             */

            moveStar(dt);

            if (timer > 50)
            {
                timer = 0.0f;
                generateStar(1);
            }


            base.Update(dt);

        }

    }

}
