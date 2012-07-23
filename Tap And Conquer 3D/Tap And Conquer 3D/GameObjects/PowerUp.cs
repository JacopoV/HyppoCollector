using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyppoCollector
{
    public class PowerUp
    {

        /*
         * POWERUPS HELPS player and play versus the player. Random PowerUp
         * 
         * Type  Description
         * 
         * 1     create an ice cube over animal items, prevent eating
         * 2     slow down all items on screen
         * 3     points X2 on garbage items
         *      
         * 
         */

        public float timeLimit;
        public float timeLife;
        public int objectType;

        public PowerUp(int type, float timeLimit)
        {
            objectType = type;
            this.timeLimit = timeLimit;
            timeLife = 0.0f;
        }
    }
}
