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
  

  public class GameState
  {
    
      public Animal player;
      public List<Item> wasteList;
      public List<Item> itemList;
      public List<Star> starList;
      public List<PowerUp> powerUpList;

      public int totalPoints;
      public int fartingBarStep, fartingBarMax;
      public bool fartingON;

      public GameState() {

          player = new Animal(new Vector2(400,240));
          wasteList = new List<Item>();
          itemList = new List<Item>();
          starList = new List<Star>();
          powerUpList = new List<PowerUp>();
          fartingBarStep = 0;
          fartingBarMax = 100;
          fartingON = false;

     }

  }
}

