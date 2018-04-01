--== start class define ==--
local Cabbage = tlclass("TestGenerateLua5.Common.Vegetables.Cabbage","TestGenerateLua5.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua5.Common.Botany")
    Farmer = tlload("TestGenerateLua5.Common.Farmer")
    Land = tlload("TestGenerateLua5.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
