
namespace KH2DoorRando
{
	partial class MainForm
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
			System.Windows.Forms.Label label1;
			this.generateButton = new System.Windows.Forms.Button();
			this.twoWayDoors = new System.Windows.Forms.CheckBox();
			this.seedName = new System.Windows.Forms.TextBox();
			this.cornerstoneHill = new System.Windows.Forms.CheckBox();
			this.trackerButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(35, 13);
			label1.TabIndex = 0;
			label1.Text = "Seed:";
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(147, 122);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(75, 23);
			this.generateButton.TabIndex = 3;
			this.generateButton.Text = "Generate";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// twoWayDoors
			// 
			this.twoWayDoors.AutoSize = true;
			this.twoWayDoors.Checked = true;
			this.twoWayDoors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.twoWayDoors.Location = new System.Drawing.Point(13, 46);
			this.twoWayDoors.Name = "twoWayDoors";
			this.twoWayDoors.Size = new System.Drawing.Size(103, 17);
			this.twoWayDoors.TabIndex = 4;
			this.twoWayDoors.Text = "Two-Way Doors";
			this.twoWayDoors.UseVisualStyleBackColor = true;
			// 
			// seedName
			// 
			this.seedName.Location = new System.Drawing.Point(53, 12);
			this.seedName.Name = "seedName";
			this.seedName.Size = new System.Drawing.Size(238, 20);
			this.seedName.TabIndex = 5;
			// 
			// cornerstoneHill
			// 
			this.cornerstoneHill.AutoSize = true;
			this.cornerstoneHill.Location = new System.Drawing.Point(13, 70);
			this.cornerstoneHill.Name = "cornerstoneHill";
			this.cornerstoneHill.Size = new System.Drawing.Size(136, 17);
			this.cornerstoneHill.TabIndex = 6;
			this.cornerstoneHill.Text = "GoA -> Cornerstone Hill";
			this.cornerstoneHill.UseVisualStyleBackColor = true;
			// 
			// trackerButton
			// 
			this.trackerButton.Enabled = false;
			this.trackerButton.Location = new System.Drawing.Point(228, 122);
			this.trackerButton.Name = "trackerButton";
			this.trackerButton.Size = new System.Drawing.Size(75, 23);
			this.trackerButton.TabIndex = 7;
			this.trackerButton.Text = "Tracker";
			this.trackerButton.UseVisualStyleBackColor = true;
			this.trackerButton.Click += new System.EventHandler(this.trackerButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(315, 157);
			this.Controls.Add(this.trackerButton);
			this.Controls.Add(this.cornerstoneHill);
			this.Controls.Add(this.seedName);
			this.Controls.Add(this.twoWayDoors);
			this.Controls.Add(this.generateButton);
			this.Controls.Add(label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "KH2 Door Randomizer";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.CheckBox twoWayDoors;
		private System.Windows.Forms.TextBox seedName;
		private System.Windows.Forms.CheckBox cornerstoneHill;
		private System.Windows.Forms.Button trackerButton;
	}
}