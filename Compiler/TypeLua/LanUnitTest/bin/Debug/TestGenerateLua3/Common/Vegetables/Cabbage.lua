--== start class define ==--
local Cabbage = tlclass("TestGenerateLua3.Common.Vegetables.Cabbage","TestGenerateLua3.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua3.Common.Botany")
    Farmer = tlload("TestGenerateLua3.Common.Farmer")
    Land = tlload("TestGenerateLua3.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
