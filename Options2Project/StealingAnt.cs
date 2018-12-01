using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    class StealingAnt : AntAgent
    {
        public StealingAnt(SOFT152Vector position, Random random) : base(position, random) { }

        public StealingAnt(SOFT152Vector position, Random random, Rectangle bounds) : base(position, random, bounds) { }


        /// <summary>
        /// Method that decides where stealing ant moves
        /// </summary>
        public override void AntMove()
        {
            SOFT152Vector tempVector;

            // If ant knows food position and is not carrying any food, start approaching food
            if (this.FoodPosition != null && this.IsCarryingFood == false)
            {
                // If ant is next to place where the food was, forget coordinates of food
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
            // If ant knows nest position and is carrying food, start approaching nest
            else if (this.NestPosition != null && this.IsCarryingFood == true)
            {
                // Temporary Vector that holds coordinates of middle of the nest moved by offset to top left
                SOFT152Vector nestPosition;
                float nestX;
                float nestY;

                // Middle of circle moved by offset
                nestX = (float)this.NestPosition.X + (Nest.DefaultNestSize / 2) - Nest.Offset;
                nestY = (float)this.NestPosition.Y + (Nest.DefaultNestSize / 2) - Nest.Offset;

                nestPosition = new SOFT152Vector(nestX, nestY);

                // If agent is away from nest, approach middle of it
                if (this.AgentPosition.Distance(nestPosition) > (Nest.DefaultNestSize / 2 - Nest.Offset))
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
        /// Manage stealing ant food position
        /// Steals food from collecting ants and saves position of their encounter
        /// </summary>
        /// <param name="antArray"> Array containing ants to steal the food from </param>
        public void ManageAntFoodPosition(AntAgent[] antArray)
        {
            // Consider stealing ants that are not carrying food at this moment to further optimise the program
            if (this.IsCarryingFood == false)
            {
                for (int i = 0; i < antArray.Length; i++)
                {
                    // Check if the ant carries food and if it's close to stealing ant
                    if (antArray[i].IsCarryingFood == true)
                    {
                        if (IsWithinRectangleDistance(antArray[i].AgentPosition, this.AgentPosition, 3.0f, 3.0f) == true)
                        {
                            // Change owner of food unit
                            this.IsCarryingFood = true;
                            antArray[i].IsCarryingFood = false;

                            // Remember position of this encounter
                            // Ant later returns back to this position and informs other ants about it
                            this.FoodPosition = new SOFT152Vector(antArray[i].AgentPosition);
                        }
                    }
                }
            }
        }

    }
}
