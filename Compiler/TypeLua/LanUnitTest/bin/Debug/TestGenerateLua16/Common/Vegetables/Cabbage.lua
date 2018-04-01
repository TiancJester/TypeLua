--== start class define ==--
local Cabbage = tlclass("TestGenerateLua16.Common.Vegetables.Cabbage","TestGenerateLua16.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua16.Common.Botany")
    Farmer = tlload("TestGenerateLua16.Common.Farmer")
    Land = tlload("TestGenerateLua16.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
