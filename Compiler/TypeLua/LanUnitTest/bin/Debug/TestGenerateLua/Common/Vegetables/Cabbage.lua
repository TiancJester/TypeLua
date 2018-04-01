--== start class define ==--
local Cabbage = tlclass("TestGenerateLua.Common.Vegetables.Cabbage","TestGenerateLua.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua.Common.Botany")
    Farmer = tlload("TestGenerateLua.Common.Farmer")
    Land = tlload("TestGenerateLua.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
