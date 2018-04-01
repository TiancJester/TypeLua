--== start class define ==--
local Cabbage = tlclass("TestGenerateLua7.Common.Vegetables.Cabbage","TestGenerateLua7.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua7.Common.Botany")
    Farmer = tlload("TestGenerateLua7.Common.Farmer")
    Land = tlload("TestGenerateLua7.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
