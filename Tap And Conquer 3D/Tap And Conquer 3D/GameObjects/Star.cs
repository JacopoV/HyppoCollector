using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HyppoCollector.GameObjects
{

    /*
     * this class is for star element
     * 
     */ 

    public class Star
    {
        public Vector2 currentPos;

        public float timeLimit;     /* total time life */
        public float timeLife;      /* span time for life */

        public int height = 60;
        public int width = 60;
        public int radX;
        public int radY;
        public float degree;
        public int bonus;

        public Star() { }

        public Star(Vector2 position,float time, int bonus)
        {
            currentPos = position;
            timeLimit = time;
            timeLife = 0.0f;
            degree = 0;
            radX = width / 2;
            radY = height / 2;
            this.bonus = bonus;

        }
    }
}
