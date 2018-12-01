using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    class Nest
    {
        /// <summary>
        /// Nest height and width in pixels
        /// </summary>
        public static float DefaultNestSize = 30;

        /// <summary>
        /// Detection range of nest in pixels
        /// </summary>
        public static float NestDetectionRange = 10;
        
        /// <summary>
        /// Stores how many food units has been delivered to the nest by ants
        /// </summary>
        public int FoodStoredAmount { get; set; }

        /// <summary>
        /// Coordinates of nest position
        /// </summary>
        public SOFT152Vector NestPosition { get; }

        /// <summary>
        /// Offset, half of the size of an ant. Allows ants to correctly stop when next to nest
        /// </summary>
        public static float Offset
        {
            get
            {
                return AntAgent.AntSize / 2;
            }
        }

        public Nest(SOFT152Vector coordinates)
        {
            NestPosition = coordinates;
            FoodStoredAmount = 0;
        }

        /// <summary>
        /// Method used when ant is next to nest
        /// Increases stored amount of food by one
        /// </summary>
        public void StoreFood()
        {
            FoodStoredAmount = FoodStoredAmount + 1;
        }

    }
}
