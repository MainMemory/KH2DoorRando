﻿
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label label1;
			this.generateButton = new System.Windows.Forms.Button();
			this.twoWayDoors = new System.Windows.Forms.CheckBox();
			this.seedName = new System.Windows.Forms.TextBox();
			this.trackerButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.goaEnable = new System.Windows.Forms.CheckBox();
			this.atEnable = new System.Windows.Forms.CheckBox();
			this.nwEnable = new System.Windows.Forms.CheckBox();
			this.prEnable = new System.Windows.Forms.CheckBox();
			this.spEnable = new System.Windows.Forms.CheckBox();
			this.htEnable = new System.Windows.Forms.CheckBox();
			this.dcEnable = new System.Windows.Forms.CheckBox();
			this.plEnable = new System.Windows.Forms.CheckBox();
			this.awEnable = new System.Windows.Forms.CheckBox();
			this.ldEnable = new System.Windows.Forms.CheckBox();
			this.agEnable = new System.Windows.Forms.CheckBox();
			this.ocEnable = new System.Windows.Forms.CheckBox();
			this.bcEnable = new System.Windows.Forms.CheckBox();
			this.hbEnable = new System.Windows.Forms.CheckBox();
			this.ttEnable = new System.Windows.Forms.CheckBox();
			this.spoilersButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.modeSelector = new System.Windows.Forms.ComboBox();
			this.modeInfo = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.equalPathLength = new System.Windows.Forms.CheckBox();
			this.hallwayPlacement = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.startPostGame = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
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
			this.generateButton.Location = new System.Drawing.Point(54, 518);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(75, 23);
			this.generateButton.TabIndex = 3;
			this.generateButton.Text = "Randomize";
			this.toolTip1.SetToolTip(this.generateButton, "Generates the randomized door settings and saves the Lua script.");
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// twoWayDoors
			// 
			this.twoWayDoors.AutoSize = true;
			this.twoWayDoors.Checked = true;
			this.twoWayDoors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.twoWayDoors.Location = new System.Drawing.Point(12, 65);
			this.twoWayDoors.Name = "twoWayDoors";
			this.twoWayDoors.Size = new System.Drawing.Size(103, 17);
			this.twoWayDoors.TabIndex = 4;
			this.twoWayDoors.Text = "Two-Way Doors";
			this.toolTip1.SetToolTip(this.twoWayDoors, "If enabled, going through the door you entered a room from will take you back to " +
        "the previous room.");
			this.twoWayDoors.UseVisualStyleBackColor = true;
			// 
			// seedName
			// 
			this.seedName.Location = new System.Drawing.Point(53, 12);
			this.seedName.Name = "seedName";
			this.seedName.Size = new System.Drawing.Size(238, 20);
			this.seedName.TabIndex = 5;
			this.toolTip1.SetToolTip(this.seedName, "Enter any string here to use as the basis for the randomization. Leave blank to c" +
        "hoose a seed at random.");
			// 
			// trackerButton
			// 
			this.trackerButton.Enabled = false;
			this.trackerButton.Location = new System.Drawing.Point(135, 518);
			this.trackerButton.Name = "trackerButton";
			this.trackerButton.Size = new System.Drawing.Size(75, 23);
			this.trackerButton.TabIndex = 7;
			this.trackerButton.Text = "Save Seed";
			this.toolTip1.SetToolTip(this.trackerButton, "Save the seed file, to edit manually, or for use with the tracker.");
			this.trackerButton.UseVisualStyleBackColor = true;
			this.trackerButton.Click += new System.EventHandler(this.trackerButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.goaEnable);
			this.groupBox1.Controls.Add(this.atEnable);
			this.groupBox1.Controls.Add(this.nwEnable);
			this.groupBox1.Controls.Add(this.prEnable);
			this.groupBox1.Controls.Add(this.spEnable);
			this.groupBox1.Controls.Add(this.htEnable);
			this.groupBox1.Controls.Add(this.dcEnable);
			this.groupBox1.Controls.Add(this.plEnable);
			this.groupBox1.Controls.Add(this.awEnable);
			this.groupBox1.Controls.Add(this.ldEnable);
			this.groupBox1.Controls.Add(this.agEnable);
			this.groupBox1.Controls.Add(this.ocEnable);
			this.groupBox1.Controls.Add(this.bcEnable);
			this.groupBox1.Controls.Add(this.hbEnable);
			this.groupBox1.Controls.Add(this.ttEnable);
			this.groupBox1.Location = new System.Drawing.Point(12, 138);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.groupBox1.Size = new System.Drawing.Size(170, 374);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Worlds";
			// 
			// goaEnable
			// 
			this.goaEnable.AutoSize = true;
			this.goaEnable.Checked = true;
			this.goaEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.goaEnable.Location = new System.Drawing.Point(6, 19);
			this.goaEnable.Name = "goaEnable";
			this.goaEnable.Size = new System.Drawing.Size(133, 17);
			this.goaEnable.TabIndex = 14;
			this.goaEnable.Text = "Garden of Assemblage";
			this.goaEnable.UseVisualStyleBackColor = true;
			// 
			// atEnable
			// 
			this.atEnable.AutoSize = true;
			this.atEnable.Checked = true;
			this.atEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.atEnable.Location = new System.Drawing.Point(6, 226);
			this.atEnable.Name = "atEnable";
			this.atEnable.Size = new System.Drawing.Size(67, 17);
			this.atEnable.TabIndex = 13;
			this.atEnable.Text = "Atlantica";
			this.atEnable.UseVisualStyleBackColor = true;
			// 
			// nwEnable
			// 
			this.nwEnable.AutoSize = true;
			this.nwEnable.Checked = true;
			this.nwEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.nwEnable.Location = new System.Drawing.Point(6, 341);
			this.nwEnable.Name = "nwEnable";
			this.nwEnable.Size = new System.Drawing.Size(158, 17);
			this.nwEnable.TabIndex = 12;
			this.nwEnable.Text = "The World That Never Was";
			this.nwEnable.UseVisualStyleBackColor = true;
			// 
			// prEnable
			// 
			this.prEnable.AutoSize = true;
			this.prEnable.Checked = true;
			this.prEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.prEnable.Location = new System.Drawing.Point(6, 295);
			this.prEnable.Name = "prEnable";
			this.prEnable.Size = new System.Drawing.Size(75, 17);
			this.prEnable.TabIndex = 11;
			this.prEnable.Text = "Port Royal";
			this.prEnable.UseVisualStyleBackColor = true;
			// 
			// spEnable
			// 
			this.spEnable.AutoSize = true;
			this.spEnable.Checked = true;
			this.spEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.spEnable.Location = new System.Drawing.Point(6, 318);
			this.spEnable.Name = "spEnable";
			this.spEnable.Size = new System.Drawing.Size(107, 17);
			this.spEnable.TabIndex = 10;
			this.spEnable.Text = "Space Paranoids";
			this.spEnable.UseVisualStyleBackColor = true;
			// 
			// htEnable
			// 
			this.htEnable.AutoSize = true;
			this.htEnable.Checked = true;
			this.htEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.htEnable.Location = new System.Drawing.Point(6, 272);
			this.htEnable.Name = "htEnable";
			this.htEnable.Size = new System.Drawing.Size(106, 17);
			this.htEnable.TabIndex = 9;
			this.htEnable.Text = "Halloween Town";
			this.htEnable.UseVisualStyleBackColor = true;
			// 
			// dcEnable
			// 
			this.dcEnable.AutoSize = true;
			this.dcEnable.Checked = true;
			this.dcEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.dcEnable.Location = new System.Drawing.Point(6, 249);
			this.dcEnable.Name = "dcEnable";
			this.dcEnable.Size = new System.Drawing.Size(90, 17);
			this.dcEnable.TabIndex = 8;
			this.dcEnable.Text = "Disney Castle";
			this.dcEnable.UseVisualStyleBackColor = true;
			// 
			// plEnable
			// 
			this.plEnable.AutoSize = true;
			this.plEnable.Checked = true;
			this.plEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.plEnable.Location = new System.Drawing.Point(6, 203);
			this.plEnable.Name = "plEnable";
			this.plEnable.Size = new System.Drawing.Size(82, 17);
			this.plEnable.TabIndex = 7;
			this.plEnable.Text = "Pride Lands";
			this.plEnable.UseVisualStyleBackColor = true;
			// 
			// awEnable
			// 
			this.awEnable.AutoSize = true;
			this.awEnable.Checked = true;
			this.awEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.awEnable.Location = new System.Drawing.Point(6, 180);
			this.awEnable.Name = "awEnable";
			this.awEnable.Size = new System.Drawing.Size(101, 17);
			this.awEnable.TabIndex = 6;
			this.awEnable.Text = "100 Acre Wood";
			this.awEnable.UseVisualStyleBackColor = true;
			// 
			// ldEnable
			// 
			this.ldEnable.AutoSize = true;
			this.ldEnable.Checked = true;
			this.ldEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ldEnable.Location = new System.Drawing.Point(6, 157);
			this.ldEnable.Name = "ldEnable";
			this.ldEnable.Size = new System.Drawing.Size(105, 17);
			this.ldEnable.TabIndex = 5;
			this.ldEnable.Text = "Land of Dragons";
			this.ldEnable.UseVisualStyleBackColor = true;
			// 
			// agEnable
			// 
			this.agEnable.AutoSize = true;
			this.agEnable.Checked = true;
			this.agEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.agEnable.Location = new System.Drawing.Point(6, 134);
			this.agEnable.Name = "agEnable";
			this.agEnable.Size = new System.Drawing.Size(66, 17);
			this.agEnable.TabIndex = 4;
			this.agEnable.Text = "Agrabah";
			this.agEnable.UseVisualStyleBackColor = true;
			// 
			// ocEnable
			// 
			this.ocEnable.AutoSize = true;
			this.ocEnable.Checked = true;
			this.ocEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ocEnable.Location = new System.Drawing.Point(6, 111);
			this.ocEnable.Name = "ocEnable";
			this.ocEnable.Size = new System.Drawing.Size(111, 17);
			this.ocEnable.TabIndex = 3;
			this.ocEnable.Text = "Olympus Coliseum";
			this.ocEnable.UseVisualStyleBackColor = true;
			// 
			// bcEnable
			// 
			this.bcEnable.AutoSize = true;
			this.bcEnable.Checked = true;
			this.bcEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.bcEnable.Location = new System.Drawing.Point(6, 88);
			this.bcEnable.Name = "bcEnable";
			this.bcEnable.Size = new System.Drawing.Size(92, 17);
			this.bcEnable.TabIndex = 2;
			this.bcEnable.Text = "Beast\'s Castle";
			this.bcEnable.UseVisualStyleBackColor = true;
			// 
			// hbEnable
			// 
			this.hbEnable.AutoSize = true;
			this.hbEnable.Checked = true;
			this.hbEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.hbEnable.Location = new System.Drawing.Point(6, 65);
			this.hbEnable.Name = "hbEnable";
			this.hbEnable.Size = new System.Drawing.Size(96, 17);
			this.hbEnable.TabIndex = 1;
			this.hbEnable.Text = "Hollow Bastion";
			this.hbEnable.UseVisualStyleBackColor = true;
			// 
			// ttEnable
			// 
			this.ttEnable.AutoSize = true;
			this.ttEnable.Checked = true;
			this.ttEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ttEnable.Location = new System.Drawing.Point(6, 42);
			this.ttEnable.Name = "ttEnable";
			this.ttEnable.Size = new System.Drawing.Size(92, 17);
			this.ttEnable.TabIndex = 0;
			this.ttEnable.Text = "Twilight Town";
			this.ttEnable.UseVisualStyleBackColor = true;
			// 
			// spoilersButton
			// 
			this.spoilersButton.Enabled = false;
			this.spoilersButton.Location = new System.Drawing.Point(216, 518);
			this.spoilersButton.Name = "spoilersButton";
			this.spoilersButton.Size = new System.Drawing.Size(75, 23);
			this.spoilersButton.TabIndex = 9;
			this.spoilersButton.Text = "Spoilers";
			this.toolTip1.SetToolTip(this.spoilersButton, "Generate a spoiler spreadsheet listing all the doors and where they lead to.");
			this.spoilersButton.UseVisualStyleBackColor = true;
			this.spoilersButton.Click += new System.EventHandler(this.spoilersButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Mode:";
			// 
			// modeSelector
			// 
			this.modeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.modeSelector.FormattingEnabled = true;
			this.modeSelector.Items.AddRange(new object[] {
            "Single Path",
            "Two Paths",
            "Seven Paths",
            "Branches",
            "Separate Worlds",
            "Wild"});
			this.modeSelector.Location = new System.Drawing.Point(55, 38);
			this.modeSelector.Name = "modeSelector";
			this.modeSelector.Size = new System.Drawing.Size(211, 21);
			this.modeSelector.TabIndex = 11;
			this.toolTip1.SetToolTip(this.modeSelector, "Controls how the doors are connected during randomization. Hover over the [?] for" +
        " information on each mode.");
			this.modeSelector.SelectedIndexChanged += new System.EventHandler(this.modeSelector_SelectedIndexChanged);
			// 
			// modeInfo
			// 
			this.modeInfo.AutoSize = true;
			this.modeInfo.Location = new System.Drawing.Point(272, 41);
			this.modeInfo.Name = "modeInfo";
			this.modeInfo.Size = new System.Drawing.Size(19, 13);
			this.modeInfo.TabIndex = 12;
			this.modeInfo.Text = "[?]";
			// 
			// equalPathLength
			// 
			this.equalPathLength.AutoSize = true;
			this.equalPathLength.Location = new System.Drawing.Point(121, 65);
			this.equalPathLength.Name = "equalPathLength";
			this.equalPathLength.Size = new System.Drawing.Size(114, 17);
			this.equalPathLength.TabIndex = 13;
			this.equalPathLength.Text = "Equal Path Length";
			this.toolTip1.SetToolTip(this.equalPathLength, "The number of rooms in each path will be equal (vs randomly selected).");
			this.equalPathLength.UseVisualStyleBackColor = true;
			// 
			// hallwayPlacement
			// 
			this.hallwayPlacement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.hallwayPlacement.FormattingEnabled = true;
			this.hallwayPlacement.Items.AddRange(new object[] {
            "Anywhere",
            "Before Dead-ends",
            "Between Branches"});
			this.hallwayPlacement.Location = new System.Drawing.Point(118, 88);
			this.hallwayPlacement.Name = "hallwayPlacement";
			this.hallwayPlacement.Size = new System.Drawing.Size(173, 21);
			this.hallwayPlacement.TabIndex = 15;
			this.toolTip1.SetToolTip(this.hallwayPlacement, "Where to place \"hallway\" rooms (2 doors).");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Hallway Placement:";
			// 
			// startPostGame
			// 
			this.startPostGame.AutoSize = true;
			this.startPostGame.Location = new System.Drawing.Point(12, 115);
			this.startPostGame.Name = "startPostGame";
			this.startPostGame.Size = new System.Drawing.Size(116, 17);
			this.startPostGame.TabIndex = 16;
			this.startPostGame.Text = "Start At Post-Game";
			this.toolTip1.SetToolTip(this.startPostGame, "All the worlds will be completed when the game starts. You may freely explore to " +
        "open chests and fight superbosses.");
			this.startPostGame.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(315, 579);
			this.Controls.Add(this.startPostGame);
			this.Controls.Add(this.hallwayPlacement);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.equalPathLength);
			this.Controls.Add(this.modeInfo);
			this.Controls.Add(this.modeSelector);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.spoilersButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.trackerButton);
			this.Controls.Add(this.seedName);
			this.Controls.Add(this.twoWayDoors);
			this.Controls.Add(this.generateButton);
			this.Controls.Add(label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "KH2 Door Randomizer";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.CheckBox twoWayDoors;
		private System.Windows.Forms.TextBox seedName;
		private System.Windows.Forms.Button trackerButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ttEnable;
		private System.Windows.Forms.CheckBox hbEnable;
		private System.Windows.Forms.CheckBox bcEnable;
		private System.Windows.Forms.CheckBox ocEnable;
		private System.Windows.Forms.CheckBox agEnable;
		private System.Windows.Forms.CheckBox ldEnable;
		private System.Windows.Forms.CheckBox awEnable;
		private System.Windows.Forms.CheckBox plEnable;
		private System.Windows.Forms.CheckBox dcEnable;
		private System.Windows.Forms.CheckBox htEnable;
		private System.Windows.Forms.CheckBox spEnable;
		private System.Windows.Forms.CheckBox nwEnable;
		private System.Windows.Forms.CheckBox prEnable;
		private System.Windows.Forms.CheckBox atEnable;
		private System.Windows.Forms.CheckBox goaEnable;
		private System.Windows.Forms.Button spoilersButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox modeSelector;
		private System.Windows.Forms.Label modeInfo;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox equalPathLength;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox hallwayPlacement;
		private System.Windows.Forms.CheckBox startPostGame;
	}
}