using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    public partial class AntFoodForm : Form
    {

        /// <summary>
        /// Declare array of agents collecting food
        /// </summary>
        private CollectingAnt[] collectingAntsArray;
        private uint collectingAntsAmount = 200;

        /// <summary>
        /// Declare list of nest coordinates
        /// </summary>
        private List<Nest> collectingAntsNests;

        /// <summary>
        /// Declare list of food coordinates
        /// </summary>
        private List<Food> foodList;

        /// <summary>
        /// Declare array of agents stealing food
        /// </summary>
        private StealingAnt[] stealingAntsArray;
        private uint stealingAntsAmount = 100;

        /// <summary>
        /// Declare list of collecting ants nests
        /// </summary>
        private List<Nest> stealingAntsNests;

        /// <summary>
        /// the random object given to each Ant agent
        /// </summary>
        private Random randomGenerator;

        /// <summary>
        /// A bitmap image used for double buffering
        /// </summary>
        private Bitmap backgroundImage;
        

        public AntFoodForm()
        {
            InitializeComponent();

            CreateBackgroundImage();

            // Create nest and food lists for collecting ants
            collectingAntsNests = new List<Nest>();
            foodList = new List<Food>();

            // Create nest list for stealing ants
            stealingAntsNests = new List<Nest>();
        }


        /// <summary>
        /// Creates ant objects within an array
        /// </summary>
        private void CreateAnts()
        {
            Rectangle worldLimits;

            // create a radnom object to pass to the ants
            randomGenerator = new Random();

            // define some world size for the ants to move around on
            // assume the size of the world is the same size as the panel
            // on which they are displayed
            worldLimits = new Rectangle(0, 0, drawingPanel.Width, drawingPanel.Height);

            // Create array to hold all collecting ant agents
            collectingAntsArray =  new CollectingAnt[collectingAntsAmount];

            int randomX, randomY;

            // Fill collecting ants array in amount specified by antsCollectingAmount field
            for (int i = 0; i < collectingAntsAmount; i++)
            {
                // Generate random spawn for ant agent
                randomX = randomGenerator.Next(0, drawingPanel.Width);
                randomY = randomGenerator.Next(0, drawingPanel.Height);
                CollectingAnt tempAgent = new CollectingAnt(new SOFT152Vector(randomX, randomY), randomGenerator, worldLimits);

                // Assign ant agent to array and change it's properties
                collectingAntsArray[i] = tempAgent;
                collectingAntsArray[i].AgentSpeed = 0.8;
                collectingAntsArray[i].WanderLimits = 0.25;
            }

            // Create array to hold all stealing ant agents
            stealingAntsArray = new StealingAnt[stealingAntsAmount];

            // Fill stealing ants array in amount specified by antsStealingAmount field
            for (int i = 0; i < stealingAntsAmount; i++)
            {
                randomX = randomGenerator.Next(0, drawingPanel.Width);
                randomY = randomGenerator.Next(0, drawingPanel.Height);
                StealingAnt tempAgent = new StealingAnt(new SOFT152Vector(randomX, randomY), randomGenerator, worldLimits);
                
                stealingAntsArray[i] = tempAgent;
                stealingAntsArray[i].AgentSpeed = 1.0;
                stealingAntsArray[i].WanderLimits = 0.25;
            }
        }


        /// <summary>
        ///  Creates the background image to be used in double buffering 
        /// </summary>
        private void CreateBackgroundImage()
        {
            // the backgroundImage  can be any size
            // assume it is the same size as the panel 
            // on which the Ants are drawn
            backgroundImage = new Bitmap(drawingPanel.Width, drawingPanel.Height);
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            // Compares every ant to position of other ants within the same array
            // and exchanges information about food and nests between them
            CollectingAnt.AntInformationExchange(collectingAntsArray);

            // Update details of each ant in the array
            foreach (CollectingAnt ant in collectingAntsArray)
            {
                // Decide next move for collecting ant
                ant.AntMove(foodList);

                // Update food position and take the food if close enough
                ant.ManageAntFoodPosition(foodList);

                // Update nest position and drop the food if close enough
                ant.ManageAntNestPosition(collectingAntsNests);

                // Method used to make ant forget one of remembered positions from time to time
                ant.ForgetPositions(randomGenerator);
            }
            
            StealingAnt.AntInformationExchange(stealingAntsArray);
            
            foreach (StealingAnt stealingAnt in stealingAntsArray)
            {
                // Decide next move for stealing ant
                stealingAnt.AntMove();

                // Update food position of stealing ants and steal it if
                // close to any of collecting ants that is carrying food
                stealingAnt.ManageAntFoodPosition(collectingAntsArray);

                stealingAnt.ManageAntNestPosition(stealingAntsNests);
                
                stealingAnt.ForgetPositions(randomGenerator);
            }

            // Now draw the agents
            DrawImageDoubleBuffering();
        }


        /// <summary>
        /// Draws the ants and any stationary objects using double buffering
        /// </summary>
        private void DrawImageDoubleBuffering()
        {
            // Two different brushes passed to methods
            Brush mainBrush;
            Brush differentColourBrush;

            // get the graphics context of the background image
            using (Graphics backgroundGraphics = Graphics.FromImage(backgroundImage))
            {
                // Clear panel
                backgroundGraphics.Clear(Color.White);

                // Draw every element of food list
                mainBrush = new SolidBrush(Color.PowderBlue);
                DrawFood(backgroundGraphics, mainBrush);
                

                // Draw every collecting ant using 2 different brushes
                mainBrush = new SolidBrush(Color.Black);
                differentColourBrush = new SolidBrush(Color.Crimson);

                DrawAnts(backgroundGraphics, mainBrush, differentColourBrush, collectingAntsArray);


                // Draw every stealing ant using 2 different brushes again
                mainBrush = new SolidBrush(Color.Orange);
                differentColourBrush = new SolidBrush(Color.ForestGreen);

                DrawAnts(backgroundGraphics, mainBrush, differentColourBrush, stealingAntsArray);


                // Draw every element of stealing ants nests list
                mainBrush = new SolidBrush(Color.Gray);

                DrawNests(backgroundGraphics, mainBrush, stealingAntsNests);


                // Draw every element of collecting ants nests list
                mainBrush = new SolidBrush(Color.SaddleBrown);

                DrawNests(backgroundGraphics, mainBrush, collectingAntsNests);
            }
            

            // Now draw the image on the panel
            using (Graphics g = drawingPanel.CreateGraphics())
            {
                g.DrawImage(backgroundImage, 0, 0, drawingPanel.Width, drawingPanel.Height);
            }

            // Dispose of resources
            mainBrush.Dispose();
            differentColourBrush.Dispose();
        }


        /// <summary>
        /// Draws every ant iterating through ant array
        /// </summary>
        private void DrawAnts(Graphics backgroundGraphics, Brush mainBrush, Brush carryingFoodBrush, AntAgent[] antArray)
        {
            // Variables for better readability 
            float agentXPosition;
            float agentYPosition;

            // Draw every ant on the panel
            foreach (AntAgent ant in antArray)
            {
                agentXPosition = (float)ant.AgentPosition.X;
                agentYPosition = (float)ant.AgentPosition.Y;

                // If ant is carrying food, change her colour
                if (ant.IsCarryingFood == true)
                {
                    backgroundGraphics.FillRectangle(carryingFoodBrush, agentXPosition, agentYPosition, AntAgent.AntSize, AntAgent.AntSize);
                }
                else
                {
                    backgroundGraphics.FillRectangle(mainBrush, agentXPosition, agentYPosition, AntAgent.AntSize, AntAgent.AntSize);
                }
            }
        }


        /// <summary>
        /// Draws every food iterating through food list
        /// </summary>
        private void DrawFood(Graphics backgroundGraphics, Brush mainBrush)
        {
            // For every element in food list
            for (int i = 0; i < foodList.Count; i++)
            {
                // Adjust the size of food object when it reaches certain amount of units left
                if (foodList[i].CurrentAmount <= (uint)(Food.MaxAmount * 0.2))
                {
                    foodList[i].ChangeSize(1);
                }
                else if (foodList[i].CurrentAmount <= (uint)(Food.MaxAmount * 0.4))
                {
                    foodList[i].ChangeSize(2);
                }
                else if (foodList[i].CurrentAmount <= (uint)(Food.MaxAmount * 0.6))
                {
                    foodList[i].ChangeSize(3);
                }
                else if (foodList[i].CurrentAmount <= (uint)(Food.MaxAmount * 0.8))
                {
                    foodList[i].ChangeSize(4);
                }

                // Draw food rectangle
                backgroundGraphics.FillRectangle(mainBrush, (float)foodList[i].FoodPosition.X, (float)foodList[i].FoodPosition.Y, foodList[i].CurrentFoodSize, foodList[i].CurrentFoodSize);
            }
        }


        /// <summary>
        /// Draws every nest iterating through nest list 
        /// </summary>
        private void DrawNests(Graphics backgroundGraphics, Brush solidBrush, List<Nest> nestList)
        {
            string displayedAmount;

            // Create a new font
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 12);

            // Iterate through every element of nest list
            for (int i = 0; i < nestList.Count; i++)
            {
                // Display amount of food stored over the nest
                displayedAmount = nestList[i].FoodStoredAmount.ToString();

                // Adjust position of food stored amount string if it's outside of the panel
                if (nestList[i].NestPosition.Y - 20 < 0 && nestList[i].NestPosition.X < 0)
                {
                    backgroundGraphics.DrawString(displayedAmount, font, solidBrush, (float)nestList[i].NestPosition.X + Nest.DefaultNestSize / 2, (float)nestList[i].NestPosition.Y + Nest.DefaultNestSize);
                }
                else if (nestList[i].NestPosition.Y - 20 < 0)
                {
                    backgroundGraphics.DrawString(displayedAmount, font, solidBrush, (float)nestList[i].NestPosition.X, (float)nestList[i].NestPosition.Y + Nest.DefaultNestSize);
                }
                else if (nestList[i].NestPosition.X < 0)
                {
                    backgroundGraphics.DrawString(displayedAmount, font, solidBrush, (float)nestList[i].NestPosition.X + Nest.DefaultNestSize / 2, (float)nestList[i].NestPosition.Y - 20);
                }
                else
                {
                    backgroundGraphics.DrawString(displayedAmount, font, solidBrush, (float)nestList[i].NestPosition.X, (float)nestList[i].NestPosition.Y - 20);
                }

                // Display nest itself
                backgroundGraphics.FillEllipse(solidBrush, (float)nestList[i].NestPosition.X, (float)nestList[i].NestPosition.Y, Nest.DefaultNestSize, Nest.DefaultNestSize);
            }
        }


        /// <summary>
        /// Mouse click event handler
        /// Adds new nest and food elements when left mouse button is pressed
        /// </summary>
        private void CreateFoodAndNest(object sender, MouseEventArgs e)
        {
            // If left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                // Get the position of the mouse on the panel
                Point mousePosition = drawingPanel.PointToClient(Cursor.Position);
                SOFT152Vector temporaryVector;

                double offset;

                // Check which radio button is checked to determine ant colony to add object to
                if (collectingRadioButton.Checked == true)
                {
                    // Check which radio button is checked and add object to appropriate list
                    if (nestRadioButton.Checked == true)
                    {
                        offset = (Nest.DefaultNestSize / 2);

                        // Create temporary vector including offset so the object will be drawn correctly
                        temporaryVector = new SOFT152Vector(mousePosition.X - offset, mousePosition.Y - offset);
                        collectingAntsNests.Add(new Nest(temporaryVector));
                    }
                    else if (foodRadioButton.Checked == true)
                    {
                        offset = (Food.DefaultFoodSize / 2);

                        temporaryVector = new SOFT152Vector(mousePosition.X - offset, mousePosition.Y - offset);
                        foodList.Add(new Food(temporaryVector));
                    }
                }
                else if (stealingRadioButton.Checked == true)
                {
                    offset = (Nest.DefaultNestSize / 2);

                    temporaryVector = new SOFT152Vector(mousePosition.X - offset, mousePosition.Y - offset);
                    stealingAntsNests.Add(new Nest(temporaryVector));
                }
            }
        }


        /// <summary>
        /// Method disabling parts of interface 
        /// when stealing ants radio button is checked
        /// </summary>
        private void collectingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (stealingRadioButton.Checked == true)
            {
                foodRadioButton.Enabled = false;
                nestRadioButton.Checked = true;
            }
            else
            {
                foodRadioButton.Enabled = true;
            }
        }
        
        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();

            // Disable buttons which changes: 
            // default amount of units of food and collecting and stealing ants amount
            // after start of the program
            defaultFoodUnitsButton.Enabled = false;

            antsAmountButton.Enabled = false;

            // Creates ants after start button is pressed
            // This allows to set amount of ants and default food units in interface
            CreateAnts();
        }

        private void defaultFoodUnitsButton_Click(object sender, EventArgs e)
        {
            uint amount;

            // Try, catch to handle incorrect input
            try
            {
                amount = UInt32.Parse(defaultFoodUnitsTextBox.Text);

                // Change amount of food units to default max amount (500) if set to 0;
                if (amount == 0)
                {
                    amount = Food.MaxAmount;
                }

                // Set default food units amount to the one specified by user
                Food.MaxAmount = amount;

                // Display message informing user that value has been changed
                MessageBox.Show("Default food amount has been correctly changed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Value must be positive!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void antsAmountButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get user input from text boxes and assign it to fields
                collectingAntsAmount = UInt32.Parse(collectingAntsAmountTextBox.Text);

                stealingAntsAmount = UInt32.Parse(stealingAntsAmountTextBox.Text);

                // Display a message that the value has been correctly changed
                MessageBox.Show("Ant amounts has been correctly changed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Values entered are invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Values must be positive!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
