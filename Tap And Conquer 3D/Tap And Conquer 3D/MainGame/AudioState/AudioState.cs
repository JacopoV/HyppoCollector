using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace HyppoCollector
{
    public class AudioState
    {

        public SoundEffect introMusic; //jungle
        public SoundEffect gameMusic;  //congo
        public SoundEffect eat; // splip
        public SoundEffect oops;
        public SoundEffect powerup;
        public SoundEffect fart;

        public bool canPlay;


        public SoundEffectInstance instanceGameMusic;

        public AudioState() { }

        public AudioState(ContentManager Content, bool gameHasControl)
        {
            introMusic = Content.Load<SoundEffect>("Sounds/jungle");
            gameMusic = Content.Load<SoundEffect>("Sounds/congo");
            eat = Content.Load<SoundEffect>("Sounds/eat");
            oops = Content.Load<SoundEffect>("Sounds/ooops");
            powerup = Content.Load<SoundEffect>("Sounds/powerup");
            fart = Content.Load<SoundEffect>("Sounds/fart");
            instanceGameMusic = gameMusic.CreateInstance();

            this.canPlay = gameHasControl; 

            playGameSound();
        }

        public void playIntroSound()
        {
            if(canPlay)
              introMusic.Play();
        }

        public void playGameSound()
        {
            if (canPlay)
            {
                instanceGameMusic.IsLooped = true;
                instanceGameMusic.Play();
            }
            
        }

        public void stopGameSound()
        {
            if (canPlay)
            {
                instanceGameMusic.Stop();
            }
        }


        public void playEatSound()
        {
            if (canPlay)
                eat.Play();
        }

        public void playOoopsSound()
        {
            if(canPlay)
              oops.Play();
        }

        public void playPowerUp()
        {
            if (canPlay)
              powerup.Play();
        }

        public void playFartSound()
        {
            if(canPlay)
              fart.Play();
        }


        
    }
}
