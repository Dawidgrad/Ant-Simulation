namespace SOFT152Steering
{
    partial class AntFoodForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.objectGroupBox = new System.Windows.Forms.GroupBox();
            this.foodRadioButton = new System.Windows.Forms.RadioButton();
            this.nestRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stealingRadioButton = new System.Windows.Forms.RadioButton();
            this.collectingRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defaultFoodUnitsButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultFoodUnitsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.collectingAntsAmountTextBox = new System.Windows.Forms.TextBox();
            this.antsAmountButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.stealingAntsAmountTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.objectGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.Location = new System.Drawing.Point(42, 93);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(840, 510);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CreateFoodAndNest);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(42, 30);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(42, 59);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // objectGroupBox
            // 
            this.objectGroupBox.Controls.Add(this.foodRadioButton);
            this.objectGroupBox.Controls.Add(this.nestRadioButton);
            this.objectGroupBox.Location = new System.Drawing.Point(381, 24);
            this.objectGroupBox.Name = "objectGroupBox";
            this.objectGroupBox.Size = new System.Drawing.Size(186, 58);
            this.objectGroupBox.TabIndex = 3;
            this.objectGroupBox.TabStop = false;
            // 
            // foodRadioButton
            // 
            this.foodRadioButton.AutoSize = true;
            this.foodRadioButton.Location = new System.Drawing.Point(114, 22);
            this.foodRadioButton.Name = "foodRadioButton";
            this.foodRadioButton.Size = new System.Drawing.Size(49, 17);
            this.foodRadioButton.TabIndex = 1;
            this.foodRadioButton.Text = "Food";
            this.foodRadioButton.UseVisualStyleBackColor = true;
            // 
            // nestRadioButton
            // 
            this.nestRadioButton.AutoSize = true;
            this.nestRadioButton.Checked = true;
            this.nestRadioButton.Location = new System.Drawing.Point(26, 22);
            this.nestRadioButton.Name = "nestRadioButton";
            this.nestRadioButton.Size = new System.Drawing.Size(47, 17);
            this.nestRadioButton.TabIndex = 0;
            this.nestRadioButton.TabStop = true;
            this.nestRadioButton.Text = "Nest";
            this.nestRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stealingRadioButton);
            this.groupBox1.Controls.Add(this.collectingRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(158, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // stealingRadioButton
            // 
            this.stealingRadioButton.AutoSize = true;
            this.stealingRadioButton.Location = new System.Drawing.Point(114, 22);
            this.stealingRadioButton.Name = "stealingRadioButton";
            this.stealingRadioButton.Size = new System.Drawing.Size(63, 17);
            this.stealingRadioButton.TabIndex = 1;
            this.stealingRadioButton.Text = "Stealing";
            this.stealingRadioButton.UseVisualStyleBackColor = true;
            this.stealingRadioButton.CheckedChanged += new System.EventHandler(this.collectingRadioButton_CheckedChanged);
            // 
            // collectingRadioButton
            // 
            this.collectingRadioButton.AutoSize = true;
            this.collectingRadioButton.Checked = true;
            this.collectingRadioButton.Location = new System.Drawing.Point(26, 22);
            this.collectingRadioButton.Name = "collectingRadioButton";
            this.collectingRadioButton.Size = new System.Drawing.Size(71, 17);
            this.collectingRadioButton.TabIndex = 0;
            this.collectingRadioButton.TabStop = true;
            this.collectingRadioButton.Text = "Collecting";
            this.collectingRadioButton.UseVisualStyleBackColor = true;
            this.collectingRadioButton.CheckedChanged += new System.EventHandler(this.collectingRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ant Colony";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Object";
            // 
            // defaultFoodUnitsButton
            // 
            this.defaultFoodUnitsButton.Location = new System.Drawing.Point(900, 250);
            this.defaultFoodUnitsButton.Name = "defaultFoodUnitsButton";
            this.defaultFoodUnitsButton.Size = new System.Drawing.Size(75, 23);
            this.defaultFoodUnitsButton.TabIndex = 12;
            this.defaultFoodUnitsButton.Text = "Change";
            this.defaultFoodUnitsButton.UseVisualStyleBackColor = true;
            this.defaultFoodUnitsButton.Click += new System.EventHandler(this.defaultFoodUnitsButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(897, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Food units per food object";
            // 
            // defaultFoodUnitsTextBox
            // 
            this.defaultFoodUnitsTextBox.Location = new System.Drawing.Point(900, 208);
            this.defaultFoodUnitsTextBox.Name = "defaultFoodUnitsTextBox";
            this.defaultFoodUnitsTextBox.Size = new System.Drawing.Size(91, 20);
            this.defaultFoodUnitsTextBox.TabIndex = 10;
            this.defaultFoodUnitsTextBox.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(897, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Collecting ants amount";
            // 
            // collectingAntsAmountTextBox
            // 
            this.collectingAntsAmountTextBox.Location = new System.Drawing.Point(900, 387);
            this.collectingAntsAmountTextBox.Name = "collectingAntsAmountTextBox";
            this.collectingAntsAmountTextBox.Size = new System.Drawing.Size(91, 20);
            this.collectingAntsAmountTextBox.TabIndex = 13;
            this.collectingAntsAmountTextBox.Text = "200";
            // 
            // antsAmountButton
            // 
            this.antsAmountButton.Location = new System.Drawing.Point(900, 517);
            this.antsAmountButton.Name = "antsAmountButton";
            this.antsAmountButton.Size = new System.Drawing.Size(75, 23);
            this.antsAmountButton.TabIndex = 18;
            this.antsAmountButton.Text = "Change";
            this.antsAmountButton.UseVisualStyleBackColor = true;
            this.antsAmountButton.Click += new System.EventHandler(this.antsAmountButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(897, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Stealing ants amount";
            // 
            // stealingAntsAmountTextBox
            // 
            this.stealingAntsAmountTextBox.Location = new System.Drawing.Point(900, 469);
            this.stealingAntsAmountTextBox.Name = "stealingAntsAmountTextBox";
            this.stealingAntsAmountTextBox.Size = new System.Drawing.Size(91, 20);
            this.stealingAntsAmountTextBox.TabIndex = 16;
            this.stealingAntsAmountTextBox.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(599, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Black: Collecting ant";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(599, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Orange: Stealing ant";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(721, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Red: Collecting ant carrying food";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(720, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Green: Stealing ant carrying food";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(897, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Gray: Stealing ants\' nest";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(897, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Brown: Collecting ants\' nest";
            // 
            // AntFoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 612);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.antsAmountButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.stealingAntsAmountTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.collectingAntsAmountTextBox);
            this.Controls.Add(this.defaultFoodUnitsButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.defaultFoodUnitsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.objectGroupBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.drawingPanel);
            this.Name = "AntFoodForm";
            this.Text = "Steering";
            this.objectGroupBox.ResumeLayout(false);
            this.objectGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox objectGroupBox;
        private System.Windows.Forms.RadioButton foodRadioButton;
        private System.Windows.Forms.RadioButton nestRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton stealingRadioButton;
        private System.Windows.Forms.RadioButton collectingRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button defaultFoodUnitsButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox defaultFoodUnitsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox collectingAntsAmountTextBox;
        private System.Windows.Forms.Button antsAmountButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox stealingAntsAmountTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

