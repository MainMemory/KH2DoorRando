using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KH2DoorPlando
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		Settings settings;
		string openedFile;
		readonly int[] worldnums = new int[]
		{
			2,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			16,
			17,
			18
		};
		Room[] rooms, roomsavail, curWorld;
		readonly Dictionary<int, Dictionary<int, Room>> roomdict = new Dictionary<int, Dictionary<int, Room>>();
		Room curRoom;
		Door curDoor;
		readonly List<Door> warnDoors = new List<Door>();

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (File.Exists("Settings.json"))
			{
				settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Settings.json"));
				twoWayDoorsToolStripMenuItem.Checked = settings.TwoWayDoors;
			}
			else
				settings = new Settings();
			LoadFile("Door_Rando.json");
		}

		private void LoadFile(string filename)
		{
			rooms = JsonConvert.DeserializeObject<Room[]>(File.ReadAllText(filename));
			roomdict.Clear();
			foreach (Room r in rooms)
			{
				if (!roomdict.TryGetValue(r.World, out Dictionary<int, Room> world))
				{
					world = new Dictionary<int, Room>();
					roomdict.Add(r.World, world);
				}
				world.Add(r.ID, r);
			}
			foreach (Room r in rooms)
			{
				if (r.CopyOfID.HasValue)
					r.CopyOf = roomdict[r.World][r.CopyOfID.Value];
				if (r.CopyIDs != null)
					r.Copies = r.CopyIDs.Select(a => roomdict[r.World][a]).ToArray();
				else
				{
					r.CopyIDs = new int[0];
					r.Copies = new Room[0];
				}
				if (r.ExtraDoors == null)
					r.ExtraDoors = new Door[0];
				foreach (Door d in r.Doors)
				{
					d.Room = r;
					d.DestRoom = roomdict[d.DestWorld][d.DestRoomID];
					d.DestDoor = Array.Find(d.DestRoom.Doors, a => a.ID == d.DestDoorID);
					if (d.NewDestWorld != 0)
					{
						d.NewDestRoom = roomdict[d.NewDestWorld][d.NewDestRoomID];
						d.NewDestDoor = Array.Find(d.NewDestRoom.Doors, a => a.ID == d.NewDestDoorID);
					}
					else
					{
						d.NewDestRoom = d.DestRoom;
						d.NewDestDoor = d.DestDoor;
					}
					d.NewDestDoor.Uses++;
				}
				foreach (Door d in r.ExtraDoors)
				{
					d.Room = r;
					d.CopyOf = Array.Find(r.Doors, a => a.ID == d.CopyOfID);
					d.DestRoom = roomdict[d.DestWorld][d.DestRoomID];
					d.DestDoor = Array.Find(d.DestRoom.Doors, a => a.ID == d.DestDoorID);
				}
			}
			foreach (Room r in rooms)
				foreach (Door d in r.Doors)
					CheckDoorUses(d);
			UpdateWarningList();
			roomsavail = rooms.Where(a => a.CopyOf == null && a.Doors.Length > 0).ToArray();
			destRoom.Items.AddRange(roomsavail.Select(a => a.Name).ToArray());
			worldSelect.SelectedIndex = -1;
			worldSelect.SelectedIndex = 0;
		}

		private void Save()
		{
			File.WriteAllText(openedFile, JsonConvert.SerializeObject(rooms, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore }));
		}

		private void SaveAs()
		{
			using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "json", FileName = "DoorRando.json", Filter = "JavaScript Object Notation|*.json", RestoreDirectory = true })
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					openedFile = dlg.FileName;
					Save();
				}
		}

		private void CheckDoorUses(Door d)
		{
			if (warnDoors.Contains(d))
			{
				if (d.Uses == 1)
					warnDoors.Remove(d);
			}
			else if (d.Uses != 1)
				warnDoors.Add(d);
		}

		private void UpdateWarningList()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Door d in warnDoors)
				if (d.Uses == 0)
					sb.AppendLine($"{d.Room.Name}.{d.ID} Door is not linked to from any other doors!");
				else if (d.Uses > 1)
					sb.AppendLine($"{d.Room.Name}.{d.ID} Multiple doors link to this door!");
			textBox1.Text = sb.ToString();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			settings.TwoWayDoors = twoWayDoorsToolStripMenuItem.Checked;
			File.WriteAllText("Settings.json", JsonConvert.SerializeObject(settings));
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openedFile = null;
			LoadFile("Door_Rando.json");
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = "json", Filter = "JavaScript Object Notation|*.json", RestoreDirectory = true })
			{
				if (openedFile != null)
					dlg.InitialDirectory = Path.GetDirectoryName(openedFile);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					openedFile = dlg.FileName;
					LoadFile(openedFile);
				}
			}
		}

		private void worldSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (worldSelect.SelectedIndex == -1) return;
			roomSelect.Items.Clear();
			curWorld = roomsavail.Where(a => a.World == worldnums[worldSelect.SelectedIndex]).ToArray();
			roomSelect.Items.AddRange(curWorld.Select(a => a.Name).ToArray());
		}

		private void roomSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			doorSelect.Items.Clear();
			if (roomSelect.SelectedIndex != -1)
			{
				curRoom = curWorld[roomSelect.SelectedIndex];
				doorSelect.Items.AddRange(curRoom.Doors.Select(a => $"{a.ID} ({a.DestRoom.Name})").ToArray());
				doorSelect.Enabled = true;
			}
			else
				doorSelect.Enabled = false;
		}

		private void doorSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (doorSelect.SelectedIndex != -1)
			{
				destDoor.Enabled = destRoom.Enabled = true;
				curDoor = curRoom.Doors[doorSelect.SelectedIndex];
				Room ndr = curDoor.NewDestRoom;
				destRoom.SelectedIndex = Array.IndexOf(roomsavail, ndr);
				destDoor.SelectedIndex = Array.IndexOf(ndr.Doors, curDoor.NewDestDoor);
			}
			else
				destDoor.Enabled = destRoom.Enabled = false;
		}

		private void destRoom_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (destRoom.SelectedIndex == -1)
			{
				destDoor.Enabled = false;
				return;
			}
			destDoor.Enabled = true;
			destDoor.Items.Clear();
			destDoor.Items.AddRange(roomsavail[destRoom.SelectedIndex].Doors.Select(a => $"{a.ID} ({a.DestRoom.Name})").ToArray());
		}

		private void destDoor_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (destDoor.SelectedIndex == -1)
				return;
			curDoor.NewDestDoor.Uses--;
			CheckDoorUses(curDoor.NewDestDoor);
			curDoor.NewDestRoom = roomsavail[destRoom.SelectedIndex];
			curDoor.NewDestDoor = curDoor.NewDestRoom.Doors[destDoor.SelectedIndex];
			curDoor.NewDestDoor.Uses++;
			CheckDoorUses(curDoor.NewDestDoor);
			if (twoWayDoorsToolStripMenuItem.Checked)
			{
				curDoor.NewDestDoor.NewDestDoor.Uses--;
				CheckDoorUses(curDoor.NewDestDoor);
				curDoor.NewDestDoor.NewDestRoom = curRoom;
				curDoor.NewDestDoor.NewDestDoor = curDoor;
				curDoor.Uses++;
				CheckDoorUses(curDoor);
			}
			UpdateWarningList();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openedFile == null)
				SaveAs();
			else
				Save();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void exportScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "lua", Filter = "Lua scripts|*.lua", RestoreDirectory = true })
			{
				if (openedFile != null)
				{
					dlg.InitialDirectory = Path.GetDirectoryName(openedFile);
					dlg.FileName = Path.GetFileNameWithoutExtension(openedFile) + ".lua";
				}
				string scriptpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"KINGDOM HEARTS HD 1.5+2.5 ReMIX\scripts\kh2");
				if (Directory.Exists(scriptpath))
					dlg.CustomPlaces.Add(scriptpath);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					var sb = new StringBuilder("Rooms = {");
					sb.AppendLine();
					foreach (var room in roomsavail.Where(a => a.Doors.Any(b => b.NewDestDoor != b.DestDoor)))
					{
						sb.AppendLine($"\t[0x{(room.ID << 8) | room.World:X4}] = {{");
						foreach (var door in room.Doors.Where(a => a.NewDestDoor != a.DestDoor))
							sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.NewDestWorld}, r={door.NewDestRoomID}, d={door.NewDestDoorID} }},");
						foreach (var door in room.ExtraDoors.Where(a => a.CopyOf.NewDestDoor != a.CopyOf.DestDoor))
							sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.CopyOf.NewDestWorld}, r={door.CopyOf.NewDestRoomID}, d={door.CopyOf.NewDestDoorID} }},");
						sb.AppendLine("\t},");
						foreach (var rm2 in room.Copies)
						{
							sb.AppendLine($"\t[0x{(rm2.ID << 8) | room.World:X4}] = {{");
							foreach (var door in room.Doors.Where(a => a.NewDestDoor != a.DestDoor))
								sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.NewDestWorld}, r={door.NewDestRoomID}, d={door.NewDestDoorID} }},");
							foreach (var door in room.ExtraDoors.Where(a => a.CopyOf.NewDestDoor != a.CopyOf.DestDoor))
								sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.CopyOf.NewDestWorld}, r={door.CopyOf.NewDestRoomID}, d={door.CopyOf.NewDestDoorID} }},");
							sb.AppendLine("\t},");
						}
					}
					sb.AppendLine("}");
					sb.Replace("{ w=18, r=7, d=1 }", "{ w=18, r=8, d=0 }");
					File.WriteAllText(dlg.FileName, File.ReadAllText("DoorRando.template.lua").Replace("--REPLACE", sb.ToString()));
				}
			}
		}

		private void exportSpoilersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "csv", Filter = "Comma Separated Values|*.csv", RestoreDirectory = true })
			{
				if (openedFile != null)
				{
					dlg.InitialDirectory = Path.GetDirectoryName(openedFile);
					dlg.FileName = Path.GetFileNameWithoutExtension(openedFile) + ".csv";
				}
				if (dlg.ShowDialog(this) == DialogResult.OK)
					using (var sw = File.CreateText(dlg.FileName))
					{
						sw.Write("Rooms,");
						foreach (var item in roomsavail)
							sw.Write("{0},", item.Name);
						sw.WriteLine();
						for (int i = 0; i < 7; i++)
						{
							sw.Write("Exit {0},", i + 1);
							foreach (var item in roomsavail)
							{
								if (item.Doors.Length > i)
									sw.Write("{0} -> {1}", item.Doors[i].DestRoom.Name, item.Doors[i].NewDestRoom.Name);
								sw.Write(",");
							}
							sw.WriteLine();
						}
					}
			}
		}
	}

	public class Settings
	{
		[DefaultValue(true)]
		public bool TwoWayDoors { get; set; }
	}

	public class Room
	{
		public int World { get; set; }
		[JsonProperty("Room")]
		public int ID { get; set; }
		public string Name { get; set; }
		[JsonProperty("Copy Of")]
		public int? CopyOfID { get; set; }
		[JsonIgnore]
		public Room CopyOf { get; set; }
		public Door[] Doors { get; set; }
		public Warp[] Warps { get; set; }
		public int[] Events { get; set; }
		[JsonProperty("Extra Doors")]
		public Door[] ExtraDoors { get; set; }
		[JsonProperty("Copies")]
		public int[] CopyIDs { get; set; }
		[JsonIgnore]
		public Room[] Copies { get; set; }
	}

	public class Door
	{
		[JsonIgnore]
		public Room Room { get; set; }
		[JsonProperty("Door ID")]
		public int ID { get; set; }
		[JsonProperty("Copy Of")]
		public int? CopyOfID { get; set; }
		[JsonIgnore]
		public Door CopyOf { get; set; }
		private int destWorld;
		[JsonProperty("Dest World")]
		public int DestWorld
		{
			get => DestRoom?.World ?? destWorld;
			set => destWorld = value;
		}
		private int destRoom;
		[JsonProperty("Dest Room")]
		public int DestRoomID
		{
			get => DestRoom?.ID ?? destRoom;
			set => destRoom = value;
		}
		[JsonIgnore]
		public Room DestRoom { get; set; }
		private int destDoor;
		[JsonProperty("Dest Door")]
		public int DestDoorID
		{
			get => DestDoor?.ID ?? destDoor;
			set => destDoor = value;
		}
		[JsonIgnore]
		public Door DestDoor { get; set; }
		private int newdestWorld;
		[JsonProperty("New Dest World")]
		public int NewDestWorld
		{
			get => NewDestRoom?.World ?? newdestWorld;
			set => newdestWorld = value;
		}
		private int newdestRoom;
		[JsonProperty("New Dest Room")]
		public int NewDestRoomID
		{
			get => NewDestRoom?.ID ?? newdestRoom;
			set => newdestRoom = value;
		}
		[JsonIgnore]
		public Room NewDestRoom { get; set; }
		private int newdestDoor;
		[JsonProperty("New Dest Door")]
		public int NewDestDoorID
		{
			get => NewDestDoor?.ID ?? newdestDoor;
			set => newdestDoor = value;
		}
		[JsonIgnore]
		public Door NewDestDoor { get; set; }
		[JsonIgnore]
		public Label DoorLabel { get; set; }
		[JsonIgnore]
		public ComboBox RoomSelector { get; set; }
		[JsonIgnore]
		public ComboBox DoorSelector { get; set; }
		[JsonIgnore]
		public int Uses { get; set; }
	}

	public class Warp
	{
		[JsonIgnore]
		public Room Room { get; set; }
		private int destWorld;
		[JsonProperty("Dest World")]
		public int DestWorld
		{
			get => DestRoom?.World ?? destWorld;
			set => destWorld = value;
		}
		private int destRoom;
		[JsonProperty("Dest Room")]
		public int DestRoomID
		{
			get => DestRoom?.ID ?? destRoom;
			set => destRoom = value;
		}
		[JsonIgnore]
		public Room DestRoom { get; set; }
	}
}
