using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KH2DoorRando
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		Settings settings;
		Room[] rooms, roomsavail;
		Dictionary<int, Dictionary<int, Room>> roomdict = new Dictionary<int, Dictionary<int, Room>>();

		private static void Shuffle<T>(T[] arr, Random rand)
		{
			int[] ord = new int[arr.Length];
			for (int i = 0; i < arr.Length; i++)
				ord[i] = rand.Next();
			Array.Sort(ord, arr);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (File.Exists("Settings.json"))
			{
				settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Settings.json"));
				if (!string.IsNullOrEmpty(settings.Seed))
					seedName.Text = settings.Seed;
				twoWayDoors.Checked = settings.TwoWayDoors;
				goaEnable.Checked = settings.EnableGoA;
				ttEnable.Checked = settings.EnableTT;
				hbEnable.Checked = settings.EnableHB;
				bcEnable.Checked = settings.EnableBC;
				ocEnable.Checked = settings.EnableOC;
				agEnable.Checked = settings.EnableAG;
				ldEnable.Checked = settings.EnableLD;
				awEnable.Checked = settings.EnableAW;
				plEnable.Checked = settings.EnablePL;
				atEnable.Checked = settings.EnableAT;
				dcEnable.Checked = settings.EnableDC;
				htEnable.Checked = settings.EnableHT;
				prEnable.Checked = settings.EnablePR;
				spEnable.Checked = settings.EnableSP;
				nwEnable.Checked = settings.EnableNW;
				cornerstoneHill.Checked = settings.CornerstoneHill;
			}
			else
				settings = new Settings();
			rooms = JsonConvert.DeserializeObject<Room[]>(File.ReadAllText("Door_Rando.json"));
			foreach (Room r in rooms)
			{
				Dictionary<int, Room> world;
				if (!roomdict.TryGetValue(r.World, out world))
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
				}
				foreach (Door d in r.ExtraDoors)
				{
					d.Room = r;
					d.CopyOf = Array.Find(r.Doors, a => a.ID == d.CopyOfID);
					d.DestRoom = roomdict[d.DestWorld][d.DestRoomID];
					d.DestDoor = Array.Find(d.DestRoom.Doors, a => a.ID == d.DestDoorID);
				}
				foreach (Warp w in r.Warps)
				{
					w.Room = r;
					w.DestRoom = roomdict[w.DestWorld][w.DestRoomID];
				}
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			settings.Seed = seedName.Text;
			settings.TwoWayDoors = twoWayDoors.Checked;
			settings.EnableGoA = goaEnable.Checked;
			settings.EnableTT = ttEnable.Checked;
			settings.EnableHB = hbEnable.Checked;
			settings.EnableBC = bcEnable.Checked;
			settings.EnableOC = ocEnable.Checked;
			settings.EnableAG = agEnable.Checked;
			settings.EnableLD = ldEnable.Checked;
			settings.EnableAW = awEnable.Checked;
			settings.EnablePL = plEnable.Checked;
			settings.EnableAT = atEnable.Checked;
			settings.EnableDC = dcEnable.Checked;
			settings.EnableHT = htEnable.Checked;
			settings.EnablePR = prEnable.Checked;
			settings.EnableSP = spEnable.Checked;
			settings.EnableNW = nwEnable.Checked;
			settings.CornerstoneHill = cornerstoneHill.Checked;
			File.WriteAllText("Settings.json", JsonConvert.SerializeObject(settings));
		}

		private static string Base36(long value)
		{
			StringBuilder sb = new StringBuilder();
			while (value > 0)
			{
				int digit = (int)(value % 36);
				if (digit < 10)
					sb.Insert(0, (char)('0' + digit));
				else
					sb.Insert(0, (char)('A' + (digit - 10)));
				value /= 36;
			}
			return sb.ToString();
		}

		private void generateButton_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog() {  DefaultExt = "lua", FileName = "DoorRando.lua", Filter = "Lua scripts|*.lua", RestoreDirectory = true})
			{
				string scriptpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"KINGDOM HEARTS HD 1.5+2.5 ReMIX\scripts\kh2");
				if (Directory.Exists(scriptpath))
					dlg.InitialDirectory = scriptpath;
				if (dlg.ShowDialog(this)== DialogResult.OK)
				{
					List<int> ignoreworlds = new List<int>();
					if (!goaEnable.Checked)
						ignoreworlds.Add(-1);
					if (!ttEnable.Checked)
						ignoreworlds.Add(2);
					if (!hbEnable.Checked)
						ignoreworlds.Add(4);
					if (!bcEnable.Checked)
						ignoreworlds.Add(5);
					if (!ocEnable.Checked)
						ignoreworlds.Add(6);
					if (!agEnable.Checked)
						ignoreworlds.Add(7);
					if (!ldEnable.Checked)
						ignoreworlds.Add(8);
					if (!awEnable.Checked)
						ignoreworlds.Add(9);
					if (!plEnable.Checked)
						ignoreworlds.Add(10);
					if (!atEnable.Checked)
						ignoreworlds.Add(11);
					if (!dcEnable.Checked)
					{
						ignoreworlds.Add(12);
						ignoreworlds.Add(13);
					}
					if (!htEnable.Checked)
						ignoreworlds.Add(14);
					if (!prEnable.Checked)
						ignoreworlds.Add(16);
					if (!spEnable.Checked)
						ignoreworlds.Add(17);
					if (!nwEnable.Checked)
						ignoreworlds.Add(18);
					Room goa = roomdict[4][26];
					goa.World = -1;
					foreach (Room r in rooms)
						foreach (Door d in r.Doors)
							if (ignoreworlds.Contains(r.World))
							{
								d.NewDestRoom = roomdict[d.DestWorld][d.DestRoomID];
								if (d.NewDestRoom.CopyOf != null)
									d.NewDestRoom = d.NewDestRoom.CopyOf;
								d.Used = true;
							}
							else
							{
								d.NewDestRoom = null;
								d.NewDestDoor = null;
								d.Used = null;
							}
					roomsavail = rooms.Where(a => a.CopyOf == null && !ignoreworlds.Contains(a.World) && a.Doors.Length > 0).ToArray();
					goa.World = 4;
					if (seedName.TextLength == 0)
						seedName.Text = Base36(DateTime.Now.Ticks) + Base36((uint)Environment.TickCount);
					Random rand = new Random(seedName.Text.GetHashCode());
					if (twoWayDoors.Checked)
					{
						Room[] multiexit = roomsavail.Where(a => a.Doors.Length > 1).ToArray();
						Shuffle(multiexit, rand);
						if (goaEnable.Checked)
						{
							multiexit[Array.IndexOf(multiexit, goa)] = multiexit[0];
							multiexit[0] = goa;
						}
						if (cornerstoneHill.Checked)
						{
							multiexit[Array.IndexOf(multiexit, roomdict[13][0])] = multiexit[1];
							multiexit[1] = roomdict[13][0];
						}
						for (int i = 0; i < multiexit.Length; i++)
						{
							Room src = multiexit[i];
							Room dst = multiexit[(i + 1) % multiexit.Length];
							List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null).ToList();
							List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null).ToList();
							Door from = exits[rand.Next(exits.Count)];
							Door to = ents[rand.Next(ents.Count)];
							from.NewDestRoom = dst;
							from.NewDestDoor = to;
							to.NewDestRoom = src;
							to.NewDestDoor = from;
						}
						Door[] freeexits = multiexit.SelectMany(a => a.Doors.Where(b => b.NewDestRoom == null)).ToArray();
						Shuffle(freeexits, rand);
						Room[] singles = roomsavail.Except(multiexit).ToArray();
						Shuffle(singles, rand);
						if (singles.Length > freeexits.Length)
						{
							MessageBox.Show(this, "Randomization failed! Not enough free exits for single-exit rooms!", "Randomization Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						for (int i = 0; i < singles.Length; i++)
						{
							freeexits[i].NewDestRoom = singles[i];
							freeexits[i].NewDestDoor = singles[i].Doors[0];
							singles[i].Doors[0].NewDestRoom = freeexits[i].Room;
							singles[i].Doors[0].NewDestDoor = freeexits[i];
						}
						freeexits = freeexits.Skip(singles.Length).ToArray();
						for (int i = 0; i < freeexits.Length - 1; i += 2)
						{
							freeexits[i].NewDestRoom = freeexits[i + 1].Room;
							freeexits[i].NewDestDoor = freeexits[i + 1];
							freeexits[i + 1].NewDestRoom = freeexits[i].Room;
							freeexits[i + 1].NewDestDoor = freeexits[i];
						}
						if (freeexits.Length % 2 == 1)
						{
							Room dst;
							do
							{
								dst = roomsavail[rand.Next(roomsavail.Length)];
							}
							while (dst.Doors.Length == 0);
							freeexits[freeexits.Length - 1].NewDestRoom = dst;
							freeexits[freeexits.Length - 1].NewDestDoor = dst.Doors[rand.Next(dst.Doors.Length)];
						}
					}
					else
					{
						Room[] r2 = (Room[])roomsavail.Clone();
						Shuffle(r2, rand);
						if (goaEnable.Checked)
						{
							r2[Array.IndexOf(r2, goa)] = r2[0];
							r2[0] = goa;
						}
						if (cornerstoneHill.Checked)
						{
							r2[Array.IndexOf(r2, roomdict[13][0])] = r2[1];
							r2[1] = roomdict[13][0];
						}
						for (int i = 0; i < r2.Length; i++)
						{
							Room src = r2[i];
							Room dst = r2[(i + 1) % r2.Length];
							Door from;
							do
							{
								from = src.Doors[rand.Next(src.Doors.Length)];
							}
							while (from.NewDestRoom != null);
							from.NewDestRoom = dst;
							from.NewDestDoor = dst.Doors[rand.Next(dst.Doors.Length)];
						}
						Door[] freeexits = roomsavail.SelectMany(a => a.Doors.Where(b => b.NewDestRoom == null)).ToArray();
						Shuffle(freeexits, rand);
						for (int i = 0; i < freeexits.Length; i++)
						{
							Door src = freeexits[i];
							Door dst = freeexits[(i + 1) % freeexits.Length];
							src.NewDestRoom = dst.Room;
							src.NewDestDoor = dst;
						}
					}
					var sb = new StringBuilder("Rooms = {");
					sb.AppendLine();
					foreach (var room in roomsavail)
					{
						sb.AppendLine($"\t[0x{(room.ID << 8) | room.World:X4}] = {{");
						foreach (var door in room.Doors)
							sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.NewDestRoom.World}, r={door.NewDestRoom.ID}, d={door.NewDestDoor.ID} }},");
						foreach (var door in room.ExtraDoors)
							sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.CopyOf.NewDestRoom.World}, r={door.CopyOf.NewDestRoom.ID}, d={door.CopyOf.NewDestDoor.ID} }},");
						sb.AppendLine("\t},");
						foreach (var rm2 in room.Copies)
						{
							sb.AppendLine($"\t[0x{(rm2.ID << 8) | room.World:X4}] = {{");
							foreach (var door in room.Doors)
								sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.NewDestRoom.World}, r={door.NewDestRoom.ID}, d={door.NewDestDoor.ID} }},");
							foreach (var door in room.ExtraDoors)
								sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{ w={door.CopyOf.NewDestRoom.World}, r={door.CopyOf.NewDestRoom.ID}, d={door.CopyOf.NewDestDoor.ID} }},");
							sb.AppendLine("\t},");
						}
					}
					sb.AppendLine("}");
					sb.Replace("{ w=18, r=7, d=1 }", "{ w=18, r=8, d=0 }");
					File.WriteAllText(dlg.FileName, File.ReadAllText("DoorRando.template.lua").Replace("--REPLACE", sb.ToString()));
					spoilersButton.Enabled = trackerButton.Enabled = true;
				}
			}
		}

		private void trackerButton_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "json", FileName = "DoorRando.json", Filter = "JavaScript Object Notation|*.json", RestoreDirectory = true })
				if (dlg.ShowDialog(this) == DialogResult.OK)
					File.WriteAllText(dlg.FileName, JsonConvert.SerializeObject(rooms, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore }));
		}

		private void spoilersButton_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "csv", FileName = "DoorRando.csv", Filter = "Comma Separated Values|*.csv", RestoreDirectory = true })
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

		private void dcEnable_CheckedChanged(object sender, EventArgs e)
		{
			cornerstoneHill.Enabled = dcEnable.Checked && goaEnable.Checked;
			if (!dcEnable.Checked || !goaEnable.Checked)
				cornerstoneHill.Checked = false;
		}
	}

	public class Settings
	{
		public string Seed { get; set; } = string.Empty;
		[DefaultValue(true)]
		public bool TwoWayDoors { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableGoA { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableTT { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableHB { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableBC { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableOC { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableAG { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableLD { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableAW { get; set; } = true;
		[DefaultValue(true)]
		public bool EnablePL { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableAT { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableDC { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableHT { get; set; } = true;
		[DefaultValue(true)]
		public bool EnablePR { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableSP { get; set; } = true;
		[DefaultValue(true)]
		public bool EnableNW { get; set; } = true;
		public bool CornerstoneHill { get; set; }
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
		public bool? Used { get; set; }
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
