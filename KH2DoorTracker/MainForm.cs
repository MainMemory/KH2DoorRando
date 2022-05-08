using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KH2DoorTracker
{
	public partial class MainForm : Form
	{
		string filename;
		Process proc;
		Label[] doorLabels;
		Room[] rooms;
		Dictionary<int, Dictionary<int, Room>> roomdict = new Dictionary<int, Dictionary<int, Room>>();
		WorldData[] worlds;
		IntPtr Now, Save;
		Room lastRoom = null;
		Room[] roomsDoors;
		Door[] doors;

		public MainForm()
		{
			InitializeComponent();
		}

		private void Tracker_Load(object sender, EventArgs e)
		{
			doorLabels = new[] { door1, door2, door3, door4, door5, door6, door7 };
			using (OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = "json", Filter = "JavaScript Object Notation|*.json", RestoreDirectory = true, Title = "Select Your Seed File" })
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					filename = dlg.FileName;
					rooms = JsonConvert.DeserializeObject<Room[]>(File.ReadAllText(filename));
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
						}
						foreach (Door d in r.ExtraDoors)
						{
							d.Room = r;
							d.CopyOf = Array.Find(r.Doors, a => a.ID == d.CopyOfID);
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
						}
						foreach (Warp w in r.Warps)
						{
							w.Room = r;
							w.DestRoom = roomdict[w.DestWorld][w.DestRoomID];
						}
					}
					worlds = new[]
					{
						new WorldData(rooms.Where(a => a.World == 2).ToList(), ttEvent),
						new WorldData(rooms.Where(a => a.World == 4).ToList(), hbEvent),
						new WorldData(rooms.Where(a => a.World == 5).ToList(), bcEvent),
						new WorldData(rooms.Where(a => a.World == 6).ToList(), ocEvent),
						new WorldData(rooms.Where(a => a.World == 7).ToList(), agEvent),
						new WorldData(rooms.Where(a => a.World == 8).ToList(), ldEvent),
						new WorldData(rooms.Where(a => a.World == 9).ToList(), awEvent),
						new WorldData(rooms.Where(a => a.World == 10).ToList(), plEvent),
						new WorldData(rooms.Where(a => a.World == 12 || a.World == 13).ToList(), dcEvent),
						new WorldData(rooms.Where(a => a.World == 14).ToList(), htEvent),
						new WorldData(rooms.Where(a => a.World == 16).ToList(), prEvent),
						new WorldData(rooms.Where(a => a.World == 17).ToList(), spEvent),
						new WorldData(rooms.Where(a => a.World == 18).ToList(), nwEvent)
					};
					roomsDoors = rooms.Where(a => a.CopyOf == null && a.Doors.Length > 0).ToArray();
					roomCount.Text = $"Rooms: {roomsDoors.Count(a => a.Visited)}/{roomsDoors.Length}";
					doors = roomsDoors.SelectMany(a => a.Doors).ToArray();
					doorCount.Text = $"Doors: {doors.Count(a => a.Used)}/{doors.Length}";
					findRoom.BeginUpdate();
					foreach (var item in roomsDoors)
						findRoom.Items.Add(item.Name);
					findRoom.EndUpdate();
					timer1.Start();
				}
				else
					Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (rooms != null)
			{
				switch (MessageBox.Show(this, "Do you want to save the seed's progress?", "Save Seed Progress", MessageBoxButtons.YesNoCancel))
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
					case DialogResult.Yes:
						using (SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = "json", FileName = Path.GetFileName(filename), Filter = "JavaScript Object Notation|*.json", InitialDirectory = Path.GetDirectoryName(filename), RestoreDirectory = true })
							switch (dlg.ShowDialog(this))
							{
								case DialogResult.OK:
									File.WriteAllText(dlg.FileName, JsonConvert.SerializeObject(rooms, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore }));
									break;
								case DialogResult.Cancel:
									e.Cancel = true;
									return;
							}
						break;
				}
				timer1.Stop();
			}
		}

		int tries = 0;
		private void timer1_Tick(object sender, EventArgs e)
		{
			bool newproc = false;
			if (proc == null)
			{
				var pl = Process.GetProcessesByName("KINGDOM HEARTS II FINAL MIX");
				if (pl.Length > 0)
				{
					proc = pl[0];
					proc.EnableRaisingEvents = true;
					proc.Exited += Proc_Exited;
					var ba = proc.MainModule.BaseAddress;
					Now = ba + 0x0714DB8;
					Save = ba + 0x09A70B0;
					lastRoom = null;
					foreach (var world in worlds)
						world.LastEventRoom = null;
					newproc = true;
				}
				else
				{
					currentRoom.Text = "Game not running.";
					currentRoom.Links.Clear();
					foreach (var door in doorLabels)
						door.Text = "-";
					foreach (var world in worlds)
					{
						world.EventLabel.Text = "-";
						world.EventLabel.Links.Clear();
						world.LastEventRoom = null;
					}
					roomDist.Text = "-";
					roomPath.Text = "-";
					return;
				}
			}
			lock (proc)
			{
				Room room = null;
				if (roomdict.TryGetValue(proc.ReadByte(Now), out var world))
					world.TryGetValue(proc.ReadByte(Now + 1), out room);
				if (room != lastRoom || newproc)
				{
					if (room != null)
					{
						currentRoom.Text = room.Name;
						if (room.CopyOf != null)
							room = room.CopyOf;
						currentRoom.Links.Clear();
						currentRoom.Links.Add(new LinkLabel.Link(0, currentRoom.Text.Length, room));
						if (lastRoom != null)
						{
							foreach (var door in lastRoom.Doors.Where(a => a.NewDestRoom == room))
								door.Used = true;
							foreach (var door in room.Doors.Where(a => a.NewDestRoom == lastRoom))
								door.Used = true;
						}
						for (int i = 0; i < room.Doors.Length; i++)
						{
							if (room.Doors[i].Used || showSpoilers.Checked)
								doorLabels[i].Text = $"{room.Doors[i].DestRoom.Name} -> {room.Doors[i].NewDestRoom.Name}";
							else
								doorLabels[i].Text = "???????";
						}
						for (int i = room.Doors.Length; i < 7; i++)
							doorLabels[i].Text = "-";
						if (findRoom.SelectedIndex != -1)
						{
							Room[] path = FindShortestPath(room, roomsDoors[findRoom.SelectedIndex]);
							if (path != null)
							{
								roomDist.Text = $"{path.Length - 1} room(s)";
								roomPath.Text = string.Join(" -> ", path.Select(a => a.Name));
							}
							else
							{
								roomDist.Text = "Unknown";
								roomPath.Text = "?????????????";
							}
						}
					}
					else
					{
						currentRoom.Text = "Unknown Room";
						currentRoom.Links.Clear();
						foreach (var door in doorLabels)
							door.Text = "-";
						if (findRoom.SelectedIndex != -1)
						{
							roomDist.Text = "Unknown";
							roomPath.Text = "?????????????";
						}
					}
					if (lastRoom == null || lastRoom.Doors.Any(a => a.NewDestRoom == room) || tries++ >= 2)
					{
						tries = 0;
						if (room != null)
							room.Visited = true;
						lastRoom = room;
					}
					roomCount.Text = $"Rooms: {roomsDoors.Count(a => a.Visited)}/{roomsDoors.Length}";
					doorCount.Text = $"Doors: {doors.Count(a => a.Used)}/{doors.Length}";
				}
				foreach (var wd in worlds)
				{
					bool found = false;
					foreach (var rm in wd.Rooms)
						if (Array.IndexOf(rm.Events, proc.ReadUInt16(Save + 0x10 + 0x180 * rm.World + 0x6 * rm.ID + 4)) != -1)
						{
							found = true;
							if (rm != wd.LastEventRoom)
							{
								wd.EventLabel.Text = rm.Name;
								wd.EventLabel.Links.Clear();
								wd.EventLabel.Links.Add(new LinkLabel.Link(0, rm.Name.Length, rm));
								wd.LastEventRoom = rm;
							}
							break;
						}
					if (!found && (newproc || wd.LastEventRoom != null))
					{
						wd.EventLabel.Text = "N/A";
						wd.EventLabel.Links.Clear();
						wd.LastEventRoom = null;
					}
				}
			}
		}

		Room[] FindShortestPath(Room start, Room end)
		{
			if (start == end)
				return new[] { start };
			Stack<Room> stack = new Stack<Room>(rooms.Length);
			stack.Push(start);
			return FindShortestPath(start, end, stack, null);
		}

		Room[] FindShortestPath(Room stage, Room end, Stack<Room> path, Room[] shortestPath)
		{
			if (stage.CopyOf != null)
				stage = stage.CopyOf;
			if (shortestPath != null && path.Count >= shortestPath.Length)
				return shortestPath;
			Queue<Room> nextrooms = new Queue<Room>();
			foreach (Door door in stage.Doors)
				if (!path.Contains(door.NewDestRoom) && (door.Used || showSpoilers.Checked))
				{
					if (door.NewDestRoom == end)
					{
						if (shortestPath == null || path.Count < shortestPath.Length)
						{
							path.Push(door.NewDestRoom);
							shortestPath = path.ToArray();
							Array.Reverse(shortestPath);
							path.Pop();
							return shortestPath;
						}
					}
					else if (!nextrooms.Contains(door.NewDestRoom))
						nextrooms.Enqueue(door.NewDestRoom);
				}
			foreach (Warp door in stage.Warps)
				if (!path.Contains(door.DestRoom))
				{
					if (door.DestRoom == end)
					{
						if (shortestPath == null || path.Count < shortestPath.Length)
						{
							path.Push(door.DestRoom);
							shortestPath = path.ToArray();
							Array.Reverse(shortestPath);
							path.Pop();
							return shortestPath;
						}
					}
					else if (!nextrooms.Contains(door.DestRoom))
						nextrooms.Enqueue(door.DestRoom);
				}
			while (nextrooms.Count > 0)
			{
				Room destroom = nextrooms.Dequeue();
				path.Push(destroom);
				shortestPath = FindShortestPath(destroom, end, path, shortestPath);
				path.Pop();
			}
			return shortestPath;
		}

		private void findRoom_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (findRoom.SelectedIndex == -1)
			{
				roomDist.Text = "-";
				roomPath.Text = "-";
			}
			else if (proc != null)
			{
				lock (proc)
				{
					Room room = null;
					if (roomdict.TryGetValue(proc.ReadByte(Now), out var world))
						world.TryGetValue(proc.ReadByte(Now + 1), out room);
					if (room != null)
					{
						Room[] path = FindShortestPath(room, roomsDoors[findRoom.SelectedIndex]);
						if (path != null)
						{
							roomDist.Text = $"{path.Length - 1} room(s)";
							roomPath.Text = string.Join(" -> ", path.Select(a => a.Name));
						}
						else
						{
							roomDist.Text = "Unknown";
							roomPath.Text = "?????????????";
						}
					}
					else
					{
						roomDist.Text = "Unknown";
						roomPath.Text = "?????????????";
					}
				}
			}
		}

		private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			findRoom.SelectedIndex = Array.IndexOf(roomsDoors, (Room)e.Link.LinkData);
		}

		private void Proc_Exited(object sender, EventArgs e)
		{
			lock (proc)
				proc = null;
		}
	}

	class WorldData
	{
		public Room LastEventRoom { get; set; }
		public System.Collections.ObjectModel.ReadOnlyCollection<Room> Rooms { get; }
		public LinkLabel EventLabel { get; }

		public WorldData(IList<Room> rooms, LinkLabel evlbl)
		{
			EventLabel = evlbl;
			Rooms = new System.Collections.ObjectModel.ReadOnlyCollection<Room>(rooms);
		}
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
		public bool Visited { get; set; }
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
		[JsonProperty("Disable Events")]
		public int[] DisableEvents { get; set; }
		public bool Used { get; set; }
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
