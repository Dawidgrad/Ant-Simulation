using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    class Food
    {
        /// <summary>
        /// Amount of food left
        /// </summary>
        private uint currentAmount;

        /// <summary>
        /// Food size (min 1, max 5)
        /// </summary>
        private int foodSize;

        /// <summary>
        /// Current food size in pixels
        /// </summary>
        private float currentFoodSize;

        /// <summary>
        /// Multiplier that changes current food size every time food shrinks
        /// </summary>
        private float sizeChange = 0.75f;

        /// <summary>
        /// Starting food size
        /// </summary>
        public static float DefaultFoodSize = 30;

        /// <summary>
        /// Detection range of food
        /// </summary>
        public static float FoodDetectionRange = 10;

        /// <summary>
        /// Default amount of food units
        /// </summary>
        public static uint MaxAmount = 500;

        /// <summary>
        /// Coordinates of food position
        /// </summary>
        public SOFT152Vector FoodPosition { get; }

        /// <summary>
        /// Property that allows to access currentAmount field outside of the class
        /// </summary>
        public uint CurrentAmount
        {
            get
            {
                return currentAmount;
            }
        }

        /// <summary>
        /// Property that allows to access currentFoodSizePx field outside of the class
        /// </summary>
        public float CurrentFoodSize
        {
            get
            {
                return currentFoodSize;
            }
        }

        public Food(SOFT152Vector position)
        {
            FoodPosition = new SOFT152Vector(position.X, position.Y);
            currentFoodSize = DefaultFoodSize;
            currentAmount = MaxAmount;
            foodSize = 5;
        }

        /// <summary>
        /// Takes 1 unit of food away
        /// </summary>
        public void TakeOneUnit()
        {
            currentAmount = currentAmount - 1;
        }

        /// <summary>
        /// Changes size and position of food
        /// </summary>
        /// <param name="sizeToChange"> From 1 - 5 size of a nest. 1 being smallest </param>
        public void ChangeSize(int sizeToChange)
        {
            float sizeDifference;

            if (sizeToChange <= 5 && sizeToChange >= 1)
            {
                // If the size to change is smaller than actual size by one
                if (sizeToChange == this.foodSize - 1)
                {
                    sizeDifference = (currentFoodSize - currentFoodSize * sizeChange) / 2;
                    currentFoodSize = currentFoodSize * sizeChange;

                    // Change position of food accordingly
                    this.FoodPosition.X = this.FoodPosition.X + sizeDifference;
                    this.FoodPosition.Y = this.FoodPosition.Y + sizeDifference;

                    this.foodSize = sizeToChange;
                }
            }
        }
    }
}
