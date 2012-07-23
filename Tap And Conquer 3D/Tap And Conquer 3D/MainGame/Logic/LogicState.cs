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
    using System.Threading;
    #if WINDOWS_PHONE
    using Microsoft.Devices;
    #endif

    public enum WinLoseState
    {
        Win,
        Lose,
        None
    }

    public class LogicState : LogicAnimal
    {

        public double totalGameTime;
        public int score;

        #if WINDOWS_PHONE
        public VibrateController dual_shock = VibrateController.Default;
        #endif

        public LogicState() : base() { }
        public LogicState(GameDifficulty difficulty, ContentManager Content) : base(difficulty,Content)
        {
             this.difficulty = difficulty;
             totalGameTime = 0.0;
             score = 0;
        }

        public void UpdateTime(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalGameTime += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(dt);
     
        }


        public void UpdateInputWindows(RenderingState renderingState)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            AudioState aux = (AudioState)content.ServiceProvider.GetService(typeof(AudioState));
            Animal player = gs.player;

            MouseState touchGesture = Mouse.GetState();

            /*
             * Grab mouse position
             * 
             */
            float clickX = touchGesture.X;
            float clickY = touchGesture.Y;

            if (touchGesture.LeftButton == ButtonState.Pressed)
            {
                float buttonX = 80, buttonY = 80;
                if (clickX <= buttonX && clickY <= buttonY && gs.fartingBarStep == gs.fartingBarMax)
                {
                    aux.playFartSound();
                    gs.fartingON = true;
                    gs.fartingBarStep = 0;

                }





                if (clickX >= player.currentPos.X - player.radX && clickX <= player.currentPos.X + player.radX &&
                                 clickY >= player.currentPos.Y - player.radY && clickY <= player.currentPos.Y + player.radY)
                {

                    if (!player.selected)
                    {
                        player.selected = true;

                    }
                }


                if (player.selected)
                {
                    player.currentPos.X = clickX;
                    player.currentPos.Y = clickY;
                }



            }


            if (touchGesture.LeftButton == ButtonState.Released)
            {
                player.selected = false;
            }
           


        }

        public void UpdateInput(RenderingState renderingState)
        {

            GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));
            AudioState aux = (AudioState)content.ServiceProvider.GetService(typeof(AudioState));
            Animal player = gs.player;

            score = gs.totalPoints;

            while (TouchPanel.IsGestureAvailable)
            {
                var touchGesture = TouchPanel.ReadGesture();
                float tapX = touchGesture.Position.X;
                float tapY = touchGesture.Position.Y;
                switch (touchGesture.GestureType)
                {

                    case GestureType.Tap:

                        float buttonX = 80, buttonY = 80;
                        if (tapX <= buttonX && tapY <= buttonY && gs.fartingBarStep == gs.fartingBarMax)
                        {
                            aux.playFartSound();
                            gs.fartingON = true;
                            gs.fartingBarStep = 0;
                            #if WINDOWS_PHONE
                            dual_shock.Start(new TimeSpan(0,0,02));
                            #endif
                            
                        }
                        
                        break;

                    case GestureType.FreeDrag:

                        if (tapX >= player.currentPos.X - player.radX && tapX <= player.currentPos.X + player.radX &&
                             tapY>= player.currentPos.Y - player.radY && tapY <= player.currentPos.Y + player.radY)
                        {

                            if (!player.selected)
                            {
                                player.selected = true;
                                #if WINDOWS_PHONE
                                dual_shock.Stop();
                                #endif                         
                            }
                        }


                        if (player.selected)
                        {
                            player.currentPos.X = tapX;
                            player.currentPos.Y = tapY;
                        }

                        break;


                    case GestureType.DragComplete:

                        player.selected = false;
                        break;

                }
            }
      
        }
      }
    }


  


