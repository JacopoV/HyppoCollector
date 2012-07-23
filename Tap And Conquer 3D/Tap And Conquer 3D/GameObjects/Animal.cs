using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HyppoCollector.GameObjects
{
    public class Animal
    {
        /*
         * this class is for player character
         */

        public Vector2 currentPos;
        public int wasteEaten, starEaten;
        public int life;
        public bool isEating;

        public int height = 100;
        public int width = 105;
        public int radX;
        public int radY;

        public bool selected;               // flag if user is moving the hyppo

        public Animal() { }

        public Animal(Vector2 currentPos)
        {
            this.currentPos = currentPos;
            wasteEaten = 0;
            starEaten = 0;
            life = 3;
            radX = width / 2;
            radY = height / 2;
            isEating = false;
            selected = false;

        }
    }
}
