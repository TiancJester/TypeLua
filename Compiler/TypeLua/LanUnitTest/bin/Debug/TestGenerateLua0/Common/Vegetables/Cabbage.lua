--== start class define ==--
local Cabbage = tlclass("TestGenerateLua0.Common.Vegetables.Cabbage","TestGenerateLua0.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua0.Common.Botany")
    Farmer = tlload("TestGenerateLua0.Common.Farmer")
    Land = tlload("TestGenerateLua0.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
