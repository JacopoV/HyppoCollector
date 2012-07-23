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
    public class LogicAnimal : LogicStar
    {

        public LogicAnimal() : base() { }

        public LogicAnimal(GameDifficulty difficulty,ContentManager Content) : base(difficulty,Content) { }

        public void Update(float dt)
        {


            /*
             * check if player eats something
             * 
             */

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            AudioState aux = (AudioState)content.ServiceProvider.GetService(typeof(AudioState));

            List<Item> waste = gs.wasteList;
            List<Item> item = gs.itemList;
            List<Star> star = gs.starList;
            List<PowerUp> power = gs.powerUpList;

            for(int i=0; i< waste.Count; i++)
            {
                Item t = waste.ElementAt(i);

                if (checkPosition(gs.player.currentPos.X, gs.player.currentPos.Y, t.currentPos.X, t.currentPos.Y, t.radX, t.radY))
                {
                    aux.playEatSound();
                    gs.player.isEating = true;
                    gs.player.wasteEaten += 1;
                    if (power.Count > 0)
                    {
                        if (power.ElementAt(0).objectType == 3)
                        {
                            gs.totalPoints += 10 ;
                        }
                        
                    }

                    //increase bar with 10% every 3 waste eatean
                    if (gs.player.wasteEaten % 4 == 1)
                    {
                        if (!gs.fartingON && gs.fartingBarStep < gs.fartingBarMax )
                        {
                            gs.fartingBarStep += 10;
                        }
                    }

                    gs.totalPoints += 10;

                    removeItem(t);
                }

            }

            for (int i = 0; i < item.Count; i++)
            {
                Item t = item.ElementAt(i);
                bool skip = false;

                if (checkPosition(gs.player.currentPos.X, gs.player.currentPos.Y, t.currentPos.X, t.currentPos.Y, t.radX, t.radY))
                {
                    if (power.Count > 0)
                    {
                        if (power.ElementAt(0).objectType == 1)
                        {
                            skip = true;
                        }
                        
                    }
                    if (!skip)
                    {
                        aux.playOoopsSound();
                        if (gs.player.life > 0)
                            gs.player.life -= 1;
                        gs.player.isEating = true;
                        removeItem(t);
                    }
                    
                }

            }

            for (int i = 0; i < star.Count; i++)
            {
                Star s = star.ElementAt(i);

                if (checkPosition(gs.player.currentPos.X, gs.player.currentPos.Y, s.currentPos.X, s.currentPos.Y, s.radX, s.radY))
                {
                    aux.playEatSound();
                    gs.totalPoints += s.bonus;
                    gs.player.starEaten += 1;
                    gs.player.isEating = true;
                   
                    
                    // extra life for star
                    if (gs.player.life < 3)
                        gs.player.life += 1;

                    removeStar(s);
                }
            }

                base.Update(dt);


        }
    }
}
