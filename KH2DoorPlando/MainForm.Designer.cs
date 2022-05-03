
namespace KH2DoorPlando
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportSpoilersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.twoWayDoorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.worldSelect = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.roomSelect = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.doorSelect = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.destRoom = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.destDoor = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(384, 24);
			this.menuStrip1.TabIndex = 11;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportScriptToolStripMenuItem,
            this.exportSpoilersToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// exportScriptToolStripMenuItem
			// 
			this.exportScriptToolStripMenuItem.Name = "exportScriptToolStripMenuItem";
			this.exportScriptToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.exportScriptToolStripMenuItem.Text = "&Export Script...";
			this.exportScriptToolStripMenuItem.Click += new System.EventHandler(this.exportScriptToolStripMenuItem_Click);
			// 
			// exportSpoilersToolStripMenuItem
			// 
			this.exportSpoilersToolStripMenuItem.Name = "exportSpoilersToolStripMenuItem";
			this.exportSpoilersToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.exportSpoilersToolStripMenuItem.Text = "E&xport Spoilers...";
			this.exportSpoilersToolStripMenuItem.Click += new System.EventHandler(this.exportSpoilersToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.twoWayDoorsToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// twoWayDoorsToolStripMenuItem
			// 
			this.twoWayDoorsToolStripMenuItem.Checked = true;
			this.twoWayDoorsToolStripMenuItem.CheckOnClick = true;
			this.twoWayDoorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.twoWayDoorsToolStripMenuItem.Name = "twoWayDoorsToolStripMenuItem";
			this.twoWayDoorsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.twoWayDoorsToolStripMenuItem.Text = "&Two-Way Doors";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "World:";
			// 
			// worldSelect
			// 
			this.worldSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.worldSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.worldSelect.FormattingEnabled = true;
			this.worldSelect.Items.AddRange(new object[] {
            "Twilight Town",
            "Hollow Bastion",
            "Beast\'s Castle",
            "Olympus Coliseum",
            "Agrabah",
            "Land of Dragons",
            "100 Acre Wood",
            "Pride Lands",
            "Atlantica",
            "Disney Castle",
            "Timeless River",
            "Halloween Town",
            "Port Royal",
            "Space Paranoids",
            "The World That Never Was"});
			this.worldSelect.Location = new System.Drawing.Point(56, 27);
			this.worldSelect.Name = "worldSelect";
			this.worldSelect.Size = new System.Drawing.Size(316, 21);
			this.worldSelect.TabIndex = 13;
			this.worldSelect.SelectedIndexChanged += new System.EventHandler(this.worldSelect_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Room:";
			// 
			// roomSelect
			// 
			this.roomSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.roomSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.roomSelect.FormattingEnabled = true;
			this.roomSelect.Location = new System.Drawing.Point(56, 54);
			this.roomSelect.Name = "roomSelect";
			this.roomSelect.Size = new System.Drawing.Size(316, 21);
			this.roomSelect.TabIndex = 15;
			this.roomSelect.SelectedIndexChanged += new System.EventHandler(this.roomSelect_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Door:";
			// 
			// doorSelect
			// 
			this.doorSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.doorSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.doorSelect.Enabled = false;
			this.doorSelect.FormattingEnabled = true;
			this.doorSelect.Location = new System.Drawing.Point(56, 81);
			this.doorSelect.Name = "doorSelect";
			this.doorSelect.Size = new System.Drawing.Size(316, 21);
			this.doorSelect.TabIndex = 17;
			this.doorSelect.SelectedIndexChanged += new System.EventHandler(this.doorSelect_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 111);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "Dest Room:";
			// 
			// destRoom
			// 
			this.destRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.destRoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.destRoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.destRoom.Enabled = false;
			this.destRoom.FormattingEnabled = true;
			this.destRoom.Location = new System.Drawing.Point(81, 108);
			this.destRoom.Name = "destRoom";
			this.destRoom.Size = new System.Drawing.Size(291, 21);
			this.destRoom.TabIndex = 19;
			this.destRoom.SelectedIndexChanged += new System.EventHandler(this.destRoom_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 20;
			this.label5.Text = "Dest Door:";
			// 
			// destDoor
			// 
			this.destDoor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.destDoor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.destDoor.Enabled = false;
			this.destDoor.FormattingEnabled = true;
			this.destDoor.Location = new System.Drawing.Point(81, 135);
			this.destDoor.Name = "destDoor";
			this.destDoor.Size = new System.Drawing.Size(291, 21);
			this.destDoor.TabIndex = 21;
			this.destDoor.SelectedIndexChanged += new System.EventHandler(this.destDoor_SelectedIndexChanged);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 175);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(360, 174);
			this.textBox1.TabIndex = 22;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 159);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 23;
			this.label6.Text = "Warnings:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 361);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.destDoor);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.destRoom);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.doorSelect);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.roomSelect);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.worldSelect);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "KH2 Door Plandomizer";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportSpoilersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem twoWayDoorsToolStripMenuItem;
		private System.Windows.Forms.ComboBox worldSelect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox roomSelect;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox doorSelect;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox destRoom;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox destDoor;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
	}
}