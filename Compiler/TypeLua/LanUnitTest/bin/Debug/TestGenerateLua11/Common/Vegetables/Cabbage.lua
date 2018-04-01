--== start class define ==--
local Cabbage = tlclass("TestGenerateLua11.Common.Vegetables.Cabbage","TestGenerateLua11.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua11.Common.Botany")
    Farmer = tlload("TestGenerateLua11.Common.Farmer")
    Land = tlload("TestGenerateLua11.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
