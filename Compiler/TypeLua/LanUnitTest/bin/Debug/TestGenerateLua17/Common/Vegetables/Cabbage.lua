--== start class define ==--
local Cabbage = tlclass("TestGenerateLua17.Common.Vegetables.Cabbage","TestGenerateLua17.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua17.Common.Botany")
    Farmer = tlload("TestGenerateLua17.Common.Farmer")
    Land = tlload("TestGenerateLua17.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
