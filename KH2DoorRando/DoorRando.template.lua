LUAGUI_NAME = 'Door Randomizer'
LUAGUI_AUTH = 'MainMemory'
LUAGUI_DESC = 'Randomizes the doors in all the rooms in the game.'

function _OnInit()
if (GAME_ID == 0xF266B00B or GAME_ID == 0xFAF99301) and ENGINE_TYPE == "ENGINE" then --PCSX2
	if ENGINE_VERSION < 3.0 then
		print('LuaEngine is Outdated. Things might not work properly.')
	end
	OnPC = false
	Now = 0x032BAE0 --Current Location
	Save = 0x032BB30 --Save File
elseif GAME_ID == 0x431219CC and ENGINE_TYPE == 'BACKEND' then --PC
	if ENGINE_VERSION < 5.0 then
		ConsolePrint('LuaBackend is Outdated. Things might not work properly.',2)
	end
	OnPC = true
	Now = 0x0714DB8 - 0x56454E
	Save = 0x09A7070 - 0x56450E
end
end

function Warp(W,R,D,M,B,E) --Warp into the appropriate World, Room, Door, Map, Btl, Evt
M = M or ReadShort(Save + 0x10 + 0x180*W + 0x6*R)
B = B or ReadShort(Save + 0x10 + 0x180*W + 0x6*R + 2)
E = E or ReadShort(Save + 0x10 + 0x180*W + 0x6*R + 4)
WriteByte(Now+0x00,W)
WriteByte(Now+0x01,R)
WriteShort(Now+0x02,D)
WriteShort(Now+0x04,M)
WriteShort(Now+0x06,B)
WriteShort(Now+0x08,E)
--Record Location in Save File
WriteByte(Save+0x000C,W)
WriteByte(Save+0x000D,R)
WriteShort(Save+0x000E,D)
end

--REPLACE

function CheckDisableCombo()
if OnPC then
	return (ReadInt(0x24944A2) & 0x2000c00) == 0x2000c00
else
	return (ReadShort(0x34D45C) & 0x1800) == 0
end
end

function _OnFrame()
--Disable all door blocks
for i=0,1023,8 do
	WriteLong(Save+0x1EF8+i,0)
end
--Remove ship blocks in Port Royal
WriteByte(Save+0x1E98,ReadByte(Save+0x1E98) & ~0xD)
WriteByte(Save+0x1E99,ReadByte(Save+0x1E99) & ~2)
--Force all rooms' MAP values
for wr,room in ipairs(Rooms) do
	if room.map ~= nil then
		local w = wr & 0xFF
		local r = (wr >> 8) & 0xFF
		WriteShort(Save + 0x10 + 0x180*w + 0x6*r, room.map)
	end
end
if CheckDisableCombo() then
	DisableWarp = true
end
--Check for doors and swap them
local room = Rooms[ReadShort(Now+0x30)]
if room ~= nil then
	local door = room[ReadInt(Now+0x20) & 0xFFFFFF]
	if door ~= nil then
		if door.e ~= nil then
			local evt = ReadShort(Now+0x38)
			for i,v in ipairs(door.e) do
				if v == evt then
					WriteByte(Now+0x20,door.w)
					WriteByte(Now+0x21,door.r)
					WriteShort(Now+0x22,door.d)
					WriteArray(Now+0x24,ReadArray(Save + 0x10 + 0x180*door.w + 0x6*door.r,6))
					return
				end
			end
		end
		if not DisableWarp then
			Warp(door.w, door.r, door.d)
		else
			DisableWarp = false
		end
		WriteInt(Now+0x20,0xFFFFFFFF) --Don't repeat this process indefinitely
	end
end
end
