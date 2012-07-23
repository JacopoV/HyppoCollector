using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HyppoCollector.GameObjects
{
    /*
     * this class if for eatable items and NOT eatable items (with object type)
     */

    public class Item
    {
        public Vector2 currentPos;
        public int objectType;
        
        /* 1 = waste1, 2 = waste2, 3 = waste3,
         * 4 = waste4, 5 = waste5, 6 = waste6,
         * 7 = zebra, 8 = rhino, 9 = croc */

        public float timeLimit;
        public float timeLife;

        public float speedX;
        public float speedY;

        public int height = 60;
        public int width = 60;
        public int radX;
        public int radY;

        public Item() { }

        public Item(Vector2 currentPos, float speedX, float speedY, int objectType, float time)
        {
            this.currentPos = currentPos;
            this.speedX = speedX;
            this.speedY = speedY;
            this.objectType = objectType;
            if (objectType > 1)
            {
                height = 48;
                width = 60;
            }

            timeLife = 0.0f;
            timeLimit = time;
            radX = width / 2;
            radY = height / 2;

        }

    }
}
