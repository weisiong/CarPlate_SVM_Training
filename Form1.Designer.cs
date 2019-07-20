namespace CarPlate_SVM_Training
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTest = new System.Windows.Forms.Label();
            this.lblOouputLabel = new System.Windows.Forms.Label();
            this.lblAccuracy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 27);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(12, 45);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(80, 27);
            this.btnTrain.TabIndex = 1;
            this.btnTrain.Text = "Train Data";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 78);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(80, 27);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test Data";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(107, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 83);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(35, 120);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(57, 13);
            this.lblTest.TabIndex = 4;
            this.lblTest.Text = "InputLabel";
            // 
            // lblOouputLabel
            // 
            this.lblOouputLabel.AutoSize = true;
            this.lblOouputLabel.Location = new System.Drawing.Point(27, 143);
            this.lblOouputLabel.Name = "lblOouputLabel";
            this.lblOouputLabel.Size = new System.Drawing.Size(65, 13);
            this.lblOouputLabel.TabIndex = 5;
            this.lblOouputLabel.Text = "OutputLabel";
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.AutoSize = true;
            this.lblAccuracy.Location = new System.Drawing.Point(30, 175);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(62, 13);
            this.lblAccuracy.TabIndex = 6;
            this.lblAccuracy.Text = "lblAccuracy";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 200);
            this.Controls.Add(this.lblAccuracy);
            this.Controls.Add(this.lblOouputLabel);
            this.Controls.Add(this.lblTest);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "SVM";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Label lblOouputLabel;
        private System.Windows.Forms.Label lblAccuracy;
    }
}

