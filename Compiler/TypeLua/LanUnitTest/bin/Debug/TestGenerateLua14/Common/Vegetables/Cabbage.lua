--== start class define ==--
local Cabbage = tlclass("TestGenerateLua14.Common.Vegetables.Cabbage","TestGenerateLua14.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua14.Common.Botany")
    Farmer = tlload("TestGenerateLua14.Common.Farmer")
    Land = tlload("TestGenerateLua14.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
