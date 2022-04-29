
namespace KH2DoorTracker
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
			System.Windows.Forms.GroupBox groupBox1;
			System.Windows.Forms.GroupBox groupBox2;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
			this.currentRoom = new System.Windows.Forms.Label();
			this.door7 = new System.Windows.Forms.Label();
			this.door6 = new System.Windows.Forms.Label();
			this.door5 = new System.Windows.Forms.Label();
			this.door4 = new System.Windows.Forms.Label();
			this.door3 = new System.Windows.Forms.Label();
			this.door2 = new System.Windows.Forms.Label();
			this.door1 = new System.Windows.Forms.Label();
			this.nwEvent = new System.Windows.Forms.Label();
			this.spEvent = new System.Windows.Forms.Label();
			this.prEvent = new System.Windows.Forms.Label();
			this.htEvent = new System.Windows.Forms.Label();
			this.dcEvent = new System.Windows.Forms.Label();
			this.plEvent = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.awEvent = new System.Windows.Forms.Label();
			this.ldEvent = new System.Windows.Forms.Label();
			this.agEvent = new System.Windows.Forms.Label();
			this.ocEvent = new System.Windows.Forms.Label();
			this.bcEvent = new System.Windows.Forms.Label();
			this.hbEvent = new System.Windows.Forms.Label();
			this.ttEvent = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.roomDist = new System.Windows.Forms.Label();
			this.roomPath = new System.Windows.Forms.Label();
			this.findRoom = new System.Windows.Forms.ComboBox();
			this.showSpoilers = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.AutoSize = true;
			groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			groupBox1.Controls.Add(this.currentRoom);
			groupBox1.Location = new System.Drawing.Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			groupBox1.Size = new System.Drawing.Size(254, 42);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Current Room";
			// 
			// currentRoom
			// 
			this.currentRoom.AutoEllipsis = true;
			this.currentRoom.Location = new System.Drawing.Point(3, 16);
			this.currentRoom.Name = "currentRoom";
			this.currentRoom.Size = new System.Drawing.Size(245, 13);
			this.currentRoom.TabIndex = 0;
			this.currentRoom.Text = "Game not running.";
			this.currentRoom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// groupBox2
			// 
			groupBox2.AutoSize = true;
			groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			groupBox2.Controls.Add(tableLayoutPanel1);
			groupBox2.Location = new System.Drawing.Point(12, 60);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			groupBox2.Size = new System.Drawing.Size(480, 131);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Doors";
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(this.door7, 0, 6);
			tableLayoutPanel1.Controls.Add(this.door6, 0, 5);
			tableLayoutPanel1.Controls.Add(this.door5, 0, 4);
			tableLayoutPanel1.Controls.Add(this.door4, 0, 3);
			tableLayoutPanel1.Controls.Add(this.door3, 0, 2);
			tableLayoutPanel1.Controls.Add(this.door2, 0, 1);
			tableLayoutPanel1.Controls.Add(this.door1, 0, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 7;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.Size = new System.Drawing.Size(468, 99);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// door7
			// 
			this.door7.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door7.AutoEllipsis = true;
			this.door7.Location = new System.Drawing.Point(4, 85);
			this.door7.Name = "door7";
			this.door7.Size = new System.Drawing.Size(460, 13);
			this.door7.TabIndex = 2;
			this.door7.Text = "-";
			this.door7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door6
			// 
			this.door6.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door6.AutoEllipsis = true;
			this.door6.Location = new System.Drawing.Point(4, 71);
			this.door6.Name = "door6";
			this.door6.Size = new System.Drawing.Size(460, 13);
			this.door6.TabIndex = 2;
			this.door6.Text = "-";
			this.door6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door5
			// 
			this.door5.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door5.AutoEllipsis = true;
			this.door5.Location = new System.Drawing.Point(4, 57);
			this.door5.Name = "door5";
			this.door5.Size = new System.Drawing.Size(460, 13);
			this.door5.TabIndex = 2;
			this.door5.Text = "-";
			this.door5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door4
			// 
			this.door4.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door4.AutoEllipsis = true;
			this.door4.Location = new System.Drawing.Point(4, 43);
			this.door4.Name = "door4";
			this.door4.Size = new System.Drawing.Size(460, 13);
			this.door4.TabIndex = 2;
			this.door4.Text = "-";
			this.door4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door3
			// 
			this.door3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door3.AutoEllipsis = true;
			this.door3.Location = new System.Drawing.Point(4, 29);
			this.door3.Name = "door3";
			this.door3.Size = new System.Drawing.Size(460, 13);
			this.door3.TabIndex = 2;
			this.door3.Text = "-";
			this.door3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door2
			// 
			this.door2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door2.AutoEllipsis = true;
			this.door2.Location = new System.Drawing.Point(4, 15);
			this.door2.Name = "door2";
			this.door2.Size = new System.Drawing.Size(460, 13);
			this.door2.TabIndex = 2;
			this.door2.Text = "-";
			this.door2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// door1
			// 
			this.door1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.door1.AutoEllipsis = true;
			this.door1.Location = new System.Drawing.Point(4, 1);
			this.door1.Name = "door1";
			this.door1.Size = new System.Drawing.Size(460, 13);
			this.door1.TabIndex = 1;
			this.door1.Text = "-";
			this.door1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.Controls.Add(this.nwEvent, 1, 12);
			tableLayoutPanel2.Controls.Add(this.spEvent, 1, 11);
			tableLayoutPanel2.Controls.Add(this.prEvent, 1, 10);
			tableLayoutPanel2.Controls.Add(this.htEvent, 1, 9);
			tableLayoutPanel2.Controls.Add(this.dcEvent, 1, 8);
			tableLayoutPanel2.Controls.Add(this.plEvent, 1, 7);
			tableLayoutPanel2.Controls.Add(this.label14, 0, 6);
			tableLayoutPanel2.Controls.Add(this.label13, 0, 5);
			tableLayoutPanel2.Controls.Add(this.label12, 0, 4);
			tableLayoutPanel2.Controls.Add(this.label11, 0, 3);
			tableLayoutPanel2.Controls.Add(this.label10, 0, 2);
			tableLayoutPanel2.Controls.Add(this.label9, 0, 1);
			tableLayoutPanel2.Controls.Add(this.awEvent, 1, 6);
			tableLayoutPanel2.Controls.Add(this.ldEvent, 1, 5);
			tableLayoutPanel2.Controls.Add(this.agEvent, 1, 4);
			tableLayoutPanel2.Controls.Add(this.ocEvent, 1, 3);
			tableLayoutPanel2.Controls.Add(this.bcEvent, 1, 2);
			tableLayoutPanel2.Controls.Add(this.hbEvent, 1, 1);
			tableLayoutPanel2.Controls.Add(this.ttEvent, 1, 0);
			tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
			tableLayoutPanel2.Controls.Add(this.label15, 0, 7);
			tableLayoutPanel2.Controls.Add(this.label16, 0, 8);
			tableLayoutPanel2.Controls.Add(this.label17, 0, 9);
			tableLayoutPanel2.Controls.Add(this.label18, 0, 10);
			tableLayoutPanel2.Controls.Add(this.label19, 0, 11);
			tableLayoutPanel2.Controls.Add(this.label20, 0, 12);
			tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 13;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.Size = new System.Drawing.Size(378, 183);
			tableLayoutPanel2.TabIndex = 1;
			// 
			// nwEvent
			// 
			this.nwEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.nwEvent.AutoEllipsis = true;
			this.nwEvent.Location = new System.Drawing.Point(150, 169);
			this.nwEvent.Name = "nwEvent";
			this.nwEvent.Size = new System.Drawing.Size(224, 13);
			this.nwEvent.TabIndex = 21;
			this.nwEvent.Text = "-";
			// 
			// spEvent
			// 
			this.spEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.spEvent.AutoEllipsis = true;
			this.spEvent.Location = new System.Drawing.Point(150, 155);
			this.spEvent.Name = "spEvent";
			this.spEvent.Size = new System.Drawing.Size(224, 13);
			this.spEvent.TabIndex = 20;
			this.spEvent.Text = "-";
			// 
			// prEvent
			// 
			this.prEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.prEvent.AutoEllipsis = true;
			this.prEvent.Location = new System.Drawing.Point(150, 141);
			this.prEvent.Name = "prEvent";
			this.prEvent.Size = new System.Drawing.Size(224, 13);
			this.prEvent.TabIndex = 19;
			this.prEvent.Text = "-";
			// 
			// htEvent
			// 
			this.htEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.htEvent.AutoEllipsis = true;
			this.htEvent.Location = new System.Drawing.Point(150, 127);
			this.htEvent.Name = "htEvent";
			this.htEvent.Size = new System.Drawing.Size(224, 13);
			this.htEvent.TabIndex = 18;
			this.htEvent.Text = "-";
			// 
			// dcEvent
			// 
			this.dcEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.dcEvent.AutoEllipsis = true;
			this.dcEvent.Location = new System.Drawing.Point(150, 113);
			this.dcEvent.Name = "dcEvent";
			this.dcEvent.Size = new System.Drawing.Size(224, 13);
			this.dcEvent.TabIndex = 17;
			this.dcEvent.Text = "-";
			// 
			// plEvent
			// 
			this.plEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.plEvent.AutoEllipsis = true;
			this.plEvent.Location = new System.Drawing.Point(150, 99);
			this.plEvent.Name = "plEvent";
			this.plEvent.Size = new System.Drawing.Size(224, 13);
			this.plEvent.TabIndex = 16;
			this.plEvent.Text = "-";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(4, 85);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(82, 13);
			this.label14.TabIndex = 9;
			this.label14.Text = "100 Acre Wood";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(4, 71);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(86, 13);
			this.label13.TabIndex = 8;
			this.label13.Text = "Land of Dragons";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(4, 57);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(47, 13);
			this.label12.TabIndex = 7;
			this.label12.Text = "Agrabah";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(4, 43);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(92, 13);
			this.label11.TabIndex = 6;
			this.label11.Text = "Olympus Coliseum";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 29);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "Beast\'s Castle";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(4, 15);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(77, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Hollow Bastion";
			// 
			// awEvent
			// 
			this.awEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.awEvent.AutoEllipsis = true;
			this.awEvent.Location = new System.Drawing.Point(150, 85);
			this.awEvent.Name = "awEvent";
			this.awEvent.Size = new System.Drawing.Size(224, 13);
			this.awEvent.TabIndex = 2;
			this.awEvent.Text = "-";
			// 
			// ldEvent
			// 
			this.ldEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.ldEvent.AutoEllipsis = true;
			this.ldEvent.Location = new System.Drawing.Point(150, 71);
			this.ldEvent.Name = "ldEvent";
			this.ldEvent.Size = new System.Drawing.Size(224, 13);
			this.ldEvent.TabIndex = 2;
			this.ldEvent.Text = "-";
			// 
			// agEvent
			// 
			this.agEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.agEvent.AutoEllipsis = true;
			this.agEvent.Location = new System.Drawing.Point(150, 57);
			this.agEvent.Name = "agEvent";
			this.agEvent.Size = new System.Drawing.Size(224, 13);
			this.agEvent.TabIndex = 2;
			this.agEvent.Text = "-";
			// 
			// ocEvent
			// 
			this.ocEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.ocEvent.AutoEllipsis = true;
			this.ocEvent.Location = new System.Drawing.Point(150, 43);
			this.ocEvent.Name = "ocEvent";
			this.ocEvent.Size = new System.Drawing.Size(224, 13);
			this.ocEvent.TabIndex = 2;
			this.ocEvent.Text = "-";
			// 
			// bcEvent
			// 
			this.bcEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.bcEvent.AutoEllipsis = true;
			this.bcEvent.Location = new System.Drawing.Point(150, 29);
			this.bcEvent.Name = "bcEvent";
			this.bcEvent.Size = new System.Drawing.Size(224, 13);
			this.bcEvent.TabIndex = 2;
			this.bcEvent.Text = "-";
			// 
			// hbEvent
			// 
			this.hbEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.hbEvent.AutoEllipsis = true;
			this.hbEvent.Location = new System.Drawing.Point(150, 15);
			this.hbEvent.Name = "hbEvent";
			this.hbEvent.Size = new System.Drawing.Size(224, 13);
			this.hbEvent.TabIndex = 2;
			this.hbEvent.Text = "-";
			// 
			// ttEvent
			// 
			this.ttEvent.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.ttEvent.AutoEllipsis = true;
			this.ttEvent.Location = new System.Drawing.Point(150, 1);
			this.ttEvent.Name = "ttEvent";
			this.ttEvent.Size = new System.Drawing.Size(224, 13);
			this.ttEvent.TabIndex = 1;
			this.ttEvent.Text = "-";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 1);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(73, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Twilight Town";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(4, 99);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(63, 13);
			this.label15.TabIndex = 10;
			this.label15.Text = "Pride Lands";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(4, 113);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(71, 13);
			this.label16.TabIndex = 11;
			this.label16.Text = "Disney Castle";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(4, 127);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(87, 13);
			this.label17.TabIndex = 12;
			this.label17.Text = "Halloween Town";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(4, 141);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(56, 13);
			this.label18.TabIndex = 13;
			this.label18.Text = "Port Royal";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(4, 155);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(88, 13);
			this.label19.TabIndex = 14;
			this.label19.Text = "Space Paranoids";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(4, 169);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(139, 13);
			this.label20.TabIndex = 15;
			this.label20.Text = "The World That Never Was";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(tableLayoutPanel2);
			this.groupBox3.Location = new System.Drawing.Point(12, 197);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(393, 213);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Active Events";
			// 
			// groupBox4
			// 
			this.groupBox4.AutoSize = true;
			this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox4.Controls.Add(this.roomDist);
			this.groupBox4.Controls.Add(this.roomPath);
			this.groupBox4.Controls.Add(this.findRoom);
			this.groupBox4.Location = new System.Drawing.Point(12, 416);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.groupBox4.Size = new System.Drawing.Size(480, 173);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Path Finder";
			// 
			// roomDist
			// 
			this.roomDist.AutoSize = true;
			this.roomDist.Location = new System.Drawing.Point(317, 22);
			this.roomDist.Name = "roomDist";
			this.roomDist.Size = new System.Drawing.Size(10, 13);
			this.roomDist.TabIndex = 2;
			this.roomDist.Text = "-";
			// 
			// roomPath
			// 
			this.roomPath.AutoEllipsis = true;
			this.roomPath.Location = new System.Drawing.Point(6, 43);
			this.roomPath.Name = "roomPath";
			this.roomPath.Size = new System.Drawing.Size(468, 117);
			this.roomPath.TabIndex = 1;
			this.roomPath.Text = "-";
			// 
			// findRoom
			// 
			this.findRoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.findRoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.findRoom.FormattingEnabled = true;
			this.findRoom.Location = new System.Drawing.Point(6, 19);
			this.findRoom.Name = "findRoom";
			this.findRoom.Size = new System.Drawing.Size(305, 21);
			this.findRoom.TabIndex = 0;
			this.findRoom.SelectedIndexChanged += new System.EventHandler(this.findRoom_SelectedIndexChanged);
			// 
			// showSpoilers
			// 
			this.showSpoilers.AutoSize = true;
			this.showSpoilers.Location = new System.Drawing.Point(272, 27);
			this.showSpoilers.Name = "showSpoilers";
			this.showSpoilers.Size = new System.Drawing.Size(93, 17);
			this.showSpoilers.TabIndex = 4;
			this.showSpoilers.Text = "Show Spoilers";
			this.showSpoilers.UseVisualStyleBackColor = true;
			// 
			// timer1
			// 
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(508, 620);
			this.Controls.Add(this.showSpoilers);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(groupBox2);
			this.Controls.Add(groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "KH2 Door Tracker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.Tracker_Load);
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label currentRoom;
		private System.Windows.Forms.Label door1;
		private System.Windows.Forms.Label door7;
		private System.Windows.Forms.Label door6;
		private System.Windows.Forms.Label door5;
		private System.Windows.Forms.Label door4;
		private System.Windows.Forms.Label door3;
		private System.Windows.Forms.Label door2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label awEvent;
		private System.Windows.Forms.Label ldEvent;
		private System.Windows.Forms.Label agEvent;
		private System.Windows.Forms.Label ocEvent;
		private System.Windows.Forms.Label bcEvent;
		private System.Windows.Forms.Label hbEvent;
		private System.Windows.Forms.Label ttEvent;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label nwEvent;
		private System.Windows.Forms.Label spEvent;
		private System.Windows.Forms.Label prEvent;
		private System.Windows.Forms.Label htEvent;
		private System.Windows.Forms.Label dcEvent;
		private System.Windows.Forms.Label plEvent;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox findRoom;
		private System.Windows.Forms.Label roomPath;
		private System.Windows.Forms.Label roomDist;
		private System.Windows.Forms.CheckBox showSpoilers;
		private System.Windows.Forms.Timer timer1;
	}
}