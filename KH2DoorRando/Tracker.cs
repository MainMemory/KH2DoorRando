using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace KH2DoorRando
{
	public partial class Tracker : Form
	{
		Process proc;
		Label[] doorLabels;
		WorldData[] worlds;
		IntPtr Now, Save;
		Room lastRoom = null;
		Room[] roomsDoors;

		public Tracker()
		{
			InitializeComponent();
			doorLabels = new[] { door1, door2, door3, door4, door5, door6, door7 };
			worlds = new[]
			{
				new WorldData(MainForm.rooms.Where(a => a.World == 2).ToList(), ttEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 4).ToList(), hbEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 5).ToList(), bcEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 6).ToList(), ocEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 7).ToList(), agEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 8).ToList(), ldEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 9).ToList(), awEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 10).ToList(), plEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 12 || a.World == 13).ToList(), dcEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 14).ToList(), htEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 16).ToList(), prEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 17).ToList(), spEvent),
				new WorldData(MainForm.rooms.Where(a => a.World == 18).ToList(), nwEvent)
			};
			roomsDoors = MainForm.rooms.Where(a => !a.CopyOf.HasValue && a.Doors.Length > 0).ToArray();
		}

		private void Tracker_Load(object sender, EventArgs e)
		{
			findRoom.BeginUpdate();
			foreach (var item in roomsDoors)
				findRoom.Items.Add(item.Name);
			findRoom.EndUpdate();
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
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
					Save = ba + 0x09A7070;
					lastRoom = null;
					foreach (var world in worlds)
						world.LastEventRoom = null;
					currentRoom.Name = "Unknown Room";
				}
				else
				{
					currentRoom.Text = "Game not running.";
					foreach (var door in doorLabels)
						door.Text = "-";
					foreach (var world in worlds)
					{
						world.EventLabel.Text = "-";
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
				if (MainForm.roomdict.TryGetValue(proc.ReadByte(Now), out var world))
					world.TryGetValue(proc.ReadByte(Now + 1), out room);
				if (room != lastRoom)
				{
					if (room != null)
					{
						currentRoom.Text = room.Name;
						if (room.CopyOf.HasValue)
							room = world[room.CopyOf.Value];
						if (lastRoom != null)
						{
							foreach (var door in lastRoom.Doors.Where(a => a.NewDestRoom == room))
								door.Used = true;
							if (MainForm.twoway)
								foreach (var door in room.Doors.Where(a => a.NewDestRoom == lastRoom))
									door.Used = true;
						}
						for (int i = 0; i < room.Doors.Length; i++)
						{
							if (room.Doors[i].Used || showSpoilers.Checked)
								doorLabels[i].Text = $"{room.Doors[i].DestName} -> {room.Doors[i].NewDestRoom.Name}";
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
						currentRoom.Name = "Unknown Room";
						foreach (var door in doorLabels)
							door.Text = "-";
						if (findRoom.SelectedIndex != -1)
						{
							roomDist.Text = "Unknown";
							roomPath.Text = "?????????????";
						}
					}
					lastRoom = room;
				}
				foreach (var wd in worlds)
				{
					bool found = false;
					foreach (var rm in wd.Rooms)
						if (proc.ReadUInt16(Save + 0x10 + 0x180 * rm.World + 0x6 * rm.ID + 4) != 0)
						{
							found = true;
							if (rm != wd.LastEventRoom)
							{
								wd.EventLabel.Text = rm.Name;
								wd.LastEventRoom = rm;
							}
							break;
						}
					if (!found && wd.LastEventRoom != null)
					{
						wd.EventLabel.Text = "No events.";
						wd.LastEventRoom = null;
					}
				}
			}
		}

		Room[] FindShortestPath(Room start, Room end)
		{
			Stack<Room> stack = new Stack<Room>(MainForm.rooms.Length);
			stack.Push(start);
			return FindShortestPath(start, end, stack, null);
		}

		Room[] FindShortestPath(Room stage, Room end, Stack<Room> path, Room[] shortestPath)
		{
			if (shortestPath != null && path.Count >= shortestPath.Length)
				return shortestPath;
			foreach (Door door in stage.Doors)
				if (!path.Contains(door.NewDestRoom) && (door.Used || showSpoilers.Checked))
				{
					path.Push(door.NewDestRoom);
					if (door.NewDestRoom == end)
					{
						if (shortestPath == null || path.Count < shortestPath.Length)
						{
							shortestPath = path.ToArray();
							Array.Reverse(shortestPath);
							path.Pop();
							return shortestPath;
						}
					}
					else
						shortestPath = FindShortestPath(door.NewDestRoom, end, path, shortestPath);
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
					if (MainForm.roomdict.TryGetValue(proc.ReadByte(Now), out var world))
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
		public Label EventLabel { get; }

		public WorldData(IList<Room> rooms, Label evlbl)
		{
			EventLabel = evlbl;
			Rooms = new System.Collections.ObjectModel.ReadOnlyCollection<Room>(rooms);
		}
	}
}
