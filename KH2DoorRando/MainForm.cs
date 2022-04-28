using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

		public static Room[] rooms;
		public static Dictionary<int, Dictionary<int, Room>> roomdict = new Dictionary<int, Dictionary<int, Room>>();
		public static bool twoway;

		private static void Shuffle<T>(T[] arr, Random rand)
		{
			int[] ord = new int[arr.Length];
			for (int i = 0; i < arr.Length; i++)
				ord[i] = rand.Next();
			Array.Sort(ord, arr);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
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
				foreach (Door d in r.Doors)
				{
					d.Room = r;
					if (roomdict.TryGetValue(d.DestWorld, out Dictionary<int, Room> world) && world.TryGetValue(d.DestRoom, out Room dr))
						d.DestName = dr.Name;
					else
						d.DestName = $"{d.DestWorld}:{d.DestRoom}";
				}
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
								d.NewDestRoom = roomdict[d.DestWorld][d.DestRoom];
								if (d.NewDestRoom.CopyOf.HasValue)
									d.NewDestRoom = roomdict[d.NewDestRoom.World][d.NewDestRoom.CopyOf.Value];
								d.Used = true;
							}
							else
							{
								d.NewDestRoom = null;
								d.NewDestDoor = null;
								d.Used = false;
							}
					twoway = twoWayDoors.Checked;
					if (seedName.TextLength == 0)
						seedName.Text = Base36(DateTime.Now.Ticks) + Base36((uint)Environment.TickCount);
					Random rand = new Random(seedName.Text.GetHashCode());
					if (twoway)
					{
						Room[] multiexit = rooms.Where(a => !a.CopyOf.HasValue && !ignoreworlds.Contains(a.World) && a.Doors.Length > 1).ToArray();
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
						Room[] singles = rooms.Where(a => !a.CopyOf.HasValue && !ignoreworlds.Contains(a.World) && a.Doors.Length > 0).Except(multiexit).ToArray();
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
								dst = rooms[rand.Next(rooms.Length)];
							}
							while (dst.Doors.Length == 0);
							freeexits[freeexits.Length - 1].NewDestRoom = dst;
							freeexits[freeexits.Length - 1].NewDestDoor = dst.Doors[rand.Next(dst.Doors.Length)];
						}
					}
					else
					{
						Room[] r2 = rooms.Where(a => !a.CopyOf.HasValue && !ignoreworlds.Contains(a.World) && a.Doors.Length > 0).ToArray();
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
						Door[] freeexits = rooms.Where(a => !a.CopyOf.HasValue).SelectMany(a => a.Doors.Where(b => b.NewDestRoom == null)).ToArray();
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
					foreach (var room in rooms.Where(a => !a.CopyOf.HasValue && !ignoreworlds.Contains(a.World) && a.Doors.Length > 0))
					{
						if (room == goa)
							goa.World = 4;
						sb.AppendLine($"\t[0x{(room.ID << 8) | room.World:X4}] = {{");
						foreach (var door in room.Doors)
							sb.AppendLine($"\t\t[0x{(door.DestDoor << 16) | (door.DestRoom << 8) | door.DestWorld:X6}] = {{ w={door.NewDestRoom.World}, r={door.NewDestRoom.ID}, d={door.NewDestDoor.ID} }},");
						if (room.ExtraDoors != null)
							foreach (var door in room.ExtraDoors)
							{
								var d2 = room.Doors.Single(a => a.ID == door.CopyOf);
								sb.AppendLine($"\t\t[0x{(door.DestDoor << 16) | (door.DestRoom << 8) | door.DestWorld:X6}] = {{ w={d2.NewDestRoom.World}, r={d2.NewDestRoom.ID}, d={d2.NewDestDoor.ID} }},");
							}
						sb.AppendLine("\t},");
						if (room.Copies != null)
							foreach (int id in room.Copies)
							{
								sb.AppendLine($"\t[0x{(id << 8) | room.World:X4}] = {{");
								foreach (var door in room.Doors)
									sb.AppendLine($"\t\t[0x{(door.DestDoor << 16) | (door.DestRoom << 8) | door.DestWorld:X6}] = {{ w={door.NewDestRoom.World}, r={door.NewDestRoom.ID}, d={door.NewDestDoor.ID} }},");
								if (room.ExtraDoors != null)
									foreach (var door in room.ExtraDoors)
									{
										var d2 = room.Doors.Single(a => a.ID == door.CopyOf);
										sb.AppendLine($"\t\t[0x{(door.DestDoor << 16) | (door.DestRoom << 8) | door.DestWorld:X6}] = {{ w={d2.NewDestRoom.World}, r={d2.NewDestRoom.ID}, d={d2.NewDestDoor.ID} }},");
									}
								sb.AppendLine("\t},");
							}
					}
					sb.AppendLine("}");
					sb.Replace("{ w=18, r=7, d=1 }", "{ w=18, r=8, d=0 }");
					File.WriteAllText(dlg.FileName, File.ReadAllText("DoorRando.template.lua").Replace("--REPLACE", sb.ToString()));
					goa.World = 4;
					using (var sw = File.CreateText(Path.Combine(Path.GetDirectoryName(dlg.FileName), "DoorSpoilers.csv")))
					{
						sw.Write("Rooms,");
						foreach (var item in rooms.Where(a => !a.CopyOf.HasValue && a.Doors.Length > 0))
							sw.Write("{0},", item.Name);
						sw.WriteLine();
						for (int i = 0; i < 7; i++)
						{
							sw.Write("Exit {0},", i + 1);
							foreach (var item in rooms.Where(a => !a.CopyOf.HasValue && a.Doors.Length > 0))
							{
								if (item.Doors.Length > i)
									sw.Write("{0} -> {1}", item.Doors[i].DestName, item.Doors[i].NewDestRoom.Name);
								sw.Write(",");
							}
							sw.WriteLine();
						}
					}
					trackerButton.Enabled = true;
				}
			}
		}

		private void trackerButton_Click(object sender, EventArgs e)
		{
			using (Tracker t = new Tracker())
				t.ShowDialog(this);
		}

		private void dcEnable_CheckedChanged(object sender, EventArgs e)
		{
			cornerstoneHill.Enabled = dcEnable.Checked && goaEnable.Checked;
			if (!dcEnable.Checked || !goaEnable.Checked)
				cornerstoneHill.Checked = false;
		}
	}

	public class Room
	{
		public int World { get; set; }
		[JsonProperty("Room")]
		public int ID { get; set; }
		public string Name { get; set; }
		[JsonProperty("Copy Of")]
		public int? CopyOf { get; set; }
		public Door[] Doors { get; set; }
		public Door[] Warps { get; set; }
		public int[] Events { get; set; }
		[JsonProperty("Extra Doors")]
		public Door[] ExtraDoors { get; set; }
		public int[] Copies { get; set; }
	}

	public class Door
	{
		[JsonIgnore]
		public Room Room { get; set; }
		[JsonProperty("Door ID")]
		public int ID { get; set; }
		[JsonProperty("Copy Of")]
		public int CopyOf { get; set; }
		[JsonProperty("Dest World")]
		public int DestWorld { get; set; }
		[JsonProperty("Dest Room")]
		public int DestRoom { get; set; }
		[JsonProperty("Dest Door")]
		public int DestDoor { get; set; }
		[JsonIgnore]
		public string DestName { get; set; }
		[JsonIgnore]
		public Room NewDestRoom { get; set; }
		[JsonIgnore]
		public Door NewDestDoor { get; set; }
		[JsonIgnore]
		public bool Used { get; set; }
	}
}
