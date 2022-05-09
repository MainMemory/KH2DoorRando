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

		readonly string[] modeDescs =
		{
			"All rooms in the game will be connected to each other as a single group.",
			"Each door leading out of the GoA will lead to a separate set of rooms.",
			"One of the doors leading from the GoA will lead to Cornerstone Hill.\nThe six other doors in Cornerstone Hill will all lead to separate paths.\nThe other door from GoA will also lead to its own path.",
			"Rooms with more than two exits will cause a split in the path, leading to a more complex arrangement.",
			"All the doors in each world will be randomized within that world.\nDisables GoA, 100AW, and Atlantica.",
			"Any door can connect to any other door. Probably won't be completable."
		};
		readonly int[] worldids =
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
			14,
			16,
			17,
			18
		};
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
			}
			else
				settings = new Settings();
			modeSelector.SelectedIndex = settings.Mode;
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
			settings.Mode = modeSelector.SelectedIndex;
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
			File.WriteAllText("Settings.json", JsonConvert.SerializeObject(settings));
		}

		private void modeSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (modeSelector.SelectedIndex)
			{
				case 1:
					atEnable.Enabled = awEnable.Enabled = true;
					goaEnable.Enabled = false;
					goaEnable.Checked = true;
					break;
				case 2:
					atEnable.Enabled = awEnable.Enabled = true;
					dcEnable.Enabled = goaEnable.Enabled = false;
					dcEnable.Checked = goaEnable.Checked = true;
					break;
				case 4:
					spEnable.Enabled = prEnable.Enabled = atEnable.Enabled = awEnable.Enabled = goaEnable.Enabled = false;
					spEnable.Checked = prEnable.Checked = atEnable.Checked = awEnable.Checked = goaEnable.Checked = false;
					break;
				default:
					atEnable.Enabled = awEnable.Enabled = goaEnable.Enabled = true;
					break;
			}
			toolTip1.SetToolTip(modeInfo, modeDescs[modeSelector.SelectedIndex]);
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
			using (SaveFileDialog dlg = new SaveFileDialog() {  DefaultExt = "lua", FileName = "F266B00B.DoorRando.lua", Filter = "Lua scripts|*.lua", RestoreDirectory = true})
			{
				string scriptpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"KINGDOM HEARTS HD 1.5+2.5 ReMIX\scripts\kh2");
				if (Directory.Exists(scriptpath))
					dlg.CustomPlaces.Add(scriptpath);
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
								d.NewDestRoom = d.DestRoom;
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
					switch (modeSelector.SelectedIndex)
					{
						case 0:
							if (!RandomizeRooms(rand, new List<Room>(roomsavail), true))
								return;
							break;
						case 1:
							{
								Door[] goadoors = goa.Doors;
								goa.Doors = new Door[0];
								List<Room>[] roomlists = new List<Room>[2];
								if (twoWayDoors.Checked)
								{
									Room[] multiexit = roomsavail.Where(a => a.GetUnlockedDoors().Count() > 1).ToArray();
									Shuffle(multiexit, rand);
									List<Room> me2 = new List<Room>(multiexit);
									int split = rand.Next(1, multiexit.Length);
									roomlists[0] = me2.GetRange(0, split);
									roomlists[1] = me2.GetRange(split, me2.Count - split);
									Room[] singles = roomsavail.Except(multiexit).ToArray();
									Shuffle(singles, rand);
									List<Room> singlelist = new List<Room>(singles);
									int[] freecnt = new int[2];
									for (int i = 0; i < 2; i++)
									{
										freecnt[i] = roomlists[i].Sum(a => Math.Max(a.Doors.Length - 2, 0)) + 1;
										if (freecnt[i] > 0)
										{
											int exitcnt = freecnt[i] / singles.Length;
											List<Room> collection = singlelist.GetRange(0, exitcnt);
											roomlists[i].AddRange(collection);
											singlelist.RemoveRange(0, exitcnt);
											freecnt[i] += collection.Sum(a => a.Doors.Length - 2);
										}
									}
									while (singlelist.Count > 0)
									{
										int i = rand.Next(2);
										if (freecnt[i] > 0)
										{
											roomlists[i].Add(singlelist[0]);
											freecnt[i] += singlelist[0].Doors.Length - 2;
											singlelist.RemoveAt(0);
										}
										if (singlelist.Count > 0 && freecnt.All(a => a == 0))
										{
											goa.Doors = goadoors;
											MessageBox.Show(this, "Randomization failed! Not enough free exits for single-exit rooms!", "Randomization Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
									}
								}
								else
								{
									Room[] multiexit = (Room[])roomsavail.Clone();
									Shuffle(multiexit, rand);
									List<Room> me2 = new List<Room>(multiexit);
									me2.Remove(goa);
									int split = rand.Next(1, multiexit.Length);
									roomlists[0] = me2.GetRange(0, split);
									roomlists[1] = me2.GetRange(split, me2.Count - split);
								}
								for (int i = 0; i < 2; i++)
								{
									goa.Doors = new[] { goadoors[i] };
									if (!RandomizeRooms(rand, roomlists[i], false, goa))
									{
										goa.Doors = goadoors;
										return;
									}
								}
								goa.Doors = goadoors;
							}
							break;
						case 2:
							{
								Door[] goadoors = goa.Doors;
								goa.Doors = new Door[0];
								Room ch = roomdict[13][0];
								Door[] chdoors = ch.Doors;
								ch.Doors = new Door[0];
								goadoors[1].NewDestRoom = ch;
								goadoors[1].NewDestDoor = chdoors[0];
								chdoors[0].NewDestRoom = goa;
								chdoors[0].NewDestDoor = goadoors[1];
								List<Room>[] roomlists = new List<Room>[7];
								if (twoWayDoors.Checked)
								{
									Room[] multiexit = roomsavail.Where(a => a.GetUnlockedDoors().Count() > 1).ToArray();
									Shuffle(multiexit, rand);
									List<Room> me2 = new List<Room>(multiexit);
									int avg = me2.Count / 7;
									for (int i = 0; i < 6; i++)
									{
										int split = Math.Min(me2.Count - (6 - i), Math.Max(1, avg + (rand.Next(avg) - (avg / 2))));
										roomlists[i] = me2.GetRange(0, split);
										me2.RemoveRange(0, split);
									}
									roomlists[6] = me2;
									Room[] singles = roomsavail.Except(multiexit).ToArray();
									Shuffle(singles, rand);
									List<Room> singlelist = new List<Room>(singles);
									int[] freecnt = new int[7];
									for (int i = 0; i < 7; i++)
									{
										freecnt[i] = roomlists[i].Sum(a => Math.Max(a.Doors.Length - 2, 0)) + 1;
										if (freecnt[i] > 0)
										{
											int exitcnt = freecnt[i] / singles.Length;
											List<Room> collection = singlelist.GetRange(0, exitcnt);
											roomlists[i].AddRange(collection);
											singlelist.RemoveRange(0, exitcnt);
											freecnt[i] += collection.Sum(a => a.Doors.Length - 2);
										}
									}
									while (singlelist.Count > 0)
									{
										int i = rand.Next(7);
										if (freecnt[i] > 0)
										{
											roomlists[i].Add(singlelist[0]);
											freecnt[i] += singlelist[0].Doors.Length - 2;
											singlelist.RemoveAt(0);
										}
										if (singlelist.Count > 0 && freecnt.All(a => a == 0))
										{
											goa.Doors = goadoors;
											ch.Doors = chdoors;
											MessageBox.Show(this, "Randomization failed! Not enough free exits for single-exit rooms!", "Randomization Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
									}
								}
								else
								{
									Room[] multiexit = (Room[])roomsavail.Clone();
									Shuffle(multiexit, rand);
									List<Room> me2 = new List<Room>(multiexit);
									me2.Remove(goa);
									me2.Remove(ch);
									int avg = me2.Count / 7;
									for (int i = 0; i < 6; i++)
									{
										int split = Math.Min(me2.Count - (6 - i), Math.Max(1, avg + (rand.Next(avg) - (avg / 2))));
										roomlists[i] = me2.GetRange(0, split);
										me2.RemoveRange(0, split);
									}
									roomlists[6] = me2;
								}
								for (int i = 0; i < 6; i++)
								{
									ch.Doors = new[] { chdoors[i + 1] };
									if (!RandomizeRooms(rand, roomlists[i], false, ch))
									{
										goa.Doors = goadoors;
										ch.Doors = chdoors;
										return;
									}
								}
								goa.Doors = new[] { goadoors[0] };
								if (!RandomizeRooms(rand, roomlists[6], false, goa))
								{
									goa.Doors = goadoors;
									ch.Doors = chdoors;
									return;
								}
								goa.Doors = goadoors;
								ch.Doors = chdoors;
							}
							break;
						case 3:
							if (twoWayDoors.Checked)
							{
								Room[] forks = roomsavail.Where(a => a.Doors.Length > 2).ToArray();
								Shuffle(forks, rand);
								List<Room> used = new List<Room>() { forks[0] };
								foreach (var dst in forks.Skip(1))
								{
									Room src = used[rand.Next(used.Count)];
									List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
									List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
									Door from = exits[rand.Next(exits.Count)];
									Door to = ents[rand.Next(ents.Count)];
									from.NewDestRoom = dst;
									from.NewDestDoor = to;
									to.NewDestRoom = src;
									to.NewDestDoor = from;
									if (exits.Count == 1)
										used.Remove(src);
									if (ents.Count > 1)
										used.Add(dst);
								}
								Door[] doors = forks.SelectMany(a => a.Doors.Where(b => b.NewDestDoor == null)).ToArray();
								Shuffle(doors, rand);
								Door[] singles = roomsavail.Where(a => a.Doors.Length == 1).Select(a => a.Doors[0]).ToArray();
								if (singles.Length > doors.Length)
								{
									MessageBox.Show(this, "Randomization failed! Not enough free exits for single-exit rooms!", "Randomization Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
									return;
								}
								Shuffle(singles, rand);
								for (int i = 0; i < singles.Length; i++)
								{
									doors[i].NewDestRoom = singles[i].Room;
									doors[i].NewDestDoor = singles[i];
									singles[i].NewDestRoom = doors[i].Room;
									singles[i].NewDestDoor = doors[i];
								}
								List<Door> doorlist = new List<Door>(doors.Skip(singles.Length));
								for (int i = 0; i < doorlist.Count - 1; i += 2)
									while (doorlist[i].Room == doorlist[i + 1].Room)
									{
										int nd = rand.Next(doorlist.Count);
										if (doorlist[nd].Room != doorlist[i].Room && doorlist[nd ^ 1].Room != doorlist[i].Room)
										{
											Door d = doorlist[i];
											doorlist[i] = doorlist[nd];
											doorlist[nd] = d;
										}
									}
								while (doorlist.Count > 1)
								{
									doorlist[0].NewDestRoom = doorlist[1].Room;
									doorlist[0].NewDestDoor = doorlist[1];
									doorlist[1].NewDestRoom = doorlist[0].Room;
									doorlist[1].NewDestDoor = doorlist[0];
									doorlist.RemoveRange(0, 2);
								}
								if (doorlist.Count == 1)
								{
									doorlist[0].NewDestRoom = doorlist[0].Room;
									doorlist[0].NewDestDoor = doorlist[0];
								}
								doors = forks.SelectMany(a => a.Doors).ToArray();
								foreach (Room r in roomsavail.Where(a => a.Doors.Length == 2))
								{
									Door d = doors[rand.Next(doors.Length)];
									Door d2 = d.NewDestDoor;
									d.NewDestRoom = r;
									d.NewDestDoor = r.Doors[0];
									r.Doors[0].NewDestRoom = d.Room;
									r.Doors[0].NewDestDoor = d;
									d2.NewDestRoom = r;
									d2.NewDestDoor = r.Doors[1];
									r.Doors[1].NewDestRoom = d2.Room;
									r.Doors[1].NewDestDoor = d2;
								}
							}
							else if(!RandomizeRooms(rand, new List<Room>(roomsavail), true))
								return;
							break;
						case 4:
							foreach (int w in worldids.Except(ignoreworlds))
							{
								List<Room> wr = roomsavail.Where(a => a.World == w).ToList();
								if (w == 12)
									wr.AddRange(roomsavail.Where(a => a.World == 13));
								if (twoWayDoors.Checked)
								{
									Room[] multiexit = wr.Where(a => a.Doors.Length > 1).ToArray();
									Shuffle(multiexit, rand);
									for (int i = 0; i < multiexit.Length - 1; i++)
									{
										Room src = multiexit[i];
										Room dst = multiexit[i + 1];
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
									Room[] singles = wr.Except(multiexit).ToArray();
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
										while (freeexits[i].Room == freeexits[i + 1].Room)
										{
											int nd = rand.Next(freeexits.Length);
											if (freeexits[nd].Room != freeexits[i].Room && freeexits[nd ^ 1].Room != freeexits[i].Room)
											{
												Door d = freeexits[i];
												freeexits[i] = freeexits[nd];
												freeexits[nd] = d;
											}
										}
									for (int i = 0; i < freeexits.Length - 1; i += 2)
									{
										freeexits[i].NewDestRoom = freeexits[i + 1].Room;
										freeexits[i].NewDestDoor = freeexits[i + 1];
										freeexits[i + 1].NewDestRoom = freeexits[i].Room;
										freeexits[i + 1].NewDestDoor = freeexits[i];
									}
									if (freeexits.Length % 2 == 1)
									{
										freeexits[freeexits.Length - 1].NewDestRoom = freeexits[freeexits.Length - 1].Room;
										freeexits[freeexits.Length - 1].NewDestDoor = freeexits[freeexits.Length - 1];
									}
								}
								else
								{
									Room[] r2 = wr.ToArray();
									Shuffle(r2, rand);
									for (int i = 0; i < r2.Length - 1; i++)
									{
										Room src = r2[i];
										Room dst = r2[i + 1];
										Door from;
										do
										{
											from = src.Doors[rand.Next(src.Doors.Length)];
										}
										while (from.NewDestRoom != null);
										from.NewDestRoom = dst;
										from.NewDestDoor = dst.Doors[rand.Next(dst.Doors.Length)];
									}
									List<Room> left = wr.Where(a => a.Doors.Any(b => b.NewDestRoom == null)).ToList();
									while (left.Count > 1)
									{
										r2 = left.ToArray();
										Shuffle(r2, rand);
										for (int i = 0; i < r2.Length; i++)
										{
											Room src = r2[i];
											Room dst = r2[(i + 1) % r2.Length];
											List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null).ToList();
											List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null).ToList();
											Door from = exits[rand.Next(exits.Count)];
											from.NewDestRoom = dst;
											from.NewDestDoor = ents[rand.Next(ents.Count)];
											if (exits.Count == 1)
												left.Remove(src);
										}
									}
									if (left.Count == 1)
										foreach (Door d in left[0].Doors)
										{
											d.NewDestRoom = d.Room;
											d.NewDestDoor = d;
										}
								}
							}
							break;
						case 5:
							{
								Door[] alldoors = roomsavail.SelectMany(a => a.Doors).ToArray();
								foreach (Door door in alldoors)
								{
									door.NewDestDoor = alldoors[rand.Next(alldoors.Length)];
									door.NewDestRoom = door.NewDestDoor.Room;
								}
							}
							break;
					}
					var sb = new StringBuilder("Rooms = {");
					sb.AppendLine();
					foreach (var room in roomsavail.Where(a => a.Doors.Any(b => b.NewDestDoor != b.DestDoor)))
					{
						sb.AppendLine($"\t[0x{(room.ID << 8) | room.World:X4}] = {{");
						foreach (var door in room.Doors.Where(a => a.NewDestDoor != a.DestDoor))
							PrintDoorInfo(sb, door);
						foreach (var door in room.ExtraDoors.Where(a => a.CopyOf.NewDestDoor != a.CopyOf.DestDoor))
							PrintDoorInfo(sb, door);
						sb.AppendLine("\t},");
						foreach (var rm2 in room.Copies)
						{
							sb.AppendLine($"\t[0x{(rm2.ID << 8) | room.World:X4}] = {{");
							foreach (var door in room.Doors.Where(a => a.NewDestDoor != a.DestDoor))
								PrintDoorInfo(sb, door);
							foreach (var door in room.ExtraDoors.Where(a => a.CopyOf.NewDestDoor != a.CopyOf.DestDoor))
								PrintDoorInfo(sb, door);
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

		private static void PrintDoorInfo(StringBuilder sb, Door door)
		{
			Door d2 = door;
			if (door.CopyOf != null)
				d2 = door.CopyOf;
			sb.AppendLine($"\t\t[0x{(door.DestDoorID << 16) | (door.DestRoomID << 8) | door.DestWorld:X6}] = {{");
			sb.AppendLine($"\t\t\tw = {d2.NewDestWorld},");
			sb.AppendLine($"\t\t\tr = {d2.NewDestRoomID},");
			sb.AppendLine($"\t\t\td = {d2.NewDestDoorID},");
			if (d2.DisableEvents != null)
				sb.AppendLine($"\t\t\te = {{ {string.Join(", ", d2.DisableEvents)} }},");
			sb.AppendLine("\t\t},");
		}

		private bool RandomizeRooms(Random rand, List<Room> roomset, bool connectends, Room startroom = null)
		{
			if (twoWayDoors.Checked)
			{
				Room[] multiexit = roomset.Where(a => a.GetUnlockedDoors().Count() > 1).ToArray();
				Shuffle(multiexit, rand);
				for (int i = 0; i < (connectends ? multiexit.Length : multiexit.Length - 1); i++)
				{
					Room src = multiexit[i];
					Room dst = multiexit[(i + 1) % multiexit.Length];
					List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
					List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
					Door from = exits[rand.Next(exits.Count)];
					Door to = ents[rand.Next(ents.Count)];
					from.NewDestRoom = dst;
					from.NewDestDoor = to;
					to.NewDestRoom = src;
					to.NewDestDoor = from;
				}
				if (startroom != null)
				{
					Door from = startroom.Doors[0];
					Door to = multiexit[0].Doors.First(a => a.NewDestRoom == null && a.IsUnlocked());
					from.NewDestRoom = multiexit[0];
					from.NewDestDoor = to;
					to.NewDestRoom = startroom;
					to.NewDestDoor = from;
				}
				Door[] freeexits = multiexit.SelectMany(a => a.Doors.Where(b => b.NewDestRoom == null)).ToArray();
				Shuffle(freeexits, rand);
				Room[] lockedrooms = roomset.Where(a => a.Doors.Length > 1 && a.GetUnlockedDoors().Count() == 1).ToArray();
				Door[] lockedroomdoors = lockedrooms.Select(a => a.Doors.Single(b => b.IsUnlocked())).ToArray();
				Shuffle(lockedroomdoors, rand);
				for (int i = 0; i < lockedrooms.Length; i++)
				{
					freeexits[i].NewDestRoom = lockedroomdoors[i].Room;
					freeexits[i].NewDestDoor = lockedroomdoors[i];
					lockedroomdoors[i].NewDestRoom = freeexits[i].Room;
					lockedroomdoors[i].NewDestDoor = freeexits[i];
				}
				freeexits = freeexits.Skip(lockedrooms.Length).Concat(lockedrooms.SelectMany(a => a.Doors.Where(b => b.Locked ?? false))).ToArray();
				Door[] singles = roomset.Where(a => a.Doors.Length == 1).Select(a => a.Doors[0]).ToArray();
				Shuffle(singles, rand);
				if (singles.Length > freeexits.Length)
				{
					MessageBox.Show(this, "Randomization failed! Not enough free exits for single-exit rooms!", "Randomization Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				for (int i = 0; i < singles.Length; i++)
				{
					freeexits[i].NewDestRoom = singles[i].Room;
					freeexits[i].NewDestDoor = singles[i];
					singles[i].NewDestRoom = freeexits[i].Room;
					singles[i].NewDestDoor = freeexits[i];
				}
				freeexits = freeexits.Skip(singles.Length).ToArray();
				for (int i = 0; i < freeexits.Length - 1; i += 2)
					while (freeexits[i].Room == freeexits[i + 1].Room)
					{
						int nd = rand.Next(freeexits.Length);
						if (freeexits[nd].Room != freeexits[i].Room && freeexits[nd ^ 1].Room != freeexits[i].Room)
						{
							Door d = freeexits[i];
							freeexits[i] = freeexits[nd];
							freeexits[nd] = d;
						}
					}
				for (int i = 0; i < freeexits.Length - 1; i += 2)
				{
					freeexits[i].NewDestRoom = freeexits[i + 1].Room;
					freeexits[i].NewDestDoor = freeexits[i + 1];
					freeexits[i + 1].NewDestRoom = freeexits[i].Room;
					freeexits[i + 1].NewDestDoor = freeexits[i];
				}
				if (freeexits.Length % 2 == 1)
				{
					freeexits[freeexits.Length - 1].NewDestRoom = freeexits[freeexits.Length - 1].Room;
					freeexits[freeexits.Length - 1].NewDestDoor = freeexits[freeexits.Length - 1];
				}
			}
			else
			{
				Room[] r2 = roomset.ToArray();
				Shuffle(r2, rand);
				for (int i = 0; i < (connectends ? r2.Length : r2.Length - 1); i++)
				{
					Room src = r2[i];
					Room dst = r2[(i + 1) % r2.Length];
					List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
					List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null && a.IsUnlocked()).ToList();
					Door from = exits[rand.Next(exits.Count)];
					from.NewDestRoom = dst;
					from.NewDestDoor = ents[rand.Next(ents.Count)];
				}
				if (startroom != null)
				{
					Door from = startroom.Doors[0];
					from.NewDestRoom = r2[0];
					from.NewDestDoor = r2[0].Doors.First(a => a.NewDestRoom != null && a.IsUnlocked());
					Door to = r2[r2.Length - 1].Doors.First(a => a.NewDestRoom != null && a.IsUnlocked());
					to.NewDestRoom = startroom;
					to.NewDestDoor = startroom.Doors[0];
				}
				List<Room> left = roomset.Where(a => a.Doors.Any(b => b.NewDestRoom == null)).ToList();
				while (left.Count > 1)
				{
					r2 = left.ToArray();
					Shuffle(r2, rand);
					for (int i = 0; i < r2.Length; i++)
					{
						Room src = r2[i];
						Room dst = r2[(i + 1) % r2.Length];
						List<Door> exits = src.Doors.Where(a => a.NewDestRoom == null).ToList();
						List<Door> ents = dst.Doors.Where(a => a.NewDestRoom == null).ToList();
						Door from = exits[rand.Next(exits.Count)];
						from.NewDestRoom = dst;
						from.NewDestDoor = ents[rand.Next(ents.Count)];
						if (exits.Count == 1)
							left.Remove(src);
					}
				}
				if (left.Count == 1)
					foreach (Door d in left[0].Doors)
					{
						d.NewDestRoom = d.Room;
						d.NewDestDoor = d;
					}
			}
			return true;
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
	}

	public class Settings
	{
		public string Seed { get; set; } = string.Empty;
		public int Mode { get; set; }
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

		public IEnumerable<Door> GetUnlockedDoors() => Doors.Where(a => a.IsUnlocked());
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
		public bool? Locked { get; set; }
		public bool? Used { get; set; }

		public bool IsUnlocked() => !(Locked ?? false);
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
