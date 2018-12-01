using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    class CollectingAnt : AntAgent
    {
        public CollectingAnt(SOFT152Vector position, Random random) : base(position, random) { }

        public CollectingAnt(SOFT152Vector position, Random random, Rectangle bounds) : base(position, random, bounds) { }


        /// <summary>
        /// Method that decides where collecting ant moves
        /// </summary>
        /// <param name="foodList"> List of food objects on the panel </param>
        public void AntMove(List<Food> foodList)
        {
            SOFT152Vector tempVector;
            float objectSize = 0;

            // If ant knows food position and is not carrying any food, start approaching food
            if (this.FoodPosition != null && this.IsCarryingFood == false)
            {
                // Get the size of food ant is approaching
                for (int i = 0; i < foodList.Count; i++)
                {
                    // Check if the food ant is approaching is still in the list
                    if (this.FoodPosition == foodList[i].FoodPosition)
                    {
                        objectSize = foodList[i].CurrentFoodSize;
                    }
                }

                // If food doesn't exist anymore
                // (objectSize variable has not been assigned any value)
                if (objectSize == 0)
                {
                    // If ant is where the food was before, forget coordinates of food
                    if (IsWithinRectangleDistance(this.AgentPosition, this.FoodPosition, 2.0f, 2.0f) == true)
                    {
                        this.PreviousFoodPosition = this.FoodPosition;
                        this.FoodPosition = null;
                    }
                    // Else approach food position that ant remembers
                    else
                    {
                        this.ApproachRadius = 0;
                        this.Approach(this.FoodPosition);
                    }
                }
                // If food exists
                else
                {
                    // If agent is away from food, he then approaches middle of the food position depending on the size
                    if (IsWithinRectangleDistance(this.AgentPosition, this.FoodPosition, objectSize, AntSize) == false)
                    {
                        // Change Approach radius so the ant will approach every size of the object in the same way
                        this.ApproachRadius = objectSize + 10;

                        tempVector = new SOFT152Vector(this.FoodPosition.X + (objectSize / 2), this.FoodPosition.Y + (objectSize / 2));
                        this.Approach(tempVector);
                    }
                }

            }
            // If ant knows nest position and is carrying food, start approaching nest
            else if (this.NestPosition != null && this.IsCarryingFood == true)
            {
                // Temporary Vector that holds coordinates of middle of the nest moved by offset to top left
                SOFT152Vector offsetNestPosition;
                float nestX;
                float nestY;

                // Middle of circle moved by offset
                nestX = (float)this.NestPosition.X + (Nest.DefaultNestSize / 2) - Nest.Offset;
                nestY = (float)this.NestPosition.Y + (Nest.DefaultNestSize / 2) - Nest.Offset;

                offsetNestPosition = new SOFT152Vector(nestX, nestY);

                // If agent is away from nest, approach middle of it
                if (this.AgentPosition.Distance(offsetNestPosition) > (Nest.DefaultNestSize / 2 - Nest.Offset))
                {
                    this.ApproachRadius = Nest.DefaultNestSize + 10;

                    tempVector = new SOFT152Vector(this.NestPosition.X + Nest.DefaultNestSize / 2, this.NestPosition.Y + Nest.DefaultNestSize / 2);
                    this.Approach(tempVector);
                }
            }
            // Else wander
            else
            {
                this.Wander();
            }
        }


        /// <summary>
        /// Update food position of collecting ant and take the food if close enough
        /// </summary>
        public void ManageAntFoodPosition(List<Food> foodList)
        {
            // Check if ant is close to any food
            for (int i = 0; i < foodList.Count; i++)
            {
                // If agent is within detection range of food, update his Food Position field
                if (IsWithinRectangleDistance(this.AgentPosition, foodList[i].FoodPosition, foodList[i].CurrentFoodSize + Food.FoodDetectionRange, Food.FoodDetectionRange + AntAgent.AntSize) == true)
                {
                    this.FoodPosition = foodList[i].FoodPosition;
                }

                // If agent is next to food he takes one unit of it
                if (IsWithinRectangleDistance(this.AgentPosition, foodList[i].FoodPosition, foodList[i].CurrentFoodSize, AntAgent.AntSize) == true)
                {
                    if (this.IsCarryingFood == false)
                    {
                        foodList[i].TakeOneUnit();
                        this.IsCarryingFood = true;
                    }
                }

                // If food depletes remove it from food list
                if (foodList[i].CurrentAmount == 0)
                {
                    foodList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
