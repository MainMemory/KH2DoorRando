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

function _OnFrame()
--Disable all door blocks
for i=0,1023,8 do
	WriteLong(Save+0x1EF8+i,0)
end
--Check for doors and swap them
local room = Rooms[ReadShort(Now+0x30)]
if room ~= nil then
	local door = room[ReadInt(Now+0x20) & 0xFFFFFF]
	if door ~= nil then
		Warp(door.w, door.r, door.d)
		WriteInt(Now+0x20,0xFFFFFFFF) --Don't repeat this process indefinitely
	end
end
end
