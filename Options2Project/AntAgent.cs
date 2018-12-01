using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Drawing;
using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    abstract class AntAgent
    {
        // --------------------------------------------
        // Fields 

        /// <summary>
        /// Current postion of the agent, updated by the three
        /// movment methods
        /// </summary>
        private SOFT152Vector agentPosition;  

        /// <summary>
        /// used in conjunction with the Wander() method
        /// to detemin the next position an agent should be in 
        /// Should remain a private field and do not edit within this class
        /// </summary>
        private SOFT152Vector wanderPosition;


        /// <summary>
        /// The size of the world the agent lives on as a Rectangle object.
        /// Used in conjunction with ShouldStayInWorldBounds, which if true
        /// will mean the agents position will be kept within the world bounds 
        /// (i.e. the  world width or the world height)
        /// </summary>
        private Rectangle worldBounds;   // To keep track of the obejcts bounds i.e. ViewPort dimensions

        /// <summary>
        /// The random object passed to the agent. 
        /// Used only in the Wander() method to generate a 
        /// random direction to move in
        /// </summary>
        private Random randomNumberGenerator;              // random number used for wandering
        

        /// <summary>
        /// Static size of an ant rectangle
        /// </summary>
        public static float AntSize = 3.0f;

        // --------------------------------------------
        // Properties

        /// <summary>
        /// The speed of the agent as used in all three movment methods 
        /// Ideal value depends on timer tick interval and realistic motion of
        /// agents needed. Suggest though in range 0 ... 2
        /// </summary>
        public double AgentSpeed { set; get; }


        /// <summary>
        /// If the agent is using the the ApproachAgent() method, this property defines
        /// at what point the agent will reduce the speed of approach to miminic a 
        /// more relistic approach behaviour
        /// </summary>
        public double ApproachRadius { set; get; }

        public double AvoidDistance { set; get; }

        /// <summary>
        /// Property defines how 'random' the agent movement is whilst 
        /// the agent is using the Wander() method
        /// Suggest range of WanderLimits is 0 ... 1
        /// </summary>
        public double WanderLimits { set; get; }


        /// <summary>
        /// Used in conjunction worldBounds to determine if
        /// the agents position will stay within the world bounds 
        /// </summary>
        public bool ShouldStayInWorldBounds { set; get; }


        /// <summary>
        /// Used when ant knows food was depleted, so it won't come back to the same position
        /// </summary>
        public SOFT152Vector PreviousFoodPosition { set; get; }
        

        /// <summary>
        /// Boolean value that changes when ant is carrying food
        /// </summary>
        public bool IsCarryingFood { get; set; }


        /// <summary>
        /// Coordinates of nest position ant remembers
        /// </summary>
        public SOFT152Vector NestPosition { get; set; }


        /// <summary>
        /// Coordinates of food position ant remembers
        /// </summary>
        public SOFT152Vector FoodPosition { get; set; }


        public SOFT152Vector AgentPosition
        {
            get
            {
                return agentPosition;
            }
            set
            {
                agentPosition = value;
            }
        }


        public AntAgent(SOFT152Vector position, Random random)
        {
           agentPosition = new SOFT152Vector(position.X, position.Y);

            randomNumberGenerator = random;

            InitialiseAgent();
        }

        public AntAgent(SOFT152Vector position, Random random, Rectangle bounds )
        {
            agentPosition = new SOFT152Vector(position.X, position.Y);

            worldBounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            randomNumberGenerator = random;

            InitialiseAgent();
        }

        /// <summary>
        /// Initialises the Agents various fields
        /// with default values
        /// </summary>
        private void InitialiseAgent()
        {
            wanderPosition = new SOFT152Vector();

            ApproachRadius = 10;

            AvoidDistance = 25;

            AgentSpeed = 1.0;

            ShouldStayInWorldBounds = true;

            WanderLimits = 0.5;

            IsCarryingFood = false;
        }

        /// <summary>
        /// Causes the agent to make one step towards the object at objectPosition
        /// The speed of approach will reduce one this agent is within
        /// an ApproachRadius of the objectPosition
        /// </summary>
        public void Approach(SOFT152Vector objectPosition)
        {

            Steering.MoveTo(agentPosition, objectPosition, AgentSpeed, ApproachRadius);

            StayInWorld();
        }

        /// <summary>
        /// Causes the agent to make one step away from  the objectPosition
        /// The speed of avoid is goverened by this agents speed
        /// </summary>
        public void FleeFrom(SOFT152Vector objectPosition)
        {

            Steering.MoveFrom(agentPosition, objectPosition, AgentSpeed, AvoidDistance);

            StayInWorld();
        }

        /// <summary>
        /// Causes the agent to make one random step.
        /// The size of the step determined by the value of WanderLimits
        /// and the agents speed
        /// </summary>
        public void Wander()
        {
            Steering.Wander(agentPosition, wanderPosition, WanderLimits, AgentSpeed, randomNumberGenerator);

            StayInWorld();
        }

        
        protected void StayInWorld()
        {
            // if the agent should stay with in the world
            if (ShouldStayInWorldBounds == true)
            {
                // and the world has a positive width and height
                if (worldBounds.Width >= 0 && worldBounds.Height >= 0)
                {
                    // now adjust the agents position if outside the limits of the world
                    if (agentPosition.X < 0)
                        agentPosition.X = worldBounds.Width;

                    else if (agentPosition.X > worldBounds.Width)
                        agentPosition.X = 0;

                    if (agentPosition.Y < 0)
                        agentPosition.Y = worldBounds.Height;

                    else if (AgentPosition.Y > worldBounds.Height)
                        agentPosition.Y = 0;
                }
            }
        }
        

        /// <summary>
        /// Method that decides where ant moves
        /// </summary>
        public virtual void AntMove() { }


        /// <summary>
        /// Update food position of ant and take the food if close enough
        /// </summary>
        public virtual void ManageAntFoodPosition() { }


        /// <summary>
        /// Update nest position of ant and drop the food if it's close to it
        /// </summary>
        public void ManageAntNestPosition(List<Nest> nestList)
        {
            // Temporary Vector that holds coordinates of middle of the nest moved by offset to top left
            SOFT152Vector offsetNestPosition;
            float nestX;
            float nestY;

            // Check if any of the ants is close to any nest
            for (int i = 0; i < nestList.Count; i++)
            {
                // Middle of the nest changed by offset (it is neccessary because ants are drawn from top left corner)
                nestX = (float)nestList[i].NestPosition.X + (Nest.DefaultNestSize / 2) - Nest.Offset;
                nestY = (float)nestList[i].NestPosition.Y + (Nest.DefaultNestSize / 2) - Nest.Offset;

                offsetNestPosition = new SOFT152Vector(nestX, nestY);

                // If ant is within detection range of nest, update its nest position
                if (this.AgentPosition.Distance(offsetNestPosition) <= (Nest.DefaultNestSize / 2 + Nest.NestDetectionRange + Nest.Offset))
                {
                    this.NestPosition = nestList[i].NestPosition;
                }

                // If ant is next to nest, drop the food it is holding
                if (this.AgentPosition.Distance(offsetNestPosition) <= (Nest.DefaultNestSize / 2 + Nest.Offset))
                {
                    if (this.IsCarryingFood == true)
                    {
                        nestList[i].StoreFood();
                        this.IsCarryingFood = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method that makes ant ocasionally forget food or nest positions
        /// </summary>
        /// <param name="randomGenerator"> Random object passed from form class </param>
        public void ForgetPositions(Random randomGenerator)
        {
            int randomNumber;

            // Get random value from 0 to 1000
            randomNumber = randomGenerator.Next(0, 500);

            // If this value equals 0 or 1 forget coordinates 
            // (around 9% chance every second that ant will forget one or the other)
            if (randomNumber == 0)
            {
                this.FoodPosition = null;
            }
            else if (randomNumber == 1)
            {
                this.NestPosition = null;
            }
        }



        /// <summary>
        /// Compares position of ants in the same array
        /// and exchanges information between them if they are close enough
        /// </summary>
        public static void AntInformationExchange(AntAgent[] antArray)
        {
            // Counters
            int i = 0;
            int j = 0;

            // Usage of array and foreach to optimise algorithm
            foreach (AntAgent ant in antArray)
            {
                // Compare position of every ant to other ants
                foreach (AntAgent anotherAnt in antArray)
                {
                    // Skip comparing ants that have already exchanged information
                    if (j > i)
                    {
                        //If the ants are close to each other
                        if (IsWithinRectangleDistance(anotherAnt.AgentPosition, ant.AgentPosition, 6.0f, 6.0f) == true)
                        {
                            // If one of the ants knows nest position, exchange it
                            if (anotherAnt.NestPosition != null)
                            {
                                ant.NestPosition = anotherAnt.NestPosition;
                            }
                            else if (ant.NestPosition != null)
                            {
                                anotherAnt.NestPosition = ant.NestPosition;
                            }


                            // If one of the ants knows food position, exchange it
                            if (anotherAnt.FoodPosition != null && anotherAnt.FoodPosition != ant.PreviousFoodPosition)
                            {
                                ant.FoodPosition = anotherAnt.FoodPosition;
                            }
                            else if (ant.FoodPosition != null && ant.FoodPosition != anotherAnt.PreviousFoodPosition)
                            {
                                anotherAnt.FoodPosition = ant.FoodPosition;
                            }
                        }
                    }

                    j++;
                }

                i++;
                j = 0;
            }
        }


        /// <summary>
        /// Compares positions of ant and different object (ant / nest / food)
        /// and returns true if they are close enough to each other
        /// (detection range has a rectangle shape)
        /// </summary>
        /// <param name="antPosition"> Coordinates of ant </param>
        /// <param name="objectPosition"> Coordinates of object </param>
        /// <param name="distanceDownRight"> Length (down and right from drawing position) of detection range in pixels </param>
        /// <param name="distanceUpLeft"> Length (up and left from drawing position) of detection range in pixels </param>
        static protected bool IsWithinRectangleDistance(SOFT152Vector antPosition, SOFT152Vector objectPosition, float distanceDownRight, float distanceUpLeft)
        {
            bool isWithinRange;

            // If ant is within some distance from object
            if ((antPosition.X >= objectPosition.X - distanceUpLeft) &&
                (antPosition.X <= objectPosition.X + distanceDownRight) &&
                (antPosition.Y >= objectPosition.Y - distanceUpLeft) &&
                (antPosition.Y <= objectPosition.Y + distanceDownRight))
            {
                isWithinRange = true;
            }
            else
            {
                isWithinRange = false;
            }

            // Return result
            return isWithinRange;
        }


    }  // end class AntAgent
}
